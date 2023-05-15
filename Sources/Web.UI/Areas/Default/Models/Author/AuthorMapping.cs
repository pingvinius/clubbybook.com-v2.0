namespace ClubbyBook.Web.UI.Areas.Default.Models.Author
{
    using System;
    using ClubbyBook.Web.UI.Mapping;
    using ClubbyBook.Web.UI.Models;
    using ClubbyBook.Web.UI.Utilities;

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

                viewModel.FullName = entity.FullName;
                viewModel.LifeYears = UIHelper.GetAuthorLifeYearsString(entity);
                viewModel.Type = new AuthorTypeViewModel(entity);
                viewModel.Gender = new GenderViewModel(entity.Gender);
                viewModel.ShortDescription = entity.ShortDescription;
                viewModel.Biography = entity.Biography;
                viewModel.TotalBookCount = entity.BookAuthors.Count;
                viewModel.Image = UIHelper.BuildImageViewModel(entity, false);
                viewModel.UrlRewrite = entity.UrlRewrite;

                return base.MapSourceToTarget(entity, viewModel);
            }
        }
    }
}