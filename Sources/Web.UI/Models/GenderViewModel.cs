namespace ClubbyBook.Web.UI.Models
{
    using System.Collections.Generic;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Utilities;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class GenderViewModel : EnumViewModel<Gender>
    {
        private static readonly Dictionary<Gender, string> displayNames = new Dictionary<Gender, string>()
        {
            { Gender.NotSpecified, UIHelper.NotSpecifiedString },
            { Gender.Male, "Мужской" },
            { Gender.Female, "Женский" }
        };

        protected override Dictionary<Gender, string> DisplayNames
        {
            get { return GenderViewModel.displayNames; }
        }

        public GenderViewModel()
            : this(Gender.NotSpecified)
        {
        }

        public GenderViewModel(Gender selected)
            : base(selected)
        {
        }
    }
}