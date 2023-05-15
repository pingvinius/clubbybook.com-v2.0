namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class NewsSelectCriteria
    {
        #region Where Routine

        public sealed class UrlRewrite : BaseUrlRewriteSelectCriteria<News>
        {
            public UrlRewrite(string urlRewrite)
                : base(urlRewrite)
            {
            }

            public override IQueryable<News> Apply(IQueryable<News> query)
            {
                return query.Where(n => n.UrlRewrite.Equals(this.UrlRewrite, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public sealed class DefaultSearch : BaseWhereSelectCriteria<News>
        {
            public string SearchKey { get; private set; }

            public DefaultSearch(string searchKey)
            {
                this.SearchKey = searchKey;
            }

            public override IQueryable<News> Apply(IQueryable<News> query)
            {
                string searchKey = this.SearchKey.Trim();

                if (string.IsNullOrWhiteSpace(searchKey))
                {
                    return query;
                }

                var words = searchKey.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return query.Where(n => words.Any(w => n.Title.Contains(w)));
            }
        }

        public sealed class Duplicated : BaseWhereSelectCriteria<News>
        {
            public News News { get; private set; }

            public Duplicated(News news)
            {
                this.News = news;
            }

            public override IQueryable<News> Apply(IQueryable<News> query)
            {
                return query.Where(e => e.Id != this.News.Id && e.UrlRewrite.Equals(this.News.UrlRewrite, StringComparison.OrdinalIgnoreCase));
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class CreatedDateSort : BaseOrderBySelectCriteria<News>
        {
            public override IQueryable<News> Apply(IQueryable<News> query)
            {
                return query.OrderByDescending(entity => entity.CreatedDate);
            }
        }

        #endregion OrderBy Routine
    }
}