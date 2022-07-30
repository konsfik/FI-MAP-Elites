using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Evolvable_Geometry : Data_Structure
    {

        /// <summary>
        /// Calculates the compactness of the entire plan.
        /// Compactness is calculated based on the plan's total area and total perimeter, 
        /// using the formula: 4 * Pi * plan_area / plan_perimeter^2
        /// 
        /// The formula has been taken from the following paper:
        /// reference:
        /// "Dissecting Visibility Graph Analysis:
        /// The metrics and their role in understanding workplace human behaviour"
        /// (Petros Koutsolampros, Kerstin Sailer, Tasos Varoudis, Rosie Haslem)
        /// </summary>
        /// <returns></returns>
        public double Q__BC__Compactness()
        {
            double plan_area = Q__Plan__Total_Area();
            double plan_perimeter = Q__Plan__Exterior_Perimeter_Length();
            double bc__compactness = 4.0 * Math.PI * plan_area / (plan_perimeter * plan_perimeter);
            return bc__compactness;
        }

        /// <summary>
        /// Iterates over the *existing* space units, 
        /// calculates the compactness of each one of them,
        /// and returns the average compactness.
        /// </summary>
        /// <returns></returns>
        public double Q__BC__Compactness_Per_Space_Unit()
        {
            List<int> existing_space_units = Q__Existing_Space_Units();
            double compactness_sum = 0.0;
            foreach (int space_unit in existing_space_units)
            {
                compactness_sum += Q__Space_Unit_Compactness(space_unit);
            }
            double bc__compactness_per_space_unit = compactness_sum / (double)existing_space_units.Count;
            return bc__compactness_per_space_unit;
        }

        public double Q__BC__Used_Cells_Compactness()
        {
            List<int> used_cells = Q__Plan__Used_Cells();

            double compactness_sum = 0.0;
            int num_used_cells = used_cells.Count;

            foreach (var cell in used_cells)
            {
                compactness_sum += voronoi_tessellation.Q__Cell_Compactness(cell);
            }

            double bc__used_cells_compactness = compactness_sum / (double)num_used_cells;
            return bc__used_cells_compactness;
        }

        public double Q__BC__Lines_Orthogonality()
        {
            List<int> prescribed_space_units = Q__Prescribed_Space_Units();

            // find all the perimeter lines of all rooms, and gather them together in a single list
            List<Line_Segment> all_perimeter_lines = new List<Line_Segment>();
            foreach (var space_unit in prescribed_space_units)
            {
                all_perimeter_lines.AddRange(
                    Q__Space_Unit__Perimeter_Lines__Unordered(space_unit)
                    );
            }

            // discard the multiple entries (but keep one of them)
            // and make a list of unique elements
            List<Line_Segment> unique_perimeter_lines = new List<Line_Segment>();
            foreach (var line in all_perimeter_lines)
            {
                if (unique_perimeter_lines.Contains(line) == false)
                {
                    unique_perimeter_lines.Add(line);
                }
            }


            double score_sum = 0.0;
            foreach (var line in unique_perimeter_lines)
            {
                Vec2d v = (line.p1 - line.p0);

                double line_angle = v.Q__Angle_To(new Vec2d(1, 0));

                double ang_score = 0;

                if (line_angle < 0)
                {
                    ang_score = 0;
                }
                else if (line_angle < Math.PI / 4.0)
                {
                    ang_score = line_angle.Q__Mapped(
                        0.0,
                        Math.PI / 4,
                        1.0,
                        0.0
                        );
                }
                else if (line_angle < Math.PI / 2.0)
                {
                    ang_score = line_angle.Q__Mapped(
                        Math.PI / 4,
                        Math.PI / 2,
                        0.0,
                        1.0
                        );
                }
                else if (line_angle < 3.0 * Math.PI / 4.0)
                {
                    ang_score = line_angle.Q__Mapped(
                        Math.PI / 2.0,
                        3.0 * Math.PI / 4.0,
                        1.0,
                        0.0
                        );
                }
                else if (line_angle <= Math.PI)
                {
                    ang_score = line_angle.Q__Mapped(
                        3.0 * Math.PI / 4.0,
                        Math.PI,
                        0.0,
                        1.0
                        );
                }
                else
                {
                    ang_score = 0;
                }

                score_sum += ang_score;
            }

            int num_lines = unique_perimeter_lines.Count;

            double bc__lines_orthogonality = score_sum / (double)num_lines;

            return bc__lines_orthogonality;
        }

        public double Q__BC__Lines_Orthogonality_Weighted()
        {
            List<int> prescribed_rooms = Q__Prescribed_Space_Units();

            // find all the perimeter lines of all rooms, and gather them together in a single list
            List<Line_Segment> all_perimeter_lines = new List<Line_Segment>();
            foreach (var room in prescribed_rooms)
            {
                all_perimeter_lines.AddRange(
                    Q__Space_Unit__Perimeter_Lines__Unordered(room)
                    );
            }

            // discard the multiple entries (but keep one of them)
            // and make a list of unique elements
            List<Line_Segment> unique_perimeter_lines = new List<Line_Segment>();
            foreach (var line in all_perimeter_lines)
            {
                if (unique_perimeter_lines.Contains(line) == false)
                {
                    unique_perimeter_lines.Add(line);
                }
            }

            double total_perimeter_length = 0;
            foreach (var line in unique_perimeter_lines)
            {
                total_perimeter_length += line.Q__Magnitude();
            }

            double score = 0.0;
            foreach (var line in unique_perimeter_lines)
            {
                Vec2d v = (line.p1 - line.p0);
                double line_angle = v.Q__Angle_To(new Vec2d(1, 0));

                double ang_score = 0;

                if (line_angle < 0)
                {
                    ang_score = 0;
                }
                else if (line_angle < Math.PI / 4.0)
                {
                    ang_score = line_angle.Q__Mapped(
                        0.0,
                        Math.PI / 4,
                        1.0,
                        0.0
                        );
                }
                else if (line_angle < Math.PI / 2.0)
                {
                    ang_score = line_angle.Q__Mapped(
                        Math.PI / 4,
                        Math.PI / 2,
                        0.0,
                        1.0
                        );
                }
                else if (line_angle < 3.0 * Math.PI / 4.0)
                {
                    ang_score = line_angle.Q__Mapped(
                        Math.PI / 2.0,
                        3.0 * Math.PI / 4.0,
                        1.0,
                        0.0
                        );
                }
                else if (line_angle <= Math.PI)
                {
                    ang_score = line_angle.Q__Mapped(
                        3.0 * Math.PI / 4.0,
                        Math.PI,
                        0.0,
                        1.0
                        );
                }
                else
                {
                    ang_score = 0;
                }

                double line_length = line.Q__Magnitude();
                double weight = line_length / total_perimeter_length;
                ang_score *= weight;

                score += ang_score;
            }

            double bc__lines_orthogonality_weighted = score;

            return bc__lines_orthogonality_weighted;
        }

        public double Q__BC__Angles_Orthogonality()
        {
            var plan_unique_lines = Q__Plan__Unique_Wall_Lines__Unordered();

            int num_lines = plan_unique_lines.Count;
            double orthogonality_sum = 0.0;
            int num_measurements = 0;
            for (int i1 = 0; i1 < num_lines - 1; i1++)
            {
                for (int i2 = i1 + 1; i2 < num_lines; i2++)
                {
                    Line_Segment line_1 = plan_unique_lines[i1];
                    Line_Segment line_2 = plan_unique_lines[i2];

                    if (line_1.Q__Is_Connected_To(line_2))
                    {
                        double orthogonality =
                            line_1
                            .Q__Orthogonality__Between_Connected_Lines(line_2);
                        orthogonality_sum += orthogonality;
                        num_measurements++;
                    }
                }
            }

            double bc__angles_orthogonality = orthogonality_sum / (double)num_measurements;

            return bc__angles_orthogonality;
        }

        public double Q__BC__Angles_Non_Acute()
        {
            var plan_unique_lines = Q__Plan__Unique_Wall_Lines__Unordered();

            int num_lines = plan_unique_lines.Count;
            double score_sum = 0.0;
            int num_measurements = 0;
            for (int i1 = 0; i1 < num_lines - 1; i1++)
            {
                for (int i2 = i1 + 1; i2 < num_lines; i2++)
                {
                    Line_Segment line_1 = plan_unique_lines[i1];
                    Line_Segment line_2 = plan_unique_lines[i2];

                    if (line_1.Q__Is_Connected_To(line_2))
                    {
                        double angle = line_1.Q__Angle__Between_Connected_Lines(line_2);
                        if (angle > Math.PI / 2.0)
                        {
                            score_sum += 1.0;
                            num_measurements++;
                        }
                        else
                        {
                            double score =
                                angle.Q__Mapped(
                                    0.0,
                                    Math.PI / 2.0,
                                    0.0,
                                    1.0
                                    );

                            score_sum += score;
                            num_measurements++;
                        }
                    }
                }
            }

            double bc__angles_non_acute = score_sum / (double)num_measurements;

            return bc__angles_non_acute;
        }

        public double Q__Eval__Space_Units_Area_Precision()
        {
            List<int> prescribed_space_units = Q__Prescribed_Space_Units();

            double area_score_sum = 0.0;

            foreach (var space_unit in prescribed_space_units)
            {
                if (Q__Space_Unit_Exists(space_unit))
                {
                    double space_unit_area =
                        Q__Space_Unit__Area(space_unit);

                    double space_unit_prescribed_area =
                        Q__Space_Unit__Prescribed_Area(space_unit);

                    double similarity = space_unit_area.Q__Fractional_Similarity(space_unit_prescribed_area);

                    area_score_sum += similarity;
                }
            }

            double total_area_score = area_score_sum / (double)prescribed_space_units.Count;

            return total_area_score;
        }

    }
}
