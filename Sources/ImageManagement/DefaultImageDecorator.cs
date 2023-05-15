namespace Pingvinius.ImageManagement
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public sealed class DefaultImageDecorator : IImageModifier
    {
        #region Properties

        public string Text { get; set; }

        public Color BrushColor { get; set; }

        public int TransparentLevel { get; set; }

        public string FontName { get; set; }

        public int FontSize { get; set; }

        public bool AutoFontSize { get; set; }

        public int Angle { get; set; }

        #endregion Properties

        public DefaultImageDecorator(string text)
        {
            this.Text = text;
            this.FontName = "Arial";
            this.FontSize = 14;
            this.AutoFontSize = true;
            this.BrushColor = Color.White;
            this.TransparentLevel = 90;
            this.Angle = 45;
        }

        #region IImageModifier Members

        public Image Modify(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (!string.IsNullOrEmpty(this.Text))
            {
                Image newImage = new Bitmap(image);

                float fontSize = (float)this.FontSize;
                GraphicsUnit fontGraphicsUnit = GraphicsUnit.Point;
                if (this.AutoFontSize)
                {
                    fontSize = (float)image.Height * (float)0.1;
                    fontGraphicsUnit = GraphicsUnit.Pixel;
                }

                using (Font font = new Font(this.FontName, fontSize, FontStyle.Bold, fontGraphicsUnit))
                {
                    using (Brush brush = new SolidBrush(Color.FromArgb(this.TransparentLevel, this.BrushColor)))
                    {
                        using (Graphics context = Graphics.FromImage(newImage))
                        {
                            context.PageUnit = GraphicsUnit.Pixel;

                            context.SmoothingMode = SmoothingMode.AntiAlias;
                            context.TranslateTransform(image.Width / 2, image.Height / 2);
                            context.RotateTransform(this.Angle);
                            SizeF sz = context.MeasureString(this.Text, font);
                            context.DrawString(this.Text, font, brush, -(sz.Width / 2), -(sz.Height / 2));
                            context.ResetTransform();
                            context.Flush();
                        }
                    }
                }

                return newImage;
            }

            return null;
        }

        #endregion IImageModifier Members
    }
}