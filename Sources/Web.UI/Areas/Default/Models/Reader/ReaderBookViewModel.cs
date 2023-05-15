namespace ClubbyBook.Web.UI.Areas.Default.Models.Reader
{
    using System;
    using System.Collections.Generic;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Mvc.Models;
    using Pingvinius.Framework.Security;

    public sealed class ReaderBookViewModel : ViewModel
    {
        private static Dictionary<UserBookStatuses, string> statusToStringForCurrentUser = new Dictionary<UserBookStatuses, string>()
        {
            { UserBookStatuses.AlreadyRead, "Читал(а)" },
            { UserBookStatuses.ReadingNow, "Читаю" },
            { UserBookStatuses.WantToRead, "Хочу прочесть" },
        };

        private static Dictionary<UserBookStatuses, string> statusToStringForOtherUser = new Dictionary<UserBookStatuses, string>()
        {
            { UserBookStatuses.AlreadyRead, "Читал(а)" },
            { UserBookStatuses.ReadingNow, "Читает" },
            { UserBookStatuses.WantToRead, "Хочет прочесть" },
        };

        public string Title { get; set; }

        public string StatusText { get; set; }

        public string UrlRewrite { get; set; }

        public UserBookStatuses Status { get; set; }

        public ReaderBookViewModel(UserBook userBook)
        {
            if (userBook == null)
            {
                throw new ArgumentNullException("userBook");
            }

            this.UrlRewrite = userBook.Book.UrlRewrite;
            this.Status = userBook.Status;
            this.Title = userBook.Book.Title;
            this.StatusText = string.Empty;
            if (userBook.Status != UserBookStatuses.None)
            {
                Dictionary<UserBookStatuses, string> strings = null;
                if (AccessManager.IsAuthenticated && AccessManager.CurrentIdentity.Id == userBook.UserId)
                {
                    strings = ReaderBookViewModel.statusToStringForCurrentUser;
                }
                else
                {
                    strings = ReaderBookViewModel.statusToStringForOtherUser;
                }

                string additionalText = strings[userBook.Status];
                if (!string.IsNullOrWhiteSpace(additionalText))
                {
                    this.StatusText += string.Format("{0}", additionalText);
                }
            }
        }
    }
}