namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Default.Models.Reader;
    using ClubbyBook.Web.UI.Exceptions;
    using ClubbyBook.Web.UI.Extensions;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Repositories.SelectCriteria;
    using Pingvinius.Framework.Security;

    public sealed class ReaderController : DefaultEntityController<IProfileRepository, Profile, ReaderViewModel, ReaderViewModelList>
    {
        [HttpGet]
        [AccessPermission(RoleNames.Account, RoleNames.Editor, RoleNames.Admin)]
        public ActionResult Edit(string urlRewrite)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentException("The URL rewrite string is not specified.");
            }

            // Get entity
            var entity = this.Repository.Get(urlRewrite);
            if (entity == null)
            {
                throw new EntityIsNotFoundException(urlRewrite);
            }

            // Check permissions
            if (entity.UserId != AccessManager.CurrentIdentity.Id)
            {
                throw new AccessDenyException();
            }

            // Convert entity to view model
            ReaderViewModel viewModel = this.Convert(entity);

            // Render view
            return this.View(viewModel);
        }

        [HttpPost]
        [AccessPermission(RoleNames.Account, RoleNames.Editor, RoleNames.Admin)]
        public ActionResult Edit(ReaderViewModel viewModel)
        {
            // Validate input parameters
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            // Validate model
            if (!this.ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Get entity to modify
            var entity = this.Repository.Get(viewModel.Id);
            if (entity == null)
            {
                throw new EntityIsNotFoundException(viewModel.Id);
            }

            // Check permissions
            if (entity.UserId != AccessManager.CurrentIdentity.Id)
            {
                throw new AccessDenyException();
            }

            // Fill up properties
            entity.CityId = viewModel.City.CityId;
            entity.Gender = viewModel.Gender.Selected;
            entity.ImagePath = viewModel.Image.ImageUrl;
            entity.Name = viewModel.Name;
            entity.Nickname = viewModel.Nickname;
            entity.Surname = viewModel.Surname;

            // Save changes
            this.Repository.SaveChanges(entity);

            // Redirect to details page for this entity.
            return this.Redirect(this.Url.GoTo(DefaultUrlTemplates.ViewReader, entity.UrlRewrite));
        }

        [HttpGet]
        [AccessPermission(RoleNames.Account, RoleNames.Editor, RoleNames.Admin)]
        public ActionResult EditAccount(string urlRewrite)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentException("The URL rewrite string is not specified.");
            }

            // Get entity
            var entity = this.Repository.Get(urlRewrite);
            if (entity == null)
            {
                throw new EntityIsNotFoundException(urlRewrite);
            }

            // Check permissions
            if (entity.UserId != AccessManager.CurrentIdentity.Id)
            {
                throw new AccessDenyException();
            }

            // Convert entity to view model
            ReaderViewModel viewModel = this.Convert(entity);

            // Render view
            return this.View(viewModel);
        }

        protected override IList<ISelectCriteria<Profile>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new ProfileSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new ProfileSelectCriteria.NotDeleted());
            selectCriteria.Add(new ProfileSelectCriteria.RegistrationDateSort());
            return selectCriteria;
        }
    }
}