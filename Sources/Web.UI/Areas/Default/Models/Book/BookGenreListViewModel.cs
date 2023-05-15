namespace ClubbyBook.Web.UI.Areas.Default.Models.Book
{
    using System;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class BookGenreListViewModel : ViewModel
    {
        public Genre[] Genres { get; private set; }

        public BookGenreListViewModel(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            this.Genres = book.BookGenres.Select(bg => bg.Genre).ToArray();
        }
    }
}