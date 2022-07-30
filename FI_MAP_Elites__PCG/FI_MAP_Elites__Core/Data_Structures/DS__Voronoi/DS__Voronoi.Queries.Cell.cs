using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public partial class DS__Voronoi : Data_Structure
    {
        public List<Vec2d> Q__Cell__Boundary_Points__Copied(int cell)
        {
            return perimeter_points__per__cell[cell].Q__Deep_Copy();
        }

        public List<Vec2d> Q__Cell__Boundary_Points(int cell)
        {
            return perimeter_points__per__cell[cell];
        }

        public List<Line_Segment> Q__Cell__Boundary_Lines(int cell)
        {
            return perimeter_lines__per__cell[cell];
        }

        public List<Line_Segment> Q__Cell__Boundary_Lines__Copied(int cell)
        {
            return perimeter_lines__per__cell[cell].Q__Deep_Copy();
        }

        public List<Vec2d> Q__Boundary_Points__Copied(int cell_id)
        {
            return perimeter_points__per__cell[cell_id].Q__Deep_Copy();
        }

        /// <summary>
        /// Returns the square root of the cell's are.
        /// This would correspond to the size (side-length) of a square that has the same area.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public double Q__Cell_Size(int cell)
        {
            return Math.Sqrt(area__per__cell[cell]);
        }

        public double Q__Cell_Area(int cell)
        {
            return area__per__cell[cell];
        }

        public double Q__Cell_Perimeter(int cell)
        {
            if (Q__Cell__Is_Active(cell))
            {
                double perimeter = 0.0;
                foreach (var line in perimeter_lines__per__cell[cell])
                {
                    perimeter += line.Q__Magnitude();
                }
                return perimeter;
            }
            else
            {
                return double.NaN;
            }
        }

        public double Q__Cell_Compactness(int cell)
        {
            double area = Q__Cell_Area(cell);
            double perimeter = Q__Cell_Perimeter(cell);
            double compactness = 4.0 * Math.PI * area / (perimeter * perimeter);
            return compactness;
        }

        public bool Q__Cell__Is_Active(int cell)
        {
            return is_active__per__cell[cell];
        }
    }
}
