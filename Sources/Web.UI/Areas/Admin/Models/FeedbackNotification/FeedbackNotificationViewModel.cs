namespace ClubbyBook.Web.UI.Areas.Admin.Models.FeedbackNotification
{
    using System;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Base;

    public sealed class FeedbackNotificationViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.FeedbackNotification>
    {
        public FeedbackNotificationUserViewModel User { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Message { get; set; }

        public bool IsNotificationNew { get; set; }
    }
}