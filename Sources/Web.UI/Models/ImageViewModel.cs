namespace ClubbyBook.Web.UI.Models
{
    using Pingvinius.Framework.Mvc.Models;

    public sealed class ImageViewModel : ViewModel
    {
        public string ImageUrl { get; set; }

        public string Alt { get; set; }

        public string Title { get; set; }

        public string LinkImageUrl { get; set; }

        public string NewTempImageUrl { get; set; }

        public bool UseBig { get; set; }

        public bool ApplyDecorator { get; set; }

        public bool WrapInLink
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.LinkImageUrl);
            }
        }

        public ImageViewModel()
            : this(string.Empty, true, string.Empty, string.Empty, string.Empty)
        {
        }

        public ImageViewModel(string imageUrl, bool useBig, string linkImageUrl = "", string alt = "", string title = "", bool applyDecorator = true)
        {
            this.ImageUrl = imageUrl;
            this.UseBig = useBig;
            this.LinkImageUrl = linkImageUrl;
            this.Alt = alt;
            this.Title = title;
            this.ApplyDecorator = applyDecorator;
            this.NewTempImageUrl = string.Empty;
        }
    }
}