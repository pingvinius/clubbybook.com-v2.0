namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class CountrySelectCriteria
    {
        #region Where Routine

        public sealed class DefaultCountry : BaseWhereSelectCriteria<Country>
        {
            private const int DefaultCountryId = 1;

            public override IQueryable<Country> Apply(IQueryable<Country> query)
            {
                return query.Where(entity => entity.Id == DefaultCountry.DefaultCountryId);
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class DefaultSort : BaseOrderBySelectCriteria<Country>
        {
            public override IQueryable<Country> Apply(IQueryable<Country> query)
            {
                return query.OrderBy(entity => entity.Name);
            }
        }

        #endregion OrderBy Routine
    }
}