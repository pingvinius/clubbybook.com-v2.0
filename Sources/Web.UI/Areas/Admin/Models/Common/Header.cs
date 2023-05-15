namespace ClubbyBook.Web.UI.Areas.Admin.Models.Common
{
    using System.Collections.Generic;

    public sealed class Header
    {
        public string Title { get; set; }

        public bool ShowSmallPart { get; set; }

        public string SmallPartTitle { get; set; }

        public IList<HeaderButton> Buttons { get; private set; }

        public Header()
        {
            this.Buttons = new List<HeaderButton>();
        }
    }
}