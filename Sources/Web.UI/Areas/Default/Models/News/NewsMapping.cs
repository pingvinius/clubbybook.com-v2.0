namespace ClubbyBook.Web.UI.Areas.Default.Models.News
{
    using System;
    using ClubbyBook.Web.UI.Mapping;

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

                viewModel.Title = entity.Title;
                viewModel.Message = entity.Message;
                viewModel.CreatedDate = entity.CreatedDate;
                viewModel.LastModifiedDate = entity.LastModifiedDate;
                viewModel.UrlRewrite = entity.UrlRewrite;

                return base.MapSourceToTarget(entity, viewModel);
            }
        }
    }
}