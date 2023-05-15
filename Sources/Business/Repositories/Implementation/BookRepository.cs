namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;

    internal sealed class BookRepository : EntityRepository<Book>, IBookRepository
    {
        protected override DbSet<Book> EntityList
        {
            get { return this.Context.Books; }
        }

        protected override DbQuery<Book> ConfigureQueryForGet(DbQuery<Book> query)
        {
            return base.ConfigureQueryForGet(query).Include("BookAuthors");
        }

        protected override DbQuery<Book> ConfigureQueryForSelect(DbQuery<Book> query)
        {
            return base.ConfigureQueryForSelect(query).Include("BookAuthors");
        }

        protected override bool IsEntityUnique(Book entity)
        {
            return this.Count(new BookSelectCriteria.Duplicated(entity)) == 0;
        }

        #region IUrlRewriteEntityRepository<Book> Members

        public Book Get(string urlRewrite)
        {
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentNullException("urlRewrite");
            }

            return this.Select(new BookSelectCriteria.UrlRewrite(urlRewrite)).FirstOrDefault();
        }

        #endregion IUrlRewriteEntityRepository<Book> Members

        #region IBookRepository Members

        public IReadOnlyList<UserBook> GetUserBooks(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return this.Context.UserBooks
                .Include("Book")
                .Where(ub => ub.UserId == user.Id)
                .OrderByDescending(ub => ub.iStatus)
                .ThenBy(ub => ub.Book.Title)
                .ToList();
        }

        public void SetBookGenres(Book book, Genre newGenre)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            if (newGenre == null)
            {
                throw new ArgumentNullException("newGenre");
            }

            // Get current genres for specified book
            var bookGenres = this.Context.BookGenres.Where(bookGenre => bookGenre.BookId == book.Id).ToList();

            // Remove all old genre links
            foreach (var bookGenre in bookGenres)
            {
                this.Context.BookGenres.Remove(bookGenre);
            }

            // Add new genre link
            this.Context.BookGenres.Add(new BookGenre()
            {
                BookId = book.Id,
                GenreId = newGenre.Id
            });

            // Save changes
            this.Context.SaveChanges();
        }

        public void SetBookAuthors(Book book, IEnumerable<Author> newAuthors)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            if (newAuthors == null)
            {
                throw new ArgumentNullException("newAuthors");
            }

            if (newAuthors.Count() == 0)
            {
                throw new ArgumentException("The book should contain at least one author.");
            }

            // Get current authors for specified book
            var currentBookAuthors = this.Context.BookAuthors.Where(bookAuthor => bookAuthor.BookId == book.Id).ToList();

            // Return in case the authors for book aren't changed
            if (currentBookAuthors.Count == newAuthors.Count() && newAuthors.Count() == currentBookAuthors.Select(ur => ur.AuthorId).Intersect(newAuthors.Select(r => r.Id)).Count())
            {
                return;
            }

            // Remove all old author links
            foreach (var bookAuthor in currentBookAuthors)
            {
                this.Context.BookAuthors.Remove(bookAuthor);
            }

            // Add new author links
            foreach (var author in newAuthors)
            {
                this.Context.BookAuthors.Add(new BookAuthor()
                {
                    BookId = book.Id,
                    AuthorId = author.Id
                });
            }

            // Save changes
            this.Context.SaveChanges();
        }

        #endregion IBookRepository Members
    }
}