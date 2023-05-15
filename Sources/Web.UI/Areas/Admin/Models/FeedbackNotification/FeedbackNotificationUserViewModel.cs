namespace ClubbyBook.Web.UI.Areas.Admin.Models.FeedbackNotification
{
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class FeedbackNotificationUserViewModel : ViewModel
    {
        #region Properties

        public bool IsAnonymous
        {
            get
            {
                return this.User == null;
            }
        }

        public string UserName
        {
            get
            {
                return this.IsAnonymous ? UIHelper.AnonymousProfileFullNameString : UIHelper.GetUserFullName(this.User, false);
            }
        }

        public int UserId
        {
            get
            {
                return this.IsAnonymous ? -1 : this.User.Id;
            }
        }

        public int ProfileId
        {
            get
            {
                return this.IsAnonymous ? -1 : this.User.Profile.Id;
            }
        }

        private User User { get; set; }

        #endregion

        public FeedbackNotificationUserViewModel(User user)
        {
            this.User = user;
        }
    }
}