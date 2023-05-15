namespace Pingvinius.ImageManagement
{
    using System.Drawing;

    public interface IImageModifier
    {
        Image Modify(Image image);
    }
}