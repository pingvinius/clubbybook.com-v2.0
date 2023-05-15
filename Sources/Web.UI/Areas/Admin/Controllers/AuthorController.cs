namespace ClubbyBook.Web.UI.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Admin.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Author;
    using Pingvinius.Framework.Mvc.Controllers;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public sealed class AuthorController : AdminEntityController<IAuthorRepository, Author, AuthorViewModel, AuthorViewModelList>, IAutoCompleteReady
    {
        public AuthorController()
        {
            this.CurrentTabName = "tab-authors";
        }

        protected override IList<ISelectCriteria<Author>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new AuthorSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new AuthorSelectCriteria.FullNameSort());
            return selectCriteria;
        }

        protected override void ExecuteCustomLogicOnPostEdit(AuthorViewModel viewModel, Author entity)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            base.ExecuteCustomLogicOnPostEdit(viewModel, entity);

            // Apply new author photo image
            if (!string.IsNullOrWhiteSpace(viewModel.Image.NewTempImageUrl))
            {
                string authorPhotoFileName = string.Format(Settings.Images.AuthorPhotoFileNameFormat, entity.Id);
                string authorPhotoPath = Path.Combine(Settings.Images.AuthorPath, authorPhotoFileName);

                try
                {
                    // Copy new image from temp to images path
                    System.IO.File.Copy(this.Server.MapPath(viewModel.Image.NewTempImageUrl), this.Server.MapPath(authorPhotoPath), true);

                    // Set new image path
                    entity.PhotoPath = authorPhotoPath;

                    // Save changes for entity
                    this.Repository.SaveChanges(entity);

                    // Remove temp image
                    System.IO.File.Delete(this.Server.MapPath(viewModel.Image.NewTempImageUrl));
                }
                catch (Exception ex)
                {
                    this.Logger.Error(string.Format("An error has occurred while copying new author photo image. Details: {0}.", ex.ToString()));
                }
            }
        }

        #region IAutoCompleteReady Members

        [HttpPost]
        public ActionResult GetAutoCompleteList(string searchKey)
        {
            // Prepare select criteria
            List<ISelectCriteria<Author>> selectCriteria = new List<ISelectCriteria<Author>>();
            selectCriteria.Add(new AuthorSelectCriteria.AutoComplete(searchKey));
            selectCriteria.Add(new AuthorSelectCriteria.FullNameSort());

            // Get result list from database
            var resultList = this.Repository.Select(selectCriteria);

            // Generate JSON result array
            var jsonData = resultList.Select(author => new
            {
                id = author.Id.ToString(),
                name = author.FullName
            }).ToArray();

            // Return JSON response
            return Json(jsonData, AuthorController.DefaultJsonContentType, JsonRequestBehavior.DenyGet);
        }

        #endregion IAutoCompleteReady Members
    }
}