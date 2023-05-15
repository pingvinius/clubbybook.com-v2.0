namespace ClubbyBook.Web.UI.Models
{
    using System;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Mvc.Models;
    using Pingvinius.Framework.Repositories;

    public sealed class CityViewModel : ViewModel
    {
        private City city;

        public int? CountryId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        public string DisplayShortString
        {
            get
            {
                return UIHelper.GetCityName(this.city, false);
            }
        }

        public string DisplayString
        {
            get
            {
                return UIHelper.GetCityName(this.city, true);
            }
        }

        public CityViewModel()
            : this(null, false)
        {
        }

        public CityViewModel(City city, bool useDefaultCountry = true)
        {
            this.city = city;

            if (this.city != null)
            {
                this.CountryId = this.city.CountryId;
                this.DistrictId = this.city.DistrictId;
                this.CityId = this.city.Id;
            }
            else
            {
                this.CountryId = useDefaultCountry ? RepositoryFactory.Get<ICountryRepository>().GetDefaultCountry().Id : new Nullable<int>();
                this.DistrictId = new Nullable<int>();
                this.CityId = new Nullable<int>();
            }
        }
    }
}