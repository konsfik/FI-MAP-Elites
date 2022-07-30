using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public struct RGB_Color
    {
        public static readonly RGB_Color white = new RGB_Color(255, 255, 255);
        public static readonly RGB_Color black = new RGB_Color(0, 0, 0);

        public static readonly RGB_Color red = new RGB_Color(255, 0, 0);
        public static readonly RGB_Color dark_red = new RGB_Color(127, 0, 0);
        public static readonly RGB_Color light_red = new RGB_Color(255, 127, 127);

        public static readonly RGB_Color orange = new RGB_Color(255, 127, 0);
        public static readonly RGB_Color dark_orange = new RGB_Color(127, 63, 0);
        public static readonly RGB_Color light_orange = new RGB_Color(255, 191, 127);

        public static readonly RGB_Color yellow = new RGB_Color(255, 255, 0);
        public static readonly RGB_Color dark_yellow = new RGB_Color(127, 127, 0);
        public static readonly RGB_Color light_yellow = new RGB_Color(255, 255, 127);

        public static readonly RGB_Color green = new RGB_Color(0, 255, 0);
        public static readonly RGB_Color dark_green = new RGB_Color(0, 127, 0);
        public static readonly RGB_Color light_green = new RGB_Color(127, 255, 127);

        public static readonly RGB_Color cyan = new RGB_Color(0, 255, 255);
        public static readonly RGB_Color dark_cyan = new RGB_Color(0, 127, 127);
        public static readonly RGB_Color light_cyan = new RGB_Color(127, 255, 255);

        public static readonly RGB_Color blue = new RGB_Color(0, 0, 255);
        public static readonly RGB_Color dark_blue = new RGB_Color(0, 0, 127);
        public static readonly RGB_Color light_blue = new RGB_Color(127, 127, 255);

        public static readonly RGB_Color purple = new RGB_Color(127, 0, 255);
        public static readonly RGB_Color dark_purple = new RGB_Color(63, 0, 127);
        public static readonly RGB_Color light_purple = new RGB_Color(191, 127, 255);

        public static readonly RGB_Color magenta = new RGB_Color(255, 0, 255);
        public static readonly RGB_Color dark_magenta = new RGB_Color(127, 0, 127);
        public static readonly RGB_Color light_magenta = new RGB_Color(255, 127, 255);

        public static readonly RGB_Color pink = new RGB_Color(255, 160, 160);
        public static readonly RGB_Color yellow_green = new RGB_Color(127, 255, 0);

        public int r;
        public int g;
        public int b;

        public RGB_Color(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static List<RGB_Color> All_Colors() {
            List<RGB_Color> list = new List<RGB_Color>()
            {
                red, 
                orange,
                yellow,
                green,
                cyan,
                blue,
                purple,
                magenta,

                light_red,
                light_orange,
                light_yellow,
                light_green,
                light_cyan,
                light_blue,
                light_purple,
                light_magenta,

                dark_red,
                dark_orange,
                dark_yellow,
                dark_green,
                dark_cyan,
                dark_blue,
                dark_purple,
                dark_magenta
            };

            return list;
        }
    }
}
