namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;

    internal sealed class AuthorRepository : EntityRepository<Author>, IAuthorRepository
    {
        protected override DbSet<Author> EntityList
        {
            get { return this.Context.Authors; }
        }

        protected override bool IsEntityUnique(Author entity)
        {
            return this.Count(new AuthorSelectCriteria.Duplicated(entity)) == 0;
        }

        #region IUrlRewriteEntityRepository<Author> Members

        public Author Get(string urlRewrite)
        {
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentNullException("urlRewrite");
            }

            return this.Select(new AuthorSelectCriteria.UrlRewrite(urlRewrite)).FirstOrDefault();
        }

        #endregion IUrlRewriteEntityRepository<Author> Members
    }
}