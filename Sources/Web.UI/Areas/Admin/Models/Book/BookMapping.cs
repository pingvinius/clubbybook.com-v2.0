namespace ClubbyBook.Web.UI.Areas.Admin.Models.Book
{
    using System;
    using System.Linq;
    using ClubbyBook.Web.UI.Mapping;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Utilities;

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

                viewModel.Title = entity != null ? entity.Title : string.Empty;
                viewModel.OriginalTitle = entity != null ? entity.OriginalTitle : string.Empty;
                viewModel.Description = entity != null ? entity.Description : string.Empty;
                viewModel.Collection = entity != null ? entity.Collection : false;
                viewModel.Confirmed = entity != null ? entity.Confirmed : false;
                viewModel.AuthorList = new BookAuthorListViewModel(entity != null ? entity.BookAuthors : null);
                viewModel.Genre = new BookGenreViewModel(entity != null ? entity.BookGenres.FirstOrDefault() : null);
                viewModel.Image = UIHelper.BuildImageViewModel(entity, true);
                viewModel.CreatedDate = entity != null ? entity.CreatedDate : DateTimeHelper.Now;
                viewModel.LastModifiedDate = entity != null ? entity.LastModifiedDate : DateTimeHelper.Now;
                viewModel.UrlRewrite = entity != null ? entity.UrlRewrite : string.Empty;

                return base.MapSourceToTarget(entity, viewModel);
            }
        }

        public sealed class ViewModelToEntity : ViewModelToEntityMapping<BookViewModel, ClubbyBook.Data.Models.Book>
        {
            protected override ClubbyBook.Data.Models.Book MapSourceToTarget(BookViewModel viewModel, ClubbyBook.Data.Models.Book entity)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                entity.Title = viewModel.Title;
                entity.OriginalTitle = viewModel.OriginalTitle;
                entity.Description = viewModel.Description;
                entity.Collection = viewModel.Collection;
                entity.Confirmed = viewModel.Confirmed;

                // entity.BookGenres will be changed by ExecuteCustomLogicOnPostEdit in controller
                // entity.CoverPath will be changed by ExecuteCustomLogicOnPostEdit in controller
                // entity.BookAuthors will be changed by ExecuteCustomLogicOnPostEdit in controller

                return base.MapSourceToTarget(viewModel, entity);
            }
        }
    }
}