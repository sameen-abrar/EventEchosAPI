using System.Drawing.Drawing2D;
using System.Drawing;

namespace EventEchosAPI.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap Downscale(this Bitmap originalBitmap, int newWidth, int newHeight)
        {
            var resizedBitmap = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(resizedBitmap))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
            }
            return resizedBitmap;
        }
    }
}
