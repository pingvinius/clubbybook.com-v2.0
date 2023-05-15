namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System.Collections.Generic;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class RoleSelectCriteria
    {
        #region Where Routine

        public sealed class ByName : BaseWhereSelectCriteria<Role>
        {
            public string Name { get; private set; }

            public ByName(string name)
            {
                this.Name = name;
            }

            public override IQueryable<Role> Apply(IQueryable<Role> query)
            {
                if (string.IsNullOrWhiteSpace(this.Name))
                {
                    return this.GetNoResults();
                }

                return query.Where(r => r.Name == this.Name);
            }
        }

        public sealed class UserRoles : BaseWhereSelectCriteria<Role>
        {
            public int UserId { get; private set; }

            public UserRoles(int userId)
            {
                this.UserId = userId;
            }

            public override IQueryable<Role> Apply(IQueryable<Role> query)
            {
                return query.Where(r => r.UserRoles.Any(ur => ur.UserId == this.UserId));
            }
        }

        public sealed class RoleIds : BaseWhereSelectCriteria<Role>
        {
            public IEnumerable<int> Ids { get; private set; }

            public RoleIds(IEnumerable<int> ids)
            {
                this.Ids = ids;
            }

            public override IQueryable<Role> Apply(IQueryable<Role> query)
            {
                if (this.Ids == null)
                {
                    this.Ids = new List<int>();
                }

                return query.Where(r => this.Ids.Contains(r.Id));
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class DefaultSort : BaseOrderBySelectCriteria<Role>
        {
            public override IQueryable<Role> Apply(IQueryable<Role> query)
            {
                return query.OrderBy(entity => entity.Name);
            }
        }

        #endregion OrderBy Routine
    }
}