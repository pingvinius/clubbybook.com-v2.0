namespace ClubbyBook.Web.UI.Mapping
{
    using ClubbyBook.Data.Models;
    using AdminModels = ClubbyBook.Web.UI.Areas.Admin.Models;
    using DefaultModels = ClubbyBook.Web.UI.Areas.Default.Models;

    public sealed class ClubbyBookMapper : Pingvinius.Framework.Mapping.BaseMapper
    {
        public ClubbyBookMapper()
        {
            this.BindAdminMapping();
            this.BindDefaultMapping();
        }

        private void BindAdminMapping()
        {
            // Author
            this.Bind<Author, AdminModels.Author.AuthorViewModel>(new AdminModels.Author.AuthorMapping.EntityToViewModel());
            this.Bind<AdminModels.Author.AuthorViewModel, Author>(new AdminModels.Author.AuthorMapping.ViewModelToEntity());

            // Book
            this.Bind<Book, AdminModels.Book.BookViewModel>(new AdminModels.Book.BookMapping.EntityToViewModel());
            this.Bind<AdminModels.Book.BookViewModel, Book>(new AdminModels.Book.BookMapping.ViewModelToEntity());

            // News
            this.Bind<News, AdminModels.News.NewsViewModel>(new AdminModels.News.NewsMapping.EntityToViewModel());
            this.Bind<AdminModels.News.NewsViewModel, News>(new AdminModels.News.NewsMapping.ViewModelToEntity());

            // Profile
            this.Bind<Profile, AdminModels.Profile.ProfileViewModel>(new AdminModels.Profile.ProfileMapping.EntityToViewModel());
            this.Bind<AdminModels.Profile.ProfileViewModel, Profile>(new AdminModels.Profile.ProfileMapping.ViewModelToEntity());

            // FeedbackNotification
            this.Bind<FeedbackNotification, AdminModels.FeedbackNotification.FeedbackNotificationViewModel>(
                new AdminModels.FeedbackNotification.FeedbackNotificationMapping.EntityToViewModel());
            this.Bind<AdminModels.FeedbackNotification.FeedbackNotificationViewModel, FeedbackNotification>(
                new AdminModels.FeedbackNotification.FeedbackNotificationMapping.ViewModelToEntity());
        }

        private void BindDefaultMapping()
        {
            // News
            this.Bind<News, DefaultModels.News.NewsViewModel>(new DefaultModels.News.NewsMapping.EntityToViewModel());

            // Profile
            this.Bind<Profile, DefaultModels.Reader.ReaderViewModel>(new DefaultModels.Reader.ReaderMapping.EntityToViewModel());

            // Book
            this.Bind<Book, DefaultModels.Book.BookViewModel>(new DefaultModels.Book.BookMapping.EntityToViewModel());

            // Author
            this.Bind<Author, DefaultModels.Author.AuthorViewModel>(new DefaultModels.Author.AuthorMapping.EntityToViewModel());
        }
    }
}