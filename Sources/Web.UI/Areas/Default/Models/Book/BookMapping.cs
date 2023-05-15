namespace ClubbyBook.Web.UI.Areas.Default.Models.Book
{
    using System;
    using ClubbyBook.Web.UI.Mapping;
    using ClubbyBook.Web.UI.Utilities;

    internal static class BookMapping
    {
        public sealed class EntityToViewModel : EntityToViewModelMapping<ClubbyBook.Data.Models.Book, BookViewModel>
        {
            protected override BookViewModel MapSourceToTarget(ClubbyBook.Data.Models.Book entity, BookViewModel viewModel)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                viewModel.Title = entity.Title;
                viewModel.OriginalTitle = entity.OriginalTitle;
                viewModel.AuthorList = new BookAuthorListViewModel(entity);
                viewModel.GenreList = new BookGenreListViewModel(entity);
                viewModel.Collection = entity.Collection;
                viewModel.Description = entity.Description;
                viewModel.ContainsInOtherUserLibraryFromSameCity = true; // TODO: should be dynamic
                viewModel.ContainsInUserLibrary = true; // TODO: should be dynamic
                viewModel.Image = UIHelper.BuildImageViewModel(entity, false);
                viewModel.UrlRewrite = entity.UrlRewrite;

                return base.MapSourceToTarget(entity, viewModel);
            }
        }
    }
}