using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DavidSimmons.Core.Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Resizes an image
        /// borrowed from: http://www.codeproject.com/Articles/191424/Resizing-an-Image-On-The-Fly-using-NET
        /// </summary>
        /// <param name="imageToResize"></param>
        /// <param name="newSize"></param>
        /// <param name="preserveAspectRatio"></param>
        /// <returns>returns the resized image Object</returns>
        public static Image ResizeImage(this Image imageToResize, Size newSize, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = imageToResize.Width;
                int originalHeight = imageToResize.Height;
                float percentWidth = (float)newSize.Width / (float)originalWidth;
                float percentHeight = (float)newSize.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = newSize.Width;
                newHeight = newSize.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(imageToResize, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }

    public class Size
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
