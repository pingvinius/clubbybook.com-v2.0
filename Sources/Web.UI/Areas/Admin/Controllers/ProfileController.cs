namespace ClubbyBook.Web.UI.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Admin.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Profile;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public sealed class ProfileController : AdminEntityController<IProfileRepository, Profile, ProfileViewModel, ProfileViewModelList>
    {
        public ProfileController()
        {
            this.CurrentTabName = "tab-users";
        }

        [HttpGet]
        [AccessPermission(RoleNames.Admin)]
        public override ActionResult Edit(int? id = null)
        {
            return base.Edit(id);
        }

        [HttpPost]
        [ValidateInput(false)]
        [AccessPermission(RoleNames.Admin)]
        public override ActionResult Edit(ProfileViewModel viewModel)
        {
            return base.Edit(viewModel);
        }

        [HttpPost]
        [AccessPermission(RoleNames.Admin)]
        public override ActionResult Delete(int id)
        {
            throw new NotSupportedException();
        }

        protected override IList<ISelectCriteria<Profile>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new ProfileSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new ProfileSelectCriteria.RegistrationDateSort());
            return selectCriteria;
        }

        protected override void ExecuteCustomLogicOnPostEdit(ProfileViewModel viewModel, Profile entity)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            base.ExecuteCustomLogicOnPostEdit(viewModel, entity);

            // Assign new roles
            RepositoryFactory.Get<IUserRepository>().SetUserRoles(entity.User, viewModel.Roles.SelectedRoles);

            // Apply new profile photo image
            if (!string.IsNullOrWhiteSpace(viewModel.Image.NewTempImageUrl))
            {
                string profilePhotoFileName = string.Format(Settings.Images.ProfileAvatarFileNameFormat, entity.Id);
                string profilePhotoPath = Path.Combine(Settings.Images.ProfilePath, profilePhotoFileName);

                try
                {
                    // Copy new image from temp to images path
                    System.IO.File.Copy(this.Server.MapPath(viewModel.Image.NewTempImageUrl), this.Server.MapPath(profilePhotoPath), true);

                    // Set new image path
                    entity.ImagePath = profilePhotoPath;

                    // Save changes for entity
                    this.Repository.SaveChanges(entity);

                    // Remove temp image
                    System.IO.File.Delete(this.Server.MapPath(viewModel.Image.NewTempImageUrl));
                }
                catch (Exception ex)
                {
                    this.Logger.Error(string.Format("An error has occurred while copying new profile photo image. Details: {0}.", ex.ToString()));
                }
            }
        }
    }
}