namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;

    internal sealed class ProfileRepository : EntityRepository<Profile>, IProfileRepository
    {
        protected override DbSet<Profile> EntityList
        {
            get { return this.Context.Profiles; }
        }

        protected override DbQuery<Profile> ConfigureQueryForSelect(DbQuery<Profile> query)
        {
            query = base.ConfigureQueryForSelect(query);
            query = query.Include("User");
            query = query.Include("City");
            return query;
        }

        protected override DbQuery<Profile> ConfigureQueryForGet(DbQuery<Profile> query)
        {
            query = base.ConfigureQueryForGet(query);
            query = query.Include("User");
            query = query.Include("City");
            return query;
        }

        protected override bool IsEntityUnique(Profile entity)
        {
            return this.Count(new ProfileSelectCriteria.Duplicated(entity)) == 0;
        }

        #region IUrlRewriteEntityRepository<Profile> Members

        public Profile Get(string urlRewrite)
        {
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentNullException("urlRewrite");
            }

            return this.Select(new ProfileSelectCriteria.UrlRewrite(urlRewrite)).FirstOrDefault();
        }

        #endregion IUrlRewriteEntityRepository<Profile> Members
    }
}