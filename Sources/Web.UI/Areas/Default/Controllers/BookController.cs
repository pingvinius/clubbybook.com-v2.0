namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System.Collections.Generic;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Default.Models.Book;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public sealed class BookController : DefaultEntityController<IBookRepository, Book, BookViewModel, BookViewModelList>
    {
        protected override IList<ISelectCriteria<Book>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new BookSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new BookSelectCriteria.Confirmed());
            selectCriteria.Add(new BookSelectCriteria.TitleSort());
            return selectCriteria;
        }
    }
}