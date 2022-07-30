//using UnityEngine;
using System.Collections;
using System;

namespace Common_Tools
{
    /// <summary>
    /// A 2D recctangle, 
    /// defined by the minimum x and y coordinates,
    /// as well as its width and height.
    /// </summary>
    public struct Rect2d
    {
        public static readonly Rect2d zero = new Rect2d(0, 0, 0, 0);

        public double x;
        public double y;
        public double width;
        public double height;

        // json constructor
        public Rect2d(double x, double y, double width, double height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public double Min_X
        {
            get
            {
                return x;
            }
        }

        public double Max_X
        {
            get
            {
                return x + width;
            }
        }

        public double Min_Y
        {
            get
            {
                return y;
            }
        }

        public double Max_Y
        {
            get
            {
                return y + height;
            }
        }

        public Vec2d Min_Coords
        {
            get
            {
                return new Vec2d(Min_X, Min_Y);
            }
        }

        public Vec2d Max_Coords
        {
            get
            {
                return new Vec2d(Max_X, Max_Y);
            }
        }

        public double Q__Perimeter()
        {
            return (double)width * 2.0 + (double)height * 2;
        }

        public double Q__Diagonal()
        {
            double w2 = width * width;
            double h2 = height * height;
            return Math.Sqrt(w2 + h2);
        }
    }
}
