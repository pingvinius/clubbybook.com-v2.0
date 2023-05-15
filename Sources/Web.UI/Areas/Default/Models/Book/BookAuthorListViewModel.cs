namespace ClubbyBook.Web.UI.Areas.Default.Models.Book
{
    using System;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class BookAuthorListViewModel : ViewModel
    {
        public Author[] Authors { get; private set; }

        public BookAuthorListViewModel(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            this.Authors = book.BookAuthors.Select(ba => ba.Author).ToArray();
        }
    }
}