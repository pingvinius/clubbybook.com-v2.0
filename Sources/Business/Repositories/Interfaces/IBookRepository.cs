namespace ClubbyBook.Business.Repositories.Interfaces
{
    using System.Collections.Generic;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories;

    public interface IBookRepository : IUrlRewriteEntityRepository<Book>
    {
        IReadOnlyList<UserBook> GetUserBooks(User user);

        void SetBookGenres(Book book, Genre newGenre);

        void SetBookAuthors(Book book, IEnumerable<Author> newAuthors);
    }
}