namespace ClubbyBook.Web.UI.Areas.Admin.Models.FeedbackNotification
{
    using System;
    using ClubbyBook.Web.UI.Mapping;

    internal static class FeedbackNotificationMapping
    {
        public sealed class EntityToViewModel : EntityToViewModelMapping<ClubbyBook.Data.Models.FeedbackNotification, FeedbackNotificationViewModel>
        {
            protected override FeedbackNotificationViewModel MapSourceToTarget(ClubbyBook.Data.Models.FeedbackNotification entity,
                FeedbackNotificationViewModel viewModel)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                viewModel.User = entity != null ? new FeedbackNotificationUserViewModel(entity.User) : null;
                viewModel.CreatedDate = entity != null ? entity.Notification.CreatedDate : DateTime.MinValue;
                viewModel.Message = entity != null ? entity.Notification.Message : string.Empty;
                viewModel.IsNotificationNew = entity != null ? entity.IsNew : false;

                return base.MapSourceToTarget(entity, viewModel);
            }
        }

        public sealed class ViewModelToEntity : ViewModelToEntityMapping<FeedbackNotificationViewModel, ClubbyBook.Data.Models.FeedbackNotification>
        {
            protected override ClubbyBook.Data.Models.FeedbackNotification MapSourceToTarget(FeedbackNotificationViewModel viewModel,
                ClubbyBook.Data.Models.FeedbackNotification entity)
            {
                throw new NotSupportedException("Converting from FeedbackNotificationViewModel into FeedbackNotification is not supported.");
            }
        }
    }
}