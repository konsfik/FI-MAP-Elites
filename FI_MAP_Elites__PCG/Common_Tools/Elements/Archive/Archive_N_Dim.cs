using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public class Archive_N_Dim<T>
    {
        public readonly int[] num_cells__per__dimension;
        public readonly T[] element__per__cell_id;

        public Archive_N_Dim(
            int[] num_cells__per__dimension
            )
        {
            if (num_cells__per__dimension.Length < 1)
                throw new Exception("there must be at least one dimension");

            if (num_cells__per__dimension.Any(x => x <= 0))
                throw new Exception("num cells per dimension must be at least 1");

            this.num_cells__per__dimension = num_cells__per__dimension.Q__Deep_Copy();

            int total_num_cells = this.num_cells__per__dimension.Q__Product();

            element__per__cell_id = new T[total_num_cells];
        }

        public void M__Set_Value(int cell_id, T value)
        {
            element__per__cell_id[cell_id] = value;
        }

        public void M__Set_Value(List<int> cell_coordinates, T value)
        {
            int cell_id = Q__Cell_Coordinates__To__Cell_Index(cell_coordinates);
            element__per__cell_id[cell_id] = value;
        }

        public bool Q__Cell_Coordinates__Proper_Dimensions(List<int> cell_coordinates)
        {
            return cell_coordinates.Count == num_cells__per__dimension.Length;
        }

        public bool Q__Cell_Coordinates__Within_Bounds(List<int> cell_coordinates)
        {
            int num_dims = num_cells__per__dimension.Length;

            for (int d = 0; d < num_dims; d++)
            {
                if (cell_coordinates[d] < 0)
                    return false;
                if (cell_coordinates[d] >= num_cells__per__dimension[d])
                    return false;
            }

            return true;
        }

        public int Q__Cell_Coordinates__To__Cell_Index(List<int> cell_coordinates)
        {
            int num_dimensions = Q__Num_Dimensions();

            if (cell_coordinates.Count != num_dimensions)
                throw new Exception("coordinates' count does not match number of dimensions");

            if (cell_coordinates.Any(x => x < 0))
                throw new Exception("all coordinate values must be > 0");

            int cell_index = 0;


            for (int dim = 0; dim < num_dimensions; dim++)
            {
                int multiplier = num_cells__per__dimension.Q__Product(dim - 1);
                cell_index += cell_coordinates[dim] * multiplier;
            }

            return cell_index;
        }

        public List<int> Q__Cell_Index__To__Cell_Coordinates(int cell_index)
        {
            List<int> cell_coordinates = new List<int>();
            int num_dimensions = Q__Num_Dimensions();

            for (int dim = 0; dim < num_dimensions; dim++)
                cell_coordinates.Add(0);

            for (int dim = 0; dim < num_dimensions; dim++)
            {
                // multiply the num cells per dimension from index 0 to index of current dim.
                int multiplier = num_cells__per__dimension.Q__Product(dim);

                cell_coordinates[dim] = (cell_index / multiplier) % num_cells__per__dimension[dim];
            }

            return cell_coordinates;
        }

        public int Q__Num_Dimensions()
        {
            return num_cells__per__dimension.Length;
        }
    }
}
