using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Algorithms.Shared_Elements
{
    public enum BS_OT__Out_Of_Range_Treatment
    {
        DISCARD,
        KEEP
    }

    public class BS_Ortho_Tessellation : I_Deep_Copyable
    {
        public readonly int num_cells;
        public readonly int num_bcs;
        public readonly int[] num_cells__per__bc;
        public readonly double[] min_value__per__bc;
        public readonly double[] max_value__per__bc;
        public readonly double[][] division_values__per__bc;
        public readonly BS_OT__Out_Of_Range_Treatment out_of_range_treatment;

        public BS_Ortho_Tessellation(
            int num_bcs,
            int[] num_cells__per__bc,
            double[] min_value__per__bc,
            double[] max_value__per__bc,
            BS_OT__Out_Of_Range_Treatment out_of_range_treatment
            )
        {
            this.num_bcs = num_bcs;
            this.num_cells__per__bc = num_cells__per__bc.Q__Deep_Copy();
            this.num_cells = num_cells__per__bc.Q__Product();
            this.min_value__per__bc = min_value__per__bc.Q__Deep_Copy();
            this.max_value__per__bc = max_value__per__bc.Q__Deep_Copy();
            this.out_of_range_treatment = out_of_range_treatment;

            this.division_values__per__bc = new double[num_bcs][];

            for (int bc = 0; bc < num_bcs; bc++)
            {
                double min_value = min_value__per__bc[bc];
                double max_value = max_value__per__bc[bc];
                double range = max_value - min_value;
                int bc_cells = num_cells__per__bc[bc];
                double step = range / (double)bc_cells;
                division_values__per__bc[bc] = new double[bc_cells + 1];
                for (int i = 0; i <= bc_cells; i++)
                {
                    division_values__per__bc[bc][i] = min_value + i * step;
                }
            }
        }

        private BS_Ortho_Tessellation(
            BS_Ortho_Tessellation tessellation_to_copy
            )
        {
            this.num_cells = tessellation_to_copy.num_cells;
            this.num_bcs = tessellation_to_copy.num_bcs;
            this.num_cells__per__bc = tessellation_to_copy.num_cells__per__bc.Q__Deep_Copy();
            this.min_value__per__bc = tessellation_to_copy.min_value__per__bc.Q__Deep_Copy();
            this.max_value__per__bc = tessellation_to_copy.max_value__per__bc.Q__Deep_Copy();
            this.division_values__per__bc = tessellation_to_copy.division_values__per__bc.Q__Deep_Copy();
            this.out_of_range_treatment = tessellation_to_copy.out_of_range_treatment;
        }

        public object Q__Deep_Copy()
        {
            return new BS_Ortho_Tessellation(this);
        }

        public int Q__Coords__To__Cell(int[] coords)
        {
            int num_dimensions = num_cells__per__bc.Length;

            if (coords.Length != num_dimensions)
            {
                throw new Exception("coordinates' count does not match number of dimensions");
            }

            for (int i = 0; i < coords.Length; i++)
            {
                int coord = coords[i];
                if (coord < 0)
                {
                    return -1;
                }
                if (coord >= num_cells__per__bc[i])
                {
                    return -1;
                }
            }

            int index = 0;

            for (int dim = 0; dim < num_dimensions; dim++)
            {
                int multiplier = num_cells__per__bc.Q__Product(dim - 1);
                index += coords[dim] * multiplier;
            }

            return index;
        }

        public int[] Q__Cell__To__Coords(int cell)
        {

            int size = num_cells__per__bc.Q__Product();

            if (cell < 0 || cell >= size)
            {
                throw new IndexOutOfRangeException();
            }

            int[] coordinates = new int[num_bcs];

            for (int dim_id = 0; dim_id < num_bcs; dim_id++)
            {
                if (dim_id == 0)
                {
                    int dim_size = num_cells__per__bc[dim_id];
                    coordinates[dim_id] = cell % dim_size;
                }
                else
                {
                    int multiplier = num_cells__per__bc.Q__Product(dim_id - 1);
                    coordinates[dim_id] =
                        (cell / multiplier) % num_cells__per__bc[dim_id];
                }
            }

            return coordinates;
        }

        public int[] Q__Feature_Vector__To__Coords(double[] feature_vector)
        {
            int[] coords = new int[num_bcs];
            for (int bc = 0; bc < num_bcs; bc++)
            {
                double feature_value = feature_vector[bc];
                double min_feature_value = min_value__per__bc[bc];
                double max_feature_value = max_value__per__bc[bc];
                if (feature_value < min_feature_value)
                {
                    if (out_of_range_treatment == BS_OT__Out_Of_Range_Treatment.DISCARD)
                    {
                        coords[bc] = -1;
                    }
                    else if (out_of_range_treatment == BS_OT__Out_Of_Range_Treatment.KEEP)
                    {
                        coords[bc] = 0;
                    }
                }
                else if (feature_value > max_feature_value)
                {
                    if (out_of_range_treatment == BS_OT__Out_Of_Range_Treatment.DISCARD)
                    {
                        coords[bc] = -1;
                    }
                    else if (out_of_range_treatment == BS_OT__Out_Of_Range_Treatment.KEEP)
                    {
                        coords[bc] = num_cells__per__bc[bc] - 1;
                    }
                }
                else
                {
                    int num_vals = division_values__per__bc[bc].Length;
                    for (int i = 1; i < num_vals; i++)
                    {
                        double division_value = division_values__per__bc[bc][i];
                        if (feature_value < division_value)
                        {
                            coords[bc] = i - 1;
                            break;
                        }
                    }
                }
            }

            return coords;
        }

        public int Q__Feature_Vector__To__Cell(double[] feature_vector)
        {
            int[] cell_coordinates = Q__Feature_Vector__To__Coords(feature_vector);
            int cell_id = Q__Coords__To__Cell(cell_coordinates);
            return cell_id;
        }

        public bool Q__Contains_Coords(int[] coords)
        {
            if (coords.Length != num_bcs) throw new Exception("wrong number of dimensions");
            int cell = Q__Coords__To__Cell(coords);
            if (cell >= 0 && cell < num_cells)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<int[]> Q__Contained_Coords(List<int[]> coords)
        {
            List<int[]> contained_coords = new List<int[]>();
            foreach (var c in coords)
            {
                if (Q__Contains_Coords(c))
                {
                    contained_coords.Add(c.Q__Deep_Copy());
                }
            }
            return contained_coords;
        }

        /// <summary>
        /// Converts a list of coordinates to the corresponding list of cells.
        /// Any coordinate that is not contained in the tessellation is ignored.
        /// </summary>
        /// <param name="coords_list"></param>
        /// <returns></returns>
        public List<int> Q__Coords_List__To__Contained_Cells_List(List<int[]> coords_list)
        {
            List<int> cells_list = new List<int>();
            foreach (var coords in coords_list)
            {
                if (Q__Contains_Coords(coords))
                {
                    int cell = Q__Coords__To__Cell(coords);
                    cells_list.Add(cell);
                }
            }
            return cells_list;
        }

    }
}
