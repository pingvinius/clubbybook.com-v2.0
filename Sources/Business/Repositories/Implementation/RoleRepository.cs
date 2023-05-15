namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    internal sealed class RoleRepository : EntityRepository<Role>, IRoleRepository
    {
        protected override DbSet<Role> EntityList
        {
            get { return this.Context.Roles; }
        }

        protected override IList<ISelectCriteria<Role>> GetDefaultSelectCriteriaForSelect()
        {
            var selectCriteria = base.GetDefaultSelectCriteriaForSelect();
            selectCriteria.Add(new RoleSelectCriteria.DefaultSort());
            return selectCriteria;
        }

        protected override Role CreateInstanceInternal()
        {
            throw new NotSupportedException();
        }

        protected override void SaveChangesInternal(Role entity)
        {
            throw new NotSupportedException();
        }

        protected override void DeleteInternal(Role entity)
        {
            throw new NotSupportedException();
        }

        #region IRoleRepository Members

        public Role GetAccountRole()
        {
            return this.Select(new RoleSelectCriteria.ByName(RoleNames.Account)).FirstOrDefault();
        }

        #endregion IRoleRepository Members
    }
}