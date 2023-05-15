namespace ClubbyBook.Web.UI.Areas.Default.Models.Reader
{
    using System;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Web.UI.Mapping;
    using ClubbyBook.Web.UI.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Repositories;

    internal static class ReaderMapping
    {
        public sealed class EntityToViewModel : EntityToViewModelMapping<ClubbyBook.Data.Models.Profile, ReaderViewModel>
        {
            protected override ReaderViewModel MapSourceToTarget(ClubbyBook.Data.Models.Profile entity, ReaderViewModel viewModel)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                if (viewModel == null)
                {
                    throw new ArgumentNullException("viewModel");
                }

                // Profile
                viewModel.Name = entity.Name;
                viewModel.Surname = entity.Surname;
                viewModel.DisplayName = UIHelper.GetProfileFullName(entity, true);
                viewModel.Nickname = entity.Nickname;
                viewModel.Gender = new GenderViewModel(entity.Gender);
                viewModel.City = new CityViewModel(entity.City);
                viewModel.UrlRewrite = entity.UrlRewrite;
                viewModel.Image = UIHelper.BuildImageViewModel(entity, false);

                // User
                viewModel.UserId = entity.User.Id;
                viewModel.CreatedDate = entity.User.CreatedDate;

                // User books
                viewModel.Books = RepositoryFactory.Get<IBookRepository>().GetUserBooks(entity.User).Select(ub => new ReaderBookViewModel(ub)).ToList();
                viewModel.TotalBookCount = viewModel.Books.Count;
                viewModel.TotalReadBookCount = viewModel.Books.Where(b => b.Status == Data.Models.UserBookStatuses.AlreadyRead).Count();

                return base.MapSourceToTarget(entity, viewModel);
            }
        }
    }
}