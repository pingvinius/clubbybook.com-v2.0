namespace ClubbyBook.Web.UI.Areas.Default.Models.Author
{
    using System;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class AuthorTypeViewModel : ViewModel
    {
        public AuthorType Type { get; private set; }

        public AuthorTypeViewModel(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            this.Type = author.Type;
        }
    }
}