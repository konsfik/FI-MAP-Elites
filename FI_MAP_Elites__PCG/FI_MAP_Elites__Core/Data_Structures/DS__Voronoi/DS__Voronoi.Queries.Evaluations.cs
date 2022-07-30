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
        #region cells_area
        /// <summary>
        /// Calculates the sum of all cells' areas.
        /// </summary>
        /// <returns></returns>
        public double Q__Eval__Cells_Area__Sum()
        {
            List<double> areas = Q__Active_Cells__Areas_List();
            return areas.Q__Sum();
        }

        /// <summary>
        /// Divides the cells areas sum by the total available area.
        /// </summary>
        /// <returns></returns>
        public double Q__Eval__Cells_Area__Sum__Normalized()
        {
            List<double> areas = Q__Active_Cells__Areas_List();
            if (areas.Count < 0.0) return double.NaN;
            double sum = areas.Q__Sum();

            double total_area =
                (double)bounding_rectangle.width
                * (double)bounding_rectangle.height;

            double normalized_sum =
                sum / total_area;

            return normalized_sum;
        }

        /// <summary>
        /// Calculates the average area of all cells.
        /// </summary>
        /// <returns></returns>
        public double Q__Eval__Cells_Area__Average()
        {
            List<double> areas = Q__Active_Cells__Areas_List();
            return areas.Q__Mean();
        }

        /// <summary>
        /// Divides the average area of cells by the total available area.
        /// In essence, the maximum value of this metric is when a single cell
        /// occupies the total available area.
        /// Given the current implementation the maximum value is impossible.
        /// </summary>
        /// <returns></returns>
        public double Q__Eval__Cells_Area__Average__Normalized()
        {
            double total_area =
                (double)bounding_rectangle.width
                * (double)bounding_rectangle.height;

            List<double> areas = Q__Active_Cells__Areas_List();
            double average_area = areas.Q__Mean();

            double average_area_normalized = average_area / total_area;

            return average_area_normalized;
        }

        public double Q__Eval__Cells_Area__Simplistic_Variance()
        {
            List<double> areas = Q__Active_Cells__Areas_List();
            return areas.Q__Simplistic_Variance();
        }

        public double Q__Eval__Cells_Area__Simplistic_Variance__Normalized()
        {
            double max_case__total_area =
                bounding_rectangle.width
                * bounding_rectangle.height;

            // the maximum total simplistic variance will be when
            // a single cell occupies the maximum_total_area and 
            // all the remaining cells have an area of zero.
            // in that case, the average area will be equal to (maximum_total_area / num_active_cells)
            int num_active_cells = Q__Num_Active_Cells();
            double max_case__average_area = max_case__total_area / num_active_cells;

            // the difference between the large cell and the average is equal to
            double max_case__large_diff = max_case__total_area - max_case__average_area;
            double max_case__small_diff = max_case__average_area;

            // maximum possible simplistic variance sum:
            double max_var_sum =
                max_case__large_diff + (num_active_cells - 1) * max_case__small_diff;

            // maximum possible variance:
            double max_var = max_var_sum / num_active_cells;


            double variance = Q__Eval__Cells_Area__Simplistic_Variance();

            return variance / max_var;
        }

        public double Q__Eval__Cells_Area__Diversity()
        {
            List<double> areas = Q__Active_Cells__Areas_List();
            return areas.Q__Diversity();
        }

        public double Q__Eval__Cells_Area__Diversity__Normalized()
        {
            int num_active_cells = Q__Num_Active_Cells();

            if (num_active_cells < 2)
            {
                return double.NaN;
            }

            double total_usable_area =
                (double)bounding_rectangle.width
                * (double)bounding_rectangle.height;

            double max_diversity = total_usable_area / (double)num_active_cells;

            List<double> areas = Q__Active_Cells__Areas_List();
            double diversity = areas.Q__Diversity();

            double normalized_diversity = diversity / max_diversity;

            return normalized_diversity;
        }

        public double Q__Eval__Cells_Area__Minimum_Difference()
        {
            List<double> areas = Q__Active_Cells__Areas_List();
            return areas.Q__Minimum_Difference();
        }

        public double Q__Eval__Cells_Area__Minimum_Difference__Normalized()
        {
            int num_active_cells = Q__Num_Active_Cells();

            if (num_active_cells < 2)
            {
                return double.NaN;
            }

            double total_usable_area =
                (double)bounding_rectangle.width
                * (double)bounding_rectangle.height;

            List<double> areas = Q__Active_Cells__Areas_List();
            double min_dif = areas.Q__Minimum_Difference();

            // the maximum min_dif is when all are equidistant from min to max...
            List<double> sample_list = new List<double>();
            double sample_counter = 0.0;
            for (int i = 0; i < num_active_cells; i++)
            {
                sample_list.Add(sample_counter);
                sample_counter += 1.0;
            }
            double sample_list_sum = sample_list.Q__Sum();

            double fraction = sample_list_sum / total_usable_area;

            sample_list.M__Divide_By(fraction);

            //double new_sum = sample_list.Q__Sum();

            double max__min_dif = sample_list[1];

            double normalized__min_dif = min_dif / max__min_dif;

            return normalized__min_dif;
        }

        #endregion

        /// <summary>
        /// Iterates over all the angles in the voronoi diagram
        /// and claculates their proximity to either square or straight angles.
        /// </summary>
        /// <returns></returns>
        public double Q__Eval__Angles_Orthogonality__Average()
        {
            int num_lines = voronoi_lines.Count;

            double score_sum = 0.0;
            int num_measurements = 0;
            for (int i = 0; i < num_lines - 1; i++)
            {
                for (int j = i + 1; j < num_lines; j++)
                {
                    Line_Segment line_1 = voronoi_lines[i];
                    Line_Segment line_2 = voronoi_lines[j];
                    bool connected = line_1.Q__Is_Connected_To(line_2);
                    if (connected == false)
                    {
                        continue;
                    }
                    double angle = line_1.Q__Angle__Between_Connected_Lines(line_2);
                    double this_score;

                    // 0 (PI/4) PI/2 (3PI/4) PI
                    // 0 -> PI/4 : HI -> LO
                    // PI/4 -> PI/2 : LO -> HI
                    // PI/2 -> 3PI/4 : HI -> LO
                    // 3PI/4 -> PI : LO -> HI

                    if (angle < Math.PI / 4.0)
                    {
                        this_score = angle.Q__Mapped(
                            0.0, Math.PI / 4.0,
                            1.0, 0.0 // HI -> LO
                            );
                    }
                    else if (angle < Math.PI / 2.0)
                    {
                        this_score = angle.Q__Mapped(
                            Math.PI / 4.0, Math.PI / 2.0,
                            0.0, 1.0 // LO -> HI
                            );
                    }
                    else if (angle < 3.0 * Math.PI / 4.0)
                    {
                        this_score = angle.Q__Mapped(
                            Math.PI / 2.0, 3.0 * Math.PI / 4.0,
                            1.0, 0.0 // HI -> LO
                            );
                    }
                    else
                    {
                        this_score = angle.Q__Mapped(
                            3.0 * Math.PI / 4.0, Math.PI,
                            0.0, 1.0 // LO -> HI
                            );
                    }

                    score_sum += this_score;
                    num_measurements++;
                }
            }

            double total_score = score_sum / (double)num_measurements;
            return total_score;
        }

        public double Q__Eval__Angles_Orthogonality__Weighted_Average()
        {
            int num_lines = voronoi_lines.Count;

            double score_sum = 0.0;
            double length_sum = 0.0;
            for (int i = 0; i < num_lines - 1; i++)
            {
                for (int j = i + 1; j < num_lines; j++)
                {
                    Line_Segment line_1 = voronoi_lines[i];
                    Line_Segment line_2 = voronoi_lines[j];
                    bool connected = line_1.Q__Is_Connected_To(line_2);
                    if (connected == false)
                    {
                        continue;
                    }
                    double angle = line_1.Q__Angle__Between_Connected_Lines(line_2);
                    double line_1_length = line_1.Q__Magnitude();
                    double line_2_length = line_2.Q__Magnitude();
                    double lines_length_sum = line_1_length + line_2_length;
                    length_sum += lines_length_sum;

                    double this_score;

                    // 0 (PI/4) PI/2 (3PI/4) PI
                    // 0 -> PI/4 : HI -> LO
                    // PI/4 -> PI/2 : LO -> HI
                    // PI/2 -> 3PI/4 : HI -> LO
                    // 3PI/4 -> PI : LO -> HI

                    if (angle < Math.PI / 4.0)
                    {
                        this_score = angle.Q__Mapped(
                            0.0, Math.PI / 4.0,
                            1.0, 0.0 // HI -> LO
                            );
                    }
                    else if (angle < Math.PI / 2.0)
                    {
                        this_score = angle.Q__Mapped(
                            Math.PI / 4.0, Math.PI / 2.0,
                            0.0, 1.0 // LO -> HI
                            );
                    }
                    else if (angle < 3.0 * Math.PI / 4.0)
                    {
                        this_score = angle.Q__Mapped(
                            Math.PI / 2.0, 3.0 * Math.PI / 4.0,
                            1.0, 0.0 // HI -> LO
                            );
                    }
                    else
                    {
                        this_score = angle.Q__Mapped(
                            3.0 * Math.PI / 4.0, Math.PI,
                            0.0, 1.0 // LO -> HI
                            );
                    }

                    this_score *= lines_length_sum;

                    score_sum += this_score;
                }
            }

            double total_score = score_sum / length_sum;
            return total_score;
        }

        public double Q__Eval__Angles_Orthogonality__Diversity()
        {
            int num_lines = voronoi_lines.Count;

            List<double> scores = new List<double>();
            for (int i = 0; i < num_lines - 1; i++)
            {
                for (int j = i + 1; j < num_lines; j++)
                {
                    Line_Segment line_1 = voronoi_lines[i];
                    Line_Segment line_2 = voronoi_lines[j];
                    bool connected = line_1.Q__Is_Connected_To(line_2);
                    if (connected == false)
                    {
                        continue;
                    }
                    double angle = line_1.Q__Angle__Between_Connected_Lines(line_2);
                    double this_score;

                    // 0 (PI/4) PI/2 (3PI/4) PI
                    // 0 -> PI/4 : HI -> LO
                    // PI/4 -> PI/2 : LO -> HI
                    // PI/2 -> 3PI/4 : HI -> LO
                    // 3PI/4 -> PI : LO -> HI

                    if (angle < Math.PI / 4.0)
                    {
                        this_score = angle.Q__Mapped(
                            0.0, Math.PI / 4.0,
                            1.0, 0.0 // HI -> LO
                            );
                    }
                    else if (angle < Math.PI / 2.0)
                    {
                        this_score = angle.Q__Mapped(
                            Math.PI / 4.0, Math.PI / 2.0,
                            0.0, 1.0 // LO -> HI
                            );
                    }
                    else if (angle < 3.0 * Math.PI / 4.0)
                    {
                        this_score = angle.Q__Mapped(
                            Math.PI / 2.0, 3.0 * Math.PI / 4.0,
                            1.0, 0.0 // HI -> LO
                            );
                    }
                    else
                    {
                        this_score = angle.Q__Mapped(
                            3.0 * Math.PI / 4.0, Math.PI,
                            0.0, 1.0 // LO -> HI
                            );
                    }

                    scores.Add(this_score);
                }
            }

            double diversity = scores.Q__Diversity();

            return diversity;
        }

        /// <summary>
        /// Returns the average proximity of all lines to the horizontal or vertical axes.
        /// </summary>
        /// <returns></returns>
        public double Q__Eval__Lines_Orthogonality__Average()
        {
            int num_lines = voronoi_lines.Count;
            if (num_lines == 0) return double.NaN;

            double score_sum = 0.0;
            foreach (var line in voronoi_lines)
            {
                double orthogonality = line.Q__Orthogonality();
                score_sum += orthogonality;
            }
            return score_sum / (double)num_lines;
        }

        public double Q__Eval__Lines_Orthogonality__Weighted__Average()
        {
            int num_lines = voronoi_lines.Count;
            if (num_lines == 0) return double.NaN;

            double score_sum = 0.0;
            double length_sum = 0.0;
            foreach (var line in voronoi_lines)
            {
                double length = line.Q__Magnitude();
                length_sum += length;

                double orthogonality = line.Q__Orthogonality();

                double score = orthogonality * length;

                score_sum += score;
            }
            return score_sum / length_sum;
        }

        public double Q__Eval__Lines_Orthogonality__Simplistic_Variance()
        {
            int num_lines = voronoi_lines.Count;
            if (num_lines == 0) return double.NaN;

            List<double> orthogonalities = new List<double>(capacity: num_lines);
            foreach (var line in voronoi_lines)
            {
                double orthogonality = line.Q__Orthogonality();
                orthogonalities.Add(orthogonality);
            }

            double diversity = orthogonalities.Q__Simplistic_Variance();

            return diversity;
        }

        public double Q__Eval__Lines_Orthogonality__Diversity()
        {
            int num_lines = voronoi_lines.Count;
            if (num_lines == 0) return double.NaN;

            List<double> orthogonalities = new List<double>(capacity: num_lines);
            foreach (var line in voronoi_lines)
            {
                double orthogonality = line.Q__Orthogonality();
                orthogonalities.Add(orthogonality);
            }

            double diversity = orthogonalities.Q__Diversity();

            return diversity;
        }

        public double Q__Eval__Cells_Compactness__Average()
        {
            int num_cells = Q__Num_Generator_Points();
            int num_active_cells = 0;
            double compactness_sum = 0.0;
            for (int c = 0; c < num_cells; c++)
            {
                if (Q__Cell__Is_Active(c))
                {
                    double cell_compactness = Q__Cell_Compactness(c);
                    compactness_sum += cell_compactness;
                    num_active_cells++;
                }
            }
            double average_compactness = compactness_sum / (double)num_active_cells;
            return average_compactness;
        }

        public double Q__Eval__Cells_Compactness__Simplistic_Variance()
        {
            int num_pts = Q__Num_Generator_Points();

            List<double> compactness_per_cell = new List<double>(capacity: num_pts);
            for (int c = 0; c < num_pts; c++)
            {
                if (Q__Cell__Is_Active(c))
                {
                    double cell_compactness = Q__Cell_Compactness(c);
                    compactness_per_cell.Add(cell_compactness);
                }
            }

            return compactness_per_cell.Q__Simplistic_Variance();
        }

        public double Q__Eval__Cells_Compactness__Simplistic_Variance__Normalized()
        {
            double simplistic_variance = Q__Eval__Cells_Compactness__Simplistic_Variance();

            // maximum possible simplistic variance is when 
            // half the cells have a compactness of 1 (max value)
            // and the other half have a compactness of 0 (min value)
            // in that case, the simplistic variance will be equal to 0.5
            // so we normalize the simplistic variance from (0, 0.5) to (0, 1.0)
            double maximum_simplistic_variance = 0.5;

            double normalized_simplistic_variance =
                simplistic_variance.Q__Mapped(0.0, maximum_simplistic_variance, 0.0, 1.0);

            return normalized_simplistic_variance;
        }

        public double Q__Eval__Cells_Compactness__Diversity()
        {
            int num_cells = Q__Num_Generator_Points();
            List<double> cells_compactness = new List<double>(capacity: num_cells);
            for (int c = 0; c < num_cells; c++)
            {
                if (Q__Cell__Is_Active(c))
                {
                    double cell_compactness = Q__Cell_Compactness(c);
                    cells_compactness.Add(cell_compactness);
                }
            }

            if (cells_compactness.Count <= 1) return double.NaN;

            return cells_compactness.Q__Diversity();
        }

        public double Q__Eval__Cells_Compactness__Diversity__Normalized()
        {
            int num_active_cells = Q__Num_Active_Cells();
            if (num_active_cells <= 1) return double.NaN;

            double diversity = Q__Eval__Cells_Compactness__Diversity();

            // maximum diversity is when all the values have been spread in the range
            // the range is (0 ... 1)
            // the optimal distance between values is 1 / (num_values - 1)

            double max_diversity = 1.0 / (double)(num_active_cells - 1);
            double normalized_diversity = diversity.Q__Mapped(0.0, max_diversity, 0.0, 1.0);

            return normalized_diversity;
        }


        public double Q__Eval__Orthogonality()
        {
            throw new NotImplementedException();
        }


    }
}
