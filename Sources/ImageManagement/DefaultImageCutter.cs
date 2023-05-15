namespace Pingvinius.ImageManagement
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public sealed class DefaultImageCutter : IImageModifier
    {
        #region Properties

        public Rectangle Rect { get; set; }

        #endregion Properties

        public DefaultImageCutter(Rectangle rect)
        {
            this.Rect = rect;
        }

        #region IImageModifier Members

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public Image Modify(Image image)
        {
            if (!this.Rect.IsEmpty)
            {
                Image newImage = new Bitmap(this.Rect.Width, this.Rect.Height);
                using (Graphics context = Graphics.FromImage(newImage))
                {
                    context.PageUnit = GraphicsUnit.Pixel;

                    context.SmoothingMode = SmoothingMode.HighQuality;
                    context.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    context.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    context.DrawImage(image, new Rectangle(0, 0, this.Rect.Width, this.Rect.Height), this.Rect, GraphicsUnit.Pixel);
                    context.Flush();
                }
                return newImage;
            }

            return null;
        }

        #endregion IImageModifier Members
    }
}