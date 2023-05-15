namespace ClubbyBook.Common
{
    using System.Configuration;

    public static class Settings
    {
        public static string EntityFrameworkConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["EntityFrameworkConnection"].ConnectionString;
            }
        }

        public static bool EnableMinification
        {
            get
            {
                return Settings.GetAppSettingBoolValue("EnableMinification", true);
            }
        }

        public static class Images
        {
            #region Profile Avatar Routine

            public static string ProfileAvatarFileNameFormat
            {
                get { return "avatar-{0}.png"; }
            }

            public static string EmptyProfileFileName
            {
                get { return "avatar-empty.png"; }
            }

            public static string AnonymousProfileFileName
            {
                get { return "avatar-anonymous.png"; }
            }

            public static string ProfilePath
            {
                get { return "/content/profile-images/"; }
            }

            #endregion Profile Avatar Routine

            #region Book Cover Routine

            public static string BookCoverFileNameFormat
            {
                get { return "Cover{0}.png"; }
            }

            public static string EmptyBookFileName
            {
                get { return "cover-empty.png"; }
            }

            public static string BookPath
            {
                get { return "/content/book-images/"; }
            }

            #endregion Book Cover Routine

            #region Author Cover Routine

            public static string AuthorPhotoFileNameFormat
            {
                get { return "Photo{0}.png"; }
            }

            public static string EmptyAuthorFileName
            {
                get { return "photo-empty.png"; }
            }

            public static string AuthorPath
            {
                get { return "/content/author-images/"; }
            }

            #endregion Author Cover Routine

            #region Temp Routine

            public static string TempFileNameFormat
            {
                get
                {
                    return "temp-{0}.png";
                }
            }

            public static string TempPath
            {
                get { return "/content/temp-images/"; }
            }

            #endregion Temp Routine
        }

        #region OLD CODE

        /*
        public static string SiteMapFilePath
        {
            get
            {
                return "~/sitemap.xml";
            }
        }

        #region Mail Templates

        public static string MailTemplatesPath
        {
            get
            {
                return "~/MailTemplates/";
            }
        }

        public static string RegistrationTemplateFileName
        {
            get
            {
                return "Registration.bcmt";
            }
        }

        public static string ConversationNotificationTemplateFileName
        {
            get
            {
                return "ConversationNotification.bcmt";
            }
        }

        public static string FeedbackNotificationTemplateFileName
        {
            get
            {
                return "FeedbackNotification.bcmt";
            }
        }

        public static string AddBookSystemNotificationTemplateFileName
        {
            get
            {
                return "AddBookSystemNotification.bcmt";
            }
        }

        public static string PasswordWasChangedTemplateFileName
        {
            get
            {
                return "PasswordWasChanged.bcmt";
            }
        }

        public static string ResetPasswordTemplateFileName
        {
            get
            {
                return "ResetPassword.bcmt";
            }
        }

        #endregion Mail Templates

        */

        #endregion OLD CODE

        #region Private Routine

        private static bool GetAppSettingBoolValue(string key, bool defaultValue)
        {
            string stringValue = Settings.GetAppSettingValue(key);
            bool boolValue = false;
            if (!bool.TryParse(stringValue, out boolValue))
            {
                boolValue = defaultValue;
            }
            return boolValue;
        }

        private static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #endregion Private Routine
    }
}