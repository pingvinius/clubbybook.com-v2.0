namespace ClubbyBook.Web.UI.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Admin.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Book;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public sealed class BookController : AdminEntityController<IBookRepository, Book, BookViewModel, BookViewModelList>
    {
        public BookController()
        {
            this.CurrentTabName = "tab-books";
        }

        protected override IList<ISelectCriteria<Book>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new BookSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new BookSelectCriteria.TitleSort());
            return selectCriteria;
        }

        protected override void ExecuteCustomLogicOnPostEdit(BookViewModel viewModel, Book entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            base.ExecuteCustomLogicOnPostEdit(viewModel, entity);

            // Modify or add genre
            this.Repository.SetBookGenres(entity, viewModel.Genre.SelectedGenre);

            // Modify or add author list
            this.Repository.SetBookAuthors(entity, viewModel.AuthorList.SelectedAuthorList);

            // Apply new book cover image
            if (!string.IsNullOrWhiteSpace(viewModel.Image.NewTempImageUrl))
            {
                string bookCoverFileName = string.Format(Settings.Images.BookCoverFileNameFormat, entity.Id);
                string bookCoverPath = Path.Combine(Settings.Images.BookPath, bookCoverFileName);

                try
                {
                    // Copy new image from temp to images path
                    System.IO.File.Copy(this.Server.MapPath(viewModel.Image.NewTempImageUrl), this.Server.MapPath(bookCoverPath), true);

                    // Set new image path
                    entity.CoverPath = bookCoverPath;

                    // Save changes for entity
                    this.Repository.SaveChanges(entity);

                    // Remove temp image
                    System.IO.File.Delete(this.Server.MapPath(viewModel.Image.NewTempImageUrl));
                }
                catch (Exception ex)
                {
                    this.Logger.Error(string.Format("An error has occurred while copying new book cover image. Details: {0}.", ex.ToString()));
                }
            }
        }
    }
}