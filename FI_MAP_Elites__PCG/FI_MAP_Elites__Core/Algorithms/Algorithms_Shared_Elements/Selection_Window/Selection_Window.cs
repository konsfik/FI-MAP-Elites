using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.Shared_Elements
{
    public class Selection_Window : I_Deep_Copyable
    {
        public int num_dimensions;
        public int[] min_coords; // minimum coordinates
        public int[] max_coords; // maximum coordinates

        public Selection_Window(
            int num_dimensions,
            int[] center_coordinates,
            int half_size
            )
        {
            this.num_dimensions = num_dimensions;
            this.min_coords = new int[num_dimensions];
            this.max_coords = new int[num_dimensions];
            for (int i = 0; i < num_dimensions; i++)
            {
                min_coords[i] = center_coordinates[i] - half_size;
                max_coords[i] = center_coordinates[i] + half_size;
            }
        }

        public Selection_Window(Selection_Window window_to_copy)
        {
            this.num_dimensions = window_to_copy.num_dimensions;
            this.min_coords = window_to_copy.min_coords.Q__Deep_Copy();
            this.max_coords = window_to_copy.max_coords.Q__Deep_Copy();
        }


        public object Q__Deep_Copy()
        {
            return new Selection_Window(this);
        }

        public int Q__Num_Dimensions()
        {
            return num_dimensions;
        }

        public int Q__Dim_Size(int dim_index)
        {
            int min = min_coords[dim_index];
            int max = max_coords[dim_index];
            return max - min;
        }

        public bool Q__Contains_Coordinates(int[] coords)
        {
            for (int d = 0; d < num_dimensions; d++)
            {
                if (coords[d] < min_coords[d])
                    return false;
                if (coords[d] > max_coords[d])
                    return false;
            }
            return true;
        }

        public int[] Q__Center_Coords()
        {
            int[] center_coords = new int[num_dimensions];

            for (int d = 0; d < num_dimensions; d++)
            {
                int min = min_coords[d];
                int w = max_coords[d] - min_coords[d];
                int half_w = (w / 2);

                center_coords[d] = min + half_w;
            }

            return center_coords;
        }

        public int[] Q__Min_Coords()
        {
            return min_coords.Q__Deep_Copy();
        }

        public int[] Q__Max_Coords()
        {
            return max_coords.Q__Deep_Copy();
        }

        public int Q__Min_X()
        {
            return min_coords[0];
        }

        public int Q__Max_X()
        {
            return max_coords[0];
        }

        public int Q__Min_Y()
        {
            return min_coords[1];
        }

        public int Q__Max_Y()
        {
            return max_coords[1];
        }

        public int Q__Cen_X()
        {
            int min_x = Q__Min_X();
            int width = Q__Width();
            int half_w = (width / 2);
            int cen_x = min_x + half_w;
            return cen_x;
        }

        public int Q__Cen_Y()
        {
            int min_y = Q__Min_Y();
            int width = Q__Width();
            int half_w = (width / 2);
            int cen_y = min_y + half_w;
            return cen_y;
        }

        public int Q__Width()
        {
            return (Q__Max_X() - Q__Min_X()) + 1;
        }

        public int Q__Height()
        {
            return (Q__Max_Y() - Q__Min_Y()) + 1;
        }

        /// <summary>
        /// Returns the list of coordinates that are contained in the left trinagle of this selection window.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<int[]> Q__Left_Triangle_Coords()
        {
            if (num_dimensions != 2) throw new Exception("only applicable in 2D");

            int min_x = Q__Min_X();
            int cen_x = Q__Cen_X();
            int max_x = Q__Max_X();

            int min_y = Q__Min_Y();
            int cen_y = Q__Cen_Y();
            int max_y = Q__Max_Y();

            List<int[]> coords = new List<int[]>();

            for (int x = min_x; x <= cen_x; x++)
            {
                for (int y = min_y; y <= max_y; y++)
                {
                    int y_comp = (cen_y - Math.Abs(y - cen_y)) - min_y;
                    int xx = x - min_x;

                    if (xx <= y_comp)
                    {
                        coords.Add(new int[] { x, y });
                    }
                }
            }

            return coords;
        }

        public List<int[]> Q__Right_Triangle_Coords()
        {
            if (num_dimensions != 2) throw new Exception("only applicable in 2D");

            int min_x = Q__Min_X();
            int cen_x = Q__Cen_X();
            int max_x = Q__Max_X();

            int min_y = Q__Min_Y();
            int cen_y = Q__Cen_Y();
            int max_y = Q__Max_Y();

            List<int[]> coords = new List<int[]>();

            for (int x = cen_x; x <= max_x; x++)
            {
                for (int y = min_y; y <= max_y; y++)
                {
                    int y_comp = (cen_y + Math.Abs(y - cen_y)) - min_y;
                    int x_dif = x - min_x;

                    if (x_dif >= y_comp)
                    {
                        coords.Add(new int[] { x, y });
                    }
                }
            }

            return coords;
        }

        public List<int[]> Q__Down_Triangle_Coords()
        {
            if (num_dimensions != 2) throw new Exception("only applicable in 2D");

            int min_x = Q__Min_X();
            int cen_x = Q__Cen_X();
            int max_x = Q__Max_X();

            int min_y = Q__Min_Y();
            int cen_y = Q__Cen_Y();
            int max_y = Q__Max_Y();

            List<int[]> coords = new List<int[]>();

            for (int x = min_x; x <= max_x; x++)
            {
                for (int y = min_y; y <= cen_y; y++)
                {
                    int x_comp = (cen_x - Math.Abs(x - cen_x)) - min_x;
                    int yy = y - min_y;

                    if (yy <= x_comp)
                    {
                        coords.Add(new int[] { x, y });
                    }
                }
            }

            return coords;
        }

        public List<int[]> Q__Up_Triangle_Coords()
        {
            if (num_dimensions != 2) throw new Exception("only applicable in 2D");

            int min_x = Q__Min_X();
            int cen_x = Q__Cen_X();
            int max_x = Q__Max_X();

            int min_y = Q__Min_Y();
            int cen_y = Q__Cen_Y();
            int max_y = Q__Max_Y();

            List<int[]> coords = new List<int[]>();

            for (int x = min_x; x <= max_x; x++)
            {
                for (int y = cen_y; y <= max_y; y++)
                {
                    int x_comp = (cen_x + Math.Abs(x - cen_x)) - min_x;
                    int yy = y - min_y;

                    if (yy >= x_comp)
                    {
                        coords.Add(new int[] { x, y });
                    }
                }
            }

            return coords;
        }

    }
}
