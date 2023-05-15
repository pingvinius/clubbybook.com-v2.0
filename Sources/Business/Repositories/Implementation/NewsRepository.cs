namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;

    internal sealed class NewsRepository : EntityRepository<News>, INewsRepository
    {
        protected override DbSet<News> EntityList
        {
            get { return this.Context.News; }
        }

        protected override bool IsEntityUnique(News entity)
        {
            return this.Count(new NewsSelectCriteria.Duplicated(entity)) == 0;
        }

        #region IUrlRewriteEntityRepository<News> Members

        public News Get(string urlRewrite)
        {
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentNullException("urlRewrite");
            }

            return this.Select(new NewsSelectCriteria.UrlRewrite(urlRewrite)).FirstOrDefault();
        }

        #endregion IUrlRewriteEntityRepository<News> Members
    }
}