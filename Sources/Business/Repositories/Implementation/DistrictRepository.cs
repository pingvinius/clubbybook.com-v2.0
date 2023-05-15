namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    internal sealed class DistrictRepository : EntityRepository<District>, IDistrictRepository
    {
        protected override DbSet<District> EntityList
        {
            get { return this.Context.Districts; }
        }

        protected override IList<ISelectCriteria<District>> GetDefaultSelectCriteriaForSelect()
        {
            var selectCriteria = base.GetDefaultSelectCriteriaForSelect();
            selectCriteria.Add(new DistrictSelectCriteria.DefaultSort());
            return selectCriteria;
        }

        protected override District CreateInstanceInternal()
        {
            throw new NotSupportedException();
        }

        protected override void SaveChangesInternal(District entity)
        {
            throw new NotSupportedException();
        }

        protected override void DeleteInternal(District entity)
        {
            throw new NotSupportedException();
        }
    }
}