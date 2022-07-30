using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;
using System.IO;

namespace FI_MAP_Elites__PCG.Visualization
{
    public static class Image_Extensions
    {
        public static void Save_To_Disk(this SKBitmap bitmap, string file_path)
        {
            using (Stream s = File.OpenWrite(file_path))
            {
                SKData d = SKImage.FromBitmap(bitmap).Encode(SKEncodedImageFormat.Png, 100);
                d.SaveTo(s);
            }
        }
    }
}
