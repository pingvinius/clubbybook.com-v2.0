namespace ClubbyBook.Web.UI.Areas.Admin.Models.Profile
{
    using System;
    using ClubbyBook.Web.UI.Mapping;
    using ClubbyBook.Web.UI.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Utilities;

    internal static class ProfileMapping
    {
        public sealed class EntityToViewModel : EntityToViewModelMapping<ClubbyBook.Data.Models.Profile, ProfileViewModel>
        {
            protected override ProfileViewModel MapSourceToTarget(ClubbyBook.Data.Models.Profile entity, ProfileViewModel viewModel)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                // Profile
                viewModel.Name = entity != null ? entity.Name : string.Empty;
                viewModel.Surname = entity != null ? entity.Surname : string.Empty;
                viewModel.Nickname = entity != null ? entity.Nickname : string.Empty;
                viewModel.FullName = entity != null ? UIHelper.GetProfileFullName(entity, true) : string.Empty;
                viewModel.Birthday = entity != null ? entity.Birthday : DateTimeHelper.Now;
                viewModel.Gender = new GenderViewModel(entity != null ? entity.Gender : Data.Models.Gender.NotSpecified);
                viewModel.City = new CityViewModel(entity != null ? entity.City : null);
                viewModel.UrlRewrite = entity != null ? entity.UrlRewrite : string.Empty;
                viewModel.Image = UIHelper.BuildImageViewModel(entity, true);

                // User
                viewModel.Email = entity != null ? entity.User.EMail : string.Empty;
                viewModel.CreatedDate = entity != null ? entity.User.CreatedDate : DateTimeHelper.Now;
                viewModel.LastAccessDate = entity != null ? entity.User.LastAccessDate : DateTimeHelper.Now;
                viewModel.Roles = new ProfileRolesViewModel(entity);

                return base.MapSourceToTarget(entity, viewModel);
            }
        }

        public sealed class ViewModelToEntity : ViewModelToEntityMapping<ProfileViewModel, ClubbyBook.Data.Models.Profile>
        {
            protected override ClubbyBook.Data.Models.Profile MapSourceToTarget(ProfileViewModel viewModel, ClubbyBook.Data.Models.Profile entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                // Profile
                entity.Name = viewModel.Name;
                entity.Surname = viewModel.Surname;
                entity.Nickname = viewModel.Nickname;
                entity.Birthday = viewModel.Birthday;
                entity.Gender = viewModel.Gender.Selected;
                entity.CityId = viewModel.City.CityId;

                // entity.ImagePath will be changed by ExecuteCustomLogicOnPostEdit in controller

                // User
                entity.User.EMail = viewModel.Email;

                return base.MapSourceToTarget(viewModel, entity);
            }
        }
    }
}