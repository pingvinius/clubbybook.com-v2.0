namespace ClubbyBook.Web.UI.Areas.Default.Models.Common
{
  using System.Collections.Generic;

    public sealed class Header
    {
        public string Title { get; set; }

        public bool UseSmallTitle { get; set; }

        public IList<RightLink> RightLinks { get; set; }

        public Header()
        {
            this.Title = string.Empty;
            this.UseSmallTitle = false;
            this.RightLinks = new List<RightLink>();
        }

        public sealed class RightLink
        {
            public string Title { get; set; }

            public string Link { get; set; }

            public RightLink(string title, string link)
            {
                this.Title = title;
                this.Link = link;
            }
        }
    }
}