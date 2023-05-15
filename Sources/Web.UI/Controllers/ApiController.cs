namespace ClubbyBook.Web.UI.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Controllers.Base;
    using ClubbyBook.Web.UI.Exceptions;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Repositories.SelectCriteria;
    using Pingvinius.Framework.Security;

    [AccessPermission(RoleNames.Account, RoleNames.Editor, RoleNames.Admin)]
    public sealed class ApiController : ClubbyBookController
    {
        #region Country/District/City Routine

        [HttpGet]
        public JsonResult GetCountries()
        {
            int? selectedCountryId = this.GetIntParameter("selectedCountryId");
            if (!selectedCountryId.HasValue)
            {
                selectedCountryId = RepositoryFactory.Get<ICountryRepository>().GetDefaultCountry().Id;
            }

            var entities = RepositoryFactory.Get<ICountryRepository>().Select(new CountrySelectCriteria.DefaultSort().ToList());

            return this.CreateAjaxSuccessResponseForEntityList<Country>(entities, delegate(Country country)
            {
                return new SelectListItem()
                {
                    Selected = country.Id.Equals(selectedCountryId),
                    Text = country.Name,
                    Value = country.Id.ToString()
                };
            });
        }

        [HttpGet]
        public JsonResult GetDistricts()
        {
            int? selectedCountryId = this.GetIntParameter("selectedCountryId");
            int? selectedDistrictId = this.GetIntParameter("selectedDistrictId");

            if (!selectedCountryId.HasValue)
            {
                return this.CreateAjaxSuccessResponseForEntityList<District>(null);
            }

            var entities = RepositoryFactory.Get<IDistrictRepository>().Select(new List<ISelectCriteria<District>>()
            {
                new DistrictSelectCriteria.Country(selectedCountryId.Value),
                new DistrictSelectCriteria.DefaultSort()
            });

            return this.CreateAjaxSuccessResponseForEntityList<District>(entities, delegate(District district)
            {
                return new SelectListItem()
                {
                    Selected = district.Id.Equals(selectedDistrictId),
                    Text = district.Name,
                    Value = district.Id.ToString()
                };
            });
        }

        [HttpGet]
        public JsonResult GetCities()
        {
            int? selectedCountryId = this.GetIntParameter("selectedCountryId");
            int? selectedDistrictId = this.GetIntParameter("selectedDistrictId");
            int? selectedCityId = this.GetIntParameter("selectedCityId");

            if (!selectedCountryId.HasValue || !selectedDistrictId.HasValue)
            {
                return this.CreateAjaxSuccessResponseForEntityList<City>(null);
            }

            var entities = RepositoryFactory.Get<ICityRepository>().Select(new List<ISelectCriteria<City>>()
            {
                new CitySelectCriteria.Country(selectedCountryId.Value),
                new CitySelectCriteria.District(selectedDistrictId.Value),
                new CitySelectCriteria.DefaultSort()
            });

            return this.CreateAjaxSuccessResponseForEntityList<City>(entities, delegate(City city)
            {
                return new SelectListItem()
                {
                    Selected = city.Id.Equals(selectedCityId),
                    Text = city.Name,
                    Value = city.Id.ToString()
                };
            });
        }

        #endregion

        #region Feedback Routine

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SendFeedback(string message)
        {
            var repository = RepositoryFactory.Get<IFeedbackNotificationRepository>();
            var feedback = repository.CreateInstance();
            feedback.Notification.Message = message;
            if (AccessManager.IsAuthenticated)
            {
                feedback.OwnerUserId = AccessManager.CurrentIdentity.Id;
            }
            repository.SaveChanges(feedback);

            return this.CreateAjaxSuccessResponse();
        }

        [AccessPermission(RoleNames.Admin)]
        public JsonResult MarkFeedbackNotificationAsRead(int id)
        {
            var repository = RepositoryFactory.Get<IFeedbackNotificationRepository>();

            // Try to find feedback
            var feedback = repository.Get(id);
            if (feedback == null)
            {
                throw new EntityIsNotFoundException(id);
            }

            // Mark as read
            feedback.IsNew = false;
            repository.SaveChanges(feedback);

            // Return success response
            return this.CreateAjaxSuccessResponse();
        }

        #endregion
    }
}