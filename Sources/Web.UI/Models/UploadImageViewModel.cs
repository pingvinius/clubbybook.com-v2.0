namespace ClubbyBook.Web.UI.Models
{
    using Pingvinius.Framework.Mvc.Models;

    public sealed class UploadImageViewModel : ViewModel
    {
        public string ImageData { get; set; }

        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }

        public bool ApplyDecorator { get; set; }

        public int Width
        {
            get
            {
                return this.X2 - this.X1;
            }
        }

        public int Height
        {
            get
            {
                return this.Y2 - this.Y1;
            }
        }
    }
}