using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Evolvable_Geometry : Data_Structure
    {

        public double Q__Plan__Prescribed_Total_Area()
        {
            double area_sum = 0.0;
            foreach (var kvp in prescription.area__per__space_unit)
            {
                area_sum += kvp.Value;
            }
            return area_sum;
        }

        public double Q__Plan__Total_Area()
        {
            List<int> existing_space_units = Q__Existing_Space_Units();

            double plan_area = 0.0;

            foreach (int space_unit in existing_space_units)
                plan_area += Q__Space_Unit__Area(space_unit);

            return plan_area;
        }

        public List<int> Q__Plan__Used_Cells()
        {
            List<int> used_cells = new List<int>();
            foreach (var kvp in space_unit__per__cell)
            {
                if (kvp.Value != -1)
                    used_cells.Add(kvp.Key);
            }
            return used_cells;
        }

        /// <summary>
        /// Returns the total length of the exterior perimeter of the plan.
        /// I.e. the total length of all the exterior walls.
        /// </summary>
        /// <returns></returns>
        public double Q__Plan__Exterior_Perimeter_Length()
        {
            List<Line_Segment> perimeter_lines = Q__Plan__Exterior_Wall_Lines__Unordered();

            double perimeter_length = 0.0;

            foreach (var line in perimeter_lines)
            {
                perimeter_length += line.Q__Magnitude();
            }

            return perimeter_length;
        }

        /// <summary>
        /// Returns all the unique lines of the plan.
        /// </summary>
        /// <returns></returns>
        public List<Line_Segment> Q__Plan__Unique_Wall_Lines__Unordered()
        {
            List<int> existing_space_units = Q__Existing_Space_Units();

            HashSet<Line_Segment> lines_hash_set = new HashSet<Line_Segment>();
            foreach (var space_unit in existing_space_units)
            {
                var unit_perimeter_lines = Q__Space_Unit__Perimeter_Lines__Unordered(space_unit);
                foreach (var line in unit_perimeter_lines)
                {
                    lines_hash_set.Add(line);
                }
            }

            return lines_hash_set.ToList();
        }

        /// <summary>
        /// Returns all the exterior walls as unordered line segments.
        /// </summary>
        /// <returns></returns>
        public List<Line_Segment> Q__Plan__Exterior_Wall_Lines__Unordered()
        {
            List<int> existing_space_units = Q__Existing_Space_Units();

            List<Line_Segment> space_units_perimeter_lines = new List<Line_Segment>();
            foreach (var space_unit in existing_space_units)
            {
                space_units_perimeter_lines.AddRange(
                    Q__Space_Unit__Perimeter_Lines__Unordered(space_unit)
                    );
            }

            List<Line_Segment> lines_to_remove = new List<Line_Segment>();
            int num_lines = space_units_perimeter_lines.Count;
            for (int i = 0; i < num_lines - 1; i++)
            {
                for (int j = i + 1; j < num_lines; j++)
                {
                    Line_Segment line_1 = space_units_perimeter_lines[i];
                    Line_Segment line_2 = space_units_perimeter_lines[j];

                    if (line_1 == line_2)
                    {
                        lines_to_remove.Add(line_1);
                        break;
                    }
                }
            }

            space_units_perimeter_lines.RemoveAll(
                x =>
                lines_to_remove.Contains(x)
                );

            return space_units_perimeter_lines;
        }
    }
}
