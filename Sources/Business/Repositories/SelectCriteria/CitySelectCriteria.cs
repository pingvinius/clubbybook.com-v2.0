namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class CitySelectCriteria
    {
        #region Where Routine

        public sealed class UrlRewrite : BaseUrlRewriteSelectCriteria<City>
        {
            public UrlRewrite(string urlRewrite)
                : base(urlRewrite)
            {
            }

            public override IQueryable<City> Apply(IQueryable<City> query)
            {
                return query.Where(с => с.UrlRewrite.Equals(this.UrlRewrite, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public sealed class Country : BaseWhereSelectCriteria<City>
        {
            public int CountryId { get; private set; }

            public Country(int countryId)
            {
                this.CountryId = countryId;
            }

            public override IQueryable<City> Apply(IQueryable<City> query)
            {
                return query.Where(entity => entity.CountryId == this.CountryId);
            }
        }

        public sealed class District : BaseWhereSelectCriteria<City>
        {
            public int DistrictId { get; private set; }

            public District(int districtId)
            {
                this.DistrictId = districtId;
            }

            public override IQueryable<City> Apply(IQueryable<City> query)
            {
                return query.Where(entity => entity.DistrictId == this.DistrictId);
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class DefaultSort : BaseOrderBySelectCriteria<City>
        {
            public override IQueryable<City> Apply(IQueryable<City> query)
            {
                return query.OrderByDescending(entity => entity.Default).ThenBy(entity => entity.Name);
            }
        }

        #endregion OrderBy Routine
    }
}