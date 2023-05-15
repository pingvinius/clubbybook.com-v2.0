namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class DistrictSelectCriteria
    {
        #region Where Routine

        public sealed class Country : BaseWhereSelectCriteria<District>
        {
            public int CountryId { get; private set; }

            public Country(int countryId)
            {
                this.CountryId = countryId;
            }

            public override IQueryable<District> Apply(IQueryable<District> query)
            {
                return query.Where(entity => entity.CountryId == this.CountryId);
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class DefaultSort : BaseOrderBySelectCriteria<District>
        {
            public override IQueryable<District> Apply(IQueryable<District> query)
            {
                return query.OrderBy(entity => entity.Name);
            }
        }

        #endregion OrderBy Routine
    }
}