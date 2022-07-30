using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public static class Archive_Utilities
    {

        /// <summary>
        /// Tested: [OK]
        /// </summary>
        /// <param name="index"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Vec2i Index__To__Coordinates_2D(
            int index,
            int width,
            int height
            )
        {
            int size = width * height;

            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }

            int x = index % width;
            int y = index / width;

            return new Vec2i(x, y);
        }



        /// <summary>
        /// Tested: [OK]
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int Coordinates_2D__To__Index(
            Vec2i coords,
            int width,
            int height
            )
        {
            if (
                coords.x < 0 ||
                coords.x >= width ||
                coords.y < 0 ||
                coords.y >= height
                )
            {
                throw new IndexOutOfRangeException();
            }

            int index = coords.y * width + coords.x;

            return index;
        }

        /// <summary>
        /// Tested: [OK]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int Coordinates_2D__To__Index(
            int x,
            int y,
            int width,
            int height
            )
        {
            if (
                x < 0 ||
                x >= width ||
                y < 0 ||
                y >= height
                )
            {
                throw new IndexOutOfRangeException();
            }

            int index = y * width + x;

            return index;
        }


        public static List<int> Index__To__Coordinates_ND(
            int index,
            int[] num_subdivisions__per__dimension
            )
        {
            int size = num_subdivisions__per__dimension.Q__Product();

            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException();
            }

            List<int> coordinates = new List<int>();
            int num_dimensions = num_subdivisions__per__dimension.Length;

            //for (int dim = 0; dim < num_dimensions; dim++)
            //{
            //    coordinates.Add(0);
            //}

            for (int dim_id = 0; dim_id < num_dimensions; dim_id++)
            {
                if (dim_id == 0)
                {
                    int dim_size = num_subdivisions__per__dimension[dim_id];
                    coordinates.Add(index % dim_size);
                }
                else
                {
                    int multiplier = num_subdivisions__per__dimension.Q__Product(dim_id - 1);
                    coordinates.Add(
                        (index / multiplier) % num_subdivisions__per__dimension[dim_id]
                    );
                }
            }

            return coordinates;
        }

        public static int Coordinates_ND__To__Index(
            List<int> coords,
            int[] num_subdivisions__per__dimension
            )
        {
            int num_dimensions = num_subdivisions__per__dimension.Length;

            if (coords.Count != num_dimensions)
            {
                throw new Exception("coordinates' count does not match number of dimensions");
            }

            for (int i = 0; i < coords.Count; i++)
            {
                int coord = coords[i];
                if (coord < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                if (coord >= num_subdivisions__per__dimension[i])
                {
                    throw new IndexOutOfRangeException();
                }
            }

            int index = 0;

            for (int dim = 0; dim < num_dimensions; dim++)
            {
                int multiplier = num_subdivisions__per__dimension.Q__Product(dim - 1);
                index += coords[dim] * multiplier;
            }

            return index;
        }

        public static int Coordinates_ND__To__Index(
            int[] coords,
            int[] num_subdivisions__per__dimension
            )
        {
            int num_dimensions = num_subdivisions__per__dimension.Length;

            if (coords.Length != num_dimensions)
            {
                throw new Exception("coordinates' count does not match number of dimensions");
            }

            for (int i = 0; i < coords.Length; i++)
            {
                int coord = coords[i];
                if (coord < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                if (coord >= num_subdivisions__per__dimension[i])
                {
                    throw new IndexOutOfRangeException();
                }
            }

            int index = 0;

            for (int dim = 0; dim < num_dimensions; dim++)
            {
                int multiplier = num_subdivisions__per__dimension.Q__Product(dim - 1);
                index += coords[dim] * multiplier;
            }

            return index;
        }

    }
}
