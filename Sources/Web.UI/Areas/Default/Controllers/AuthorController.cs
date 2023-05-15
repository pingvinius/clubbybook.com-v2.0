namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System.Collections.Generic;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Default.Models.Author;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public sealed class AuthorController : DefaultEntityController<IAuthorRepository, Author, AuthorViewModel, AuthorViewModelList>
    {
        protected override IList<ISelectCriteria<Author>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new AuthorSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new AuthorSelectCriteria.FullNameSort());
            return selectCriteria;
        }
    }
}