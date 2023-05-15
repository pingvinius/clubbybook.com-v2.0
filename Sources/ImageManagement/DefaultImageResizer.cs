namespace Pingvinius.ImageManagement
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public sealed class DefaultImageResizer : IImageModifier
    {
        #region Properties

        public Size Size { get; set; }

        public bool OnlyByHeight { get; set; }

        public bool OnlyByWidth { get; set; }

        #endregion Properties

        public DefaultImageResizer(Size size)
            : this(size, false, false)
        {
        }

        public DefaultImageResizer(Size size, bool onlyByHeight, bool onlyByWidth)
        {
            this.Size = size;
            this.OnlyByHeight = onlyByHeight;
            this.OnlyByWidth = onlyByWidth;
        }

        #region IImageModifier Members

        public Image Modify(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (!this.Size.IsEmpty)
            {
                Size destSize = new Size(this.Size.Width, this.Size.Height);

                if (this.OnlyByHeight)
                {
                    float aspectRatio = (float)image.Width / (float)image.Height;
                    destSize = new Size((int)(aspectRatio * this.Size.Height), this.Size.Height);
                }

                if (this.OnlyByWidth)
                {
                    float aspectRatio = (float)image.Height / (float)image.Width;
                    destSize = new Size(this.Size.Width, (int)(aspectRatio * this.Size.Width));
                }

                Image newImage = new Bitmap(destSize.Width, destSize.Height);
                using (Graphics context = Graphics.FromImage(newImage))
                {
                    context.PageUnit = GraphicsUnit.Pixel;

                    context.SmoothingMode = SmoothingMode.HighQuality;
                    context.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    context.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    context.DrawImage(image, new Rectangle(0, 0, newImage.Width, newImage.Height));
                    context.Flush();
                }
                return newImage;
            }

            return null;
        }

        #endregion IImageModifier Members
    }
}