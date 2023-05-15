using System.Linq;
using ClubbyBook.Data.Models;
using Pingvinius.Framework.Repositories.SelectCriteria;

namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    public static class UserSelectCriteria
    {
        #region Where Routine

        public sealed class ByEmail : BaseWhereSelectCriteria<User>
        {
            public string Email { get; private set; }

            public ByEmail(string email)
            {
                this.Email = email;
            }

            public override IQueryable<User> Apply(IQueryable<User> query)
            {
                if (string.IsNullOrWhiteSpace(this.Email))
                {
                    return this.GetNoResults();
                }

                return query.Where(u => u.EMail == this.Email);
            }
        }

        public sealed class NotDeleted : BaseWhereSelectCriteria<User>
        {
            public override IQueryable<User> Apply(IQueryable<User> query)
            {
                return query.Where(u => !u.IsDeleted);
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        //public sealed class DefaultSort : OrderBySelectCriteria<User>
        //{
        //    public override IQueryable<User> Apply(IQueryable<User> query)
        //    {
        //        return query.OrderBy(entity => entity.Name);
        //    }
        //}

        #endregion OrderBy Routine
    }
}