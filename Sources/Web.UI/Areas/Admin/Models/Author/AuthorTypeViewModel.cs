namespace ClubbyBook.Web.UI.Areas.Admin.Models.Author
{
    using System.Collections.Generic;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class AuthorTypeViewModel : EnumViewModel<AuthorType>
    {
        private static readonly Dictionary<AuthorType, string> displayNames = new Dictionary<AuthorType, string>()
        {
            { AuthorType.NotSpecified, UIHelper.NotSpecifiedString },
            { AuthorType.Person, "Писатель" },
            { AuthorType.PublishingHouse, "Издательство" }
        };

        protected override Dictionary<AuthorType, string> DisplayNames
        {
            get { return AuthorTypeViewModel.displayNames; }
        }

        public AuthorTypeViewModel()
            : this(AuthorType.NotSpecified)
        {
        }

        public AuthorTypeViewModel(AuthorType selected)
            : base(selected)
        {
        }
    }
}