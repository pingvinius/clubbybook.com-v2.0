namespace ClubbyBook.Web.UI.Areas.Admin.Models.News
{
    using System;
    using ClubbyBook.Web.UI.Mapping;
    using Pingvinius.Framework.Utilities;

    internal static class NewsMapping
    {
        public sealed class EntityToViewModel : EntityToViewModelMapping<ClubbyBook.Data.Models.News, NewsViewModel>
        {
            protected override NewsViewModel MapSourceToTarget(ClubbyBook.Data.Models.News entity, NewsViewModel viewModel)
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
                viewModel.Message = entity != null ? entity.Message : string.Empty;
                viewModel.CreatedDate = entity != null ? entity.CreatedDate : DateTimeHelper.Now;
                viewModel.LastModifiedDate = entity != null ? entity.LastModifiedDate : DateTimeHelper.Now;
                viewModel.UrlRewrite = entity != null ? entity.UrlRewrite : string.Empty;

                return base.MapSourceToTarget(entity, viewModel);
            }
        }

        public sealed class ViewModelToEntity : ViewModelToEntityMapping<NewsViewModel, ClubbyBook.Data.Models.News>
        {
            protected override ClubbyBook.Data.Models.News MapSourceToTarget(NewsViewModel viewModel, ClubbyBook.Data.Models.News entity)
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
                entity.Message = viewModel.Message;

                return base.MapSourceToTarget(viewModel, entity);
            }
        }
    }
}