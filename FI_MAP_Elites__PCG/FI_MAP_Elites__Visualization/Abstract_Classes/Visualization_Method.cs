using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Shared_Elements
{
    public abstract class Visualization_Method<T>:I_Deep_Copyable
        where T:Data_Structure
    {
        public abstract SKBitmap Generate_Bitmap(
            T individual
            );

        public abstract void Draw_On_Bitmap(
            SKBitmap image,
            T individual
            );

        public abstract int Q__Image_Width(T data_structure);
        public abstract int Q__Image_Height(T data_structure);
        public abstract object Q__Deep_Copy();
    }
}
