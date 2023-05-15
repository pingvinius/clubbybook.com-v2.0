namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class BookSelectCriteria
    {
        #region Where Routine

        public sealed class UrlRewrite : BaseUrlRewriteSelectCriteria<Book>
        {
            public UrlRewrite(string urlRewrite)
                : base(urlRewrite)
            {
            }

            public override IQueryable<Book> Apply(IQueryable<Book> query)
            {
                return query.Where(a => a.UrlRewrite.Equals(this.UrlRewrite, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public sealed class Confirmed : BaseWhereSelectCriteria<Book>
        {
            public override IQueryable<Book> Apply(IQueryable<Book> query)
            {
                return query.Where(b => b.Confirmed);
            }
        }

        public sealed class DefaultSearch : BaseWhereSelectCriteria<Book>
        {
            public string SearchKey { get; private set; }

            public DefaultSearch(string searchKey)
            {
                this.SearchKey = searchKey;
            }

            public override IQueryable<Book> Apply(IQueryable<Book> query)
            {
                string searchKey = this.SearchKey.Trim();

                if (string.IsNullOrWhiteSpace(searchKey))
                {
                    return query;
                }

                var words = searchKey.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return query.Where(b => words.Any(w => b.Title.Contains(w) || b.OriginalTitle.Contains(w) || b.Description.Contains(w)));
            }
        }

        public sealed class Duplicated : BaseWhereSelectCriteria<Book>
        {
            public Book Book { get; private set; }

            public Duplicated(Book book)
            {
                this.Book = book;
            }

            public override IQueryable<Book> Apply(IQueryable<Book> query)
            {
                return query.Where(e => e.Id != this.Book.Id && e.UrlRewrite.Equals(this.Book.UrlRewrite, StringComparison.OrdinalIgnoreCase));
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class TitleSort : BaseOrderBySelectCriteria<Book>
        {
            public override IQueryable<Book> Apply(IQueryable<Book> query)
            {
                return query.OrderBy(entity => entity.Title);
            }
        }

        #endregion OrderBy Routine
    }
}