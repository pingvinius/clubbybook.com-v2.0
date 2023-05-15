namespace ClubbyBook.Web.UI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Web.Mvc;
    using ClubbyBook.Common;
    using ClubbyBook.Web.UI.Controllers.Base;
    using ClubbyBook.Web.UI.Models;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Utilities;
    using Pingvinius.ImageManagement;

    [AccessPermission(RoleNames.Account, RoleNames.Editor, RoleNames.Admin)]
    public sealed class UploaderController : ClubbyBookController
    {
        [HttpPost]
        public JsonResult UploadImage(UploadImageViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            this.Logger.Info("UploadImage is started ({0}, {1}) - ({2}, {3}).", viewModel.X1, viewModel.Y1, viewModel.X2, viewModel.Y2);

            Image finalImage = null;

            try
            {
                // Convert bytes into image
                byte[] bitmapData = new byte[viewModel.ImageData.Length];
                bitmapData = Convert.FromBase64String(UploaderController.FixBase64ForImage(viewModel.ImageData));
                using (MemoryStream streamBitmap = new MemoryStream(bitmapData))
                {
                    using (var image = Image.FromStream(streamBitmap))
                    {
                        // Cut image correspond with specified selected area
                        var modifiers = new List<IImageModifier>();
                        modifiers.Add(new DefaultImageCutter(new Rectangle(viewModel.X1, viewModel.Y1, viewModel.Width, viewModel.Height)));
                        modifiers.Add(new DefaultImageResizer(new Size(192, 300)));
                        if (viewModel.ApplyDecorator)
                        {
                            modifiers.Add(new DefaultImageDecorator("ClubbyBook.com"));
                        }

                        finalImage = ImageHelper.Modify(image, modifiers);
                    }
                }

                // Prepare paths
                string newFileName = string.Format(Settings.Images.TempFileNameFormat, DateTimeHelper.Now.ToFileTime());
                string tempFilePath = Path.Combine(Settings.Images.TempPath, newFileName);

                // Save
                finalImage.Save(this.Server.MapPath(tempFilePath), ImageFormat.Png);

                // Log this action
                this.Logger.Info("UploadImage is finished. Saved temporary image path: {0}.", tempFilePath);

                // Return success result
                return this.CreateAjaxSuccessResponse(new { filePath = tempFilePath });
            }
            finally
            {
                if (finalImage != null)
                {
                    finalImage.Dispose();
                    finalImage = null;
                }
            }
        }

        private static string FixBase64ForImage(string imageData)
        {
            string imageDataWithoutHeader = imageData.Substring(imageData.IndexOf(',') + 1);
            StringBuilder sbText = new StringBuilder(imageDataWithoutHeader, imageDataWithoutHeader.Length);
            sbText.Replace("\r\n", string.Empty);
            sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }
    }
}