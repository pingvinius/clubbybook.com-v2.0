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
    using Pingvinius.Framework.Repositories.SelectCriteria;

    internal sealed class CityRepository : EntityRepository<City>, ICityRepository
    {
        protected override DbSet<City> EntityList
        {
            get { return this.Context.Cities; }
        }

        protected override IList<ISelectCriteria<City>> GetDefaultSelectCriteriaForSelect()
        {
            var selectCriteria = base.GetDefaultSelectCriteriaForSelect();
            selectCriteria.Add(new CitySelectCriteria.DefaultSort());
            return selectCriteria;
        }

        protected override DbQuery<City> ConfigureQueryForGet(DbQuery<City> query)
        {
            query = base.ConfigureQueryForGet(query);
            query = query.Include("District");
            query = query.Include("Country");
            return query;
        }

        protected override City CreateInstanceInternal()
        {
            throw new NotSupportedException();
        }

        protected override void SaveChangesInternal(City entity)
        {
            throw new NotSupportedException();
        }

        protected override void DeleteInternal(City entity)
        {
            throw new NotSupportedException();
        }

        #region IUrlRewriteEntityRepository<City> Members

        public City Get(string urlRewrite)
        {
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentNullException("urlRewrite");
            }

            return this.Select(new CitySelectCriteria.UrlRewrite(urlRewrite)).FirstOrDefault();
        }

        #endregion IUrlRewriteEntityRepository<City> Members
    }
}