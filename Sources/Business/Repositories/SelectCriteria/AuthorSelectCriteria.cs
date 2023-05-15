namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class AuthorSelectCriteria
    {
        #region Where Routine

        public sealed class UrlRewrite : BaseUrlRewriteSelectCriteria<Author>
        {
            public UrlRewrite(string urlRewrite)
                : base(urlRewrite)
            {
            }

            public override IQueryable<Author> Apply(IQueryable<Author> query)
            {
                return query.Where(a => a.UrlRewrite.Equals(this.UrlRewrite, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public sealed class DefaultSearch : BaseWhereSelectCriteria<Author>
        {
            public string SearchKey { get; private set; }

            public DefaultSearch(string searchKey)
            {
                this.SearchKey = searchKey;
            }

            public override IQueryable<Author> Apply(IQueryable<Author> query)
            {
                string searchKey = this.SearchKey.Trim();

                if (string.IsNullOrWhiteSpace(searchKey))
                {
                    return query;
                }

                var words = searchKey.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return query.Where(a => words.Any(w => a.FullName.Contains(w) || a.ShortDescription.Contains(w) || a.Biography.Contains(w)));
            }
        }

        public sealed class AutoComplete : BaseWhereSelectCriteria<Author>
        {
            public string SearchKey { get; private set; }

            public AutoComplete(string searchKey)
            {
                this.SearchKey = searchKey;
            }

            public override IQueryable<Author> Apply(IQueryable<Author> query)
            {
                string searchKey = this.SearchKey.Trim();

                if (string.IsNullOrWhiteSpace(searchKey))
                {
                    return query;
                }

                return query.Where(a => a.FullName.Contains(searchKey));
            }
        }

        public sealed class Duplicated : BaseWhereSelectCriteria<Author>
        {
            public Author Author { get; private set; }

            public Duplicated(Author author)
            {
                this.Author = author;
            }

            public override IQueryable<Author> Apply(IQueryable<Author> query)
            {
                return query.Where(e => e.Id != this.Author.Id && e.UrlRewrite.Equals(this.Author.UrlRewrite, StringComparison.OrdinalIgnoreCase));
            }
        }

        public sealed class ByIdList : BaseWhereSelectCriteria<Author>
        {
            public IEnumerable<int> Ids { get; private set; }

            public ByIdList(IEnumerable<int> ids)
            {
                this.Ids = ids;
            }

            public override IQueryable<Author> Apply(IQueryable<Author> query)
            {
                if (this.Ids == null || this.Ids.Count() == 0)
                {
                    return this.GetNoResults();
                }

                return query.Where(a => this.Ids.Contains(a.Id));
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class FullNameSort : BaseOrderBySelectCriteria<Author>
        {
            public override IQueryable<Author> Apply(IQueryable<Author> query)
            {
                return query.OrderBy(entity => entity.FullName);
            }
        }

        #endregion OrderBy Routine
    }
}