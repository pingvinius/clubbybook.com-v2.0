namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    internal sealed class CountryRepository : EntityRepository<Country>, ICountryRepository
    {
        protected override DbSet<Country> EntityList
        {
            get { return this.Context.Countries; }
        }

        protected override IList<ISelectCriteria<Country>> GetDefaultSelectCriteriaForSelect()
        {
            var selectCriteria = base.GetDefaultSelectCriteriaForSelect();
            selectCriteria.Add(new CountrySelectCriteria.DefaultSort());
            return selectCriteria;
        }

        protected override Country CreateInstanceInternal()
        {
            throw new NotSupportedException();
        }

        protected override void SaveChangesInternal(Country entity)
        {
            throw new NotSupportedException();
        }

        protected override void DeleteInternal(Country entity)
        {
            throw new NotSupportedException();
        }

        #region ICountryRepository Members

        public Country GetDefaultCountry()
        {
            return this.Select(new CountrySelectCriteria.DefaultCountry()).First();
        }

        #endregion ICountryRepository Members
    }
}