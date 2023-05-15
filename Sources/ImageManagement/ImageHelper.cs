namespace Pingvinius.ImageManagement
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;

    public static class ImageHelper
    {
        #region Modify Routine

        public static Image Modify(string imageFilePath, IImageModifier modifier)
        {
            return ImageHelper.Modify(imageFilePath, new List<IImageModifier>() { modifier });
        }

        public static Image Modify(string imageFilePath, IList<IImageModifier> modifiers)
        {
            if (string.IsNullOrWhiteSpace(imageFilePath))
            {
                throw new ArgumentNullException("imageFilePath");
            }

            if (!File.Exists(imageFilePath))
            {
                throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, "The file \"{0}\" isn't found.", imageFilePath));
            }

            using (Image image = Image.FromFile(imageFilePath))
            {
                return ImageHelper.Modify(image, modifiers);
            }
        }

        public static Image Modify(Image image, IImageModifier modifier)
        {
            return ImageHelper.Modify(image, new List<IImageModifier>() { modifier });
        }

        public static Image Modify(Image image, IList<IImageModifier> modifiers)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (modifiers == null)
            {
                throw new ArgumentNullException("modifiers");
            }

            if (modifiers.Count > 0)
            {
                Image currentImage = image;
                foreach (var modifier in modifiers)
                {
                    var nextImage = modifier.Modify(currentImage);
                    if (nextImage != null)
                    {
                        if (!object.ReferenceEquals(image, currentImage))
                        {
                            currentImage.Dispose();
                        }

                        currentImage = nextImage;
                    }
                }
                return currentImage;
            }

            return null;
        }

        #endregion Modify Routine

        #region Modify and Save Routine

        public static bool ModifyAndSaveAsPng(string imageFilePath, string imageNewFilePath, IImageModifier modifier)
        {
            return ImageHelper.ModifyAndSaveAsPng(imageFilePath, imageNewFilePath, new List<IImageModifier>() { modifier });
        }

        public static bool ModifyAndSaveAsPng(string imageFilePath, string imageNewFilePath, IList<IImageModifier> modifiers)
        {
            if (string.IsNullOrWhiteSpace(imageNewFilePath))
            {
                throw new ArgumentNullException("imageNewFilePath");
            }

            if (File.Exists(imageNewFilePath))
            {
                File.Delete(imageNewFilePath);
            }

            using (var newImage = Modify(imageFilePath, modifiers))
            {
                if (newImage != null)
                {
                    newImage.Save(imageNewFilePath, ImageFormat.Png);
                    return true;
                }
            }

            return false;
        }

        #endregion Modify and Save Routine
    }
}