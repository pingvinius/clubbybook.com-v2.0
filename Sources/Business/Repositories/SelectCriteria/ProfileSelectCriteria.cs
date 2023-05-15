namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class ProfileSelectCriteria
    {
        #region Where Routine

        public sealed class UrlRewrite : BaseUrlRewriteSelectCriteria<Profile>
        {
            public UrlRewrite(string urlRewrite)
                : base(urlRewrite)
            {
            }

            public override IQueryable<Profile> Apply(IQueryable<Profile> query)
            {
                return query.Where(p => p.UrlRewrite.Equals(this.UrlRewrite, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public sealed class DefaultSearch : BaseWhereSelectCriteria<Profile>
        {
            public string SearchKey { get; private set; }

            public DefaultSearch(string searchKey)
            {
                this.SearchKey = searchKey;
            }

            public override IQueryable<Profile> Apply(IQueryable<Profile> query)
            {
                string searchKey = this.SearchKey.Trim();

                if (string.IsNullOrWhiteSpace(searchKey))
                {
                    return query;
                }

                return query.Where(p => p.Name.Contains(searchKey) || p.Surname.Contains(searchKey) || p.Nickname.Contains(searchKey) || p.User.EMail.Contains(searchKey));
            }
        }

        public sealed class NotDeleted : BaseWhereSelectCriteria<Profile>
        {
            public override IQueryable<Profile> Apply(IQueryable<Profile> query)
            {
                return query.Where(p => !p.User.IsDeleted);
            }
        }

        public sealed class Duplicated : BaseWhereSelectCriteria<Profile>
        {
            public Profile Profile { get; private set; }

            public Duplicated(Profile profile)
            {
                this.Profile = profile;
            }

            public override IQueryable<Profile> Apply(IQueryable<Profile> query)
            {
                return query.Where(e => e.Id != this.Profile.Id && e.UrlRewrite.Equals(this.Profile.UrlRewrite, StringComparison.OrdinalIgnoreCase));
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class RegistrationDateSort : BaseOrderBySelectCriteria<Profile>
        {
            public override IQueryable<Profile> Apply(IQueryable<Profile> query)
            {
                return query.OrderByDescending(entity => entity.User.CreatedDate);
            }
        }

        #endregion OrderBy Routine
    }
}