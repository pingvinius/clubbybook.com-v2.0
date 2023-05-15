namespace ClubbyBook.Web.UI.Areas.Admin.Models.Author
{
    using System;
    using ClubbyBook.Web.UI.Mapping;
    using ClubbyBook.Web.UI.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Utilities;

    internal static class AuthorMapping
    {
        public sealed class EntityToViewModel : EntityToViewModelMapping<ClubbyBook.Data.Models.Author, AuthorViewModel>
        {
            protected override AuthorViewModel MapSourceToTarget(ClubbyBook.Data.Models.Author entity, AuthorViewModel viewModel)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                viewModel.FullName = entity != null ? entity.FullName : string.Empty;
                viewModel.ShortDescription = entity != null ? entity.ShortDescription : string.Empty;
                viewModel.Biography = entity != null ? entity.Biography : string.Empty;
                viewModel.BirthdayYear = entity != null && entity.BirthdayYear.HasValue ? entity.BirthdayYear.Value.ToString() : string.Empty;
                viewModel.DeathYear = entity != null && entity.DeathYear.HasValue ? entity.DeathYear.Value.ToString() : string.Empty;
                viewModel.Type = new AuthorTypeViewModel(entity != null ? entity.Type : Data.Models.AuthorType.NotSpecified);
                viewModel.Gender = new GenderViewModel(entity != null ? entity.Gender : Data.Models.Gender.NotSpecified);
                viewModel.Image = UIHelper.BuildImageViewModel(entity, true);
                viewModel.CreatedDate = entity != null ? entity.CreatedDate : DateTimeHelper.Now;
                viewModel.LastModifiedDate = entity != null ? entity.LastModifiedDate : DateTimeHelper.Now;
                viewModel.UrlRewrite = entity != null ? entity.UrlRewrite : string.Empty;
                viewModel.LifeYearsString = UIHelper.GetAuthorLifeYearsString(entity);

                return base.MapSourceToTarget(entity, viewModel);
            }
        }

        public sealed class ViewModelToEntity : ViewModelToEntityMapping<AuthorViewModel, ClubbyBook.Data.Models.Author>
        {
            protected override ClubbyBook.Data.Models.Author MapSourceToTarget(AuthorViewModel viewModel, ClubbyBook.Data.Models.Author entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                entity.FullName = viewModel.FullName;
                entity.ShortDescription = viewModel.ShortDescription;
                entity.Biography = viewModel.Biography;
                entity.BirthdayYear = ParsingHelper.ToNullableInt(viewModel.BirthdayYear);
                entity.DeathYear = ParsingHelper.ToNullableInt(viewModel.DeathYear);
                entity.Type = viewModel.Type.Selected;
                entity.Gender = viewModel.Gender.Selected;

                // entity.PhotoPath will be changed by ExecuteCustomLogicOnPostEdit in controller

                return base.MapSourceToTarget(viewModel, entity);
            }
        }
    }
}