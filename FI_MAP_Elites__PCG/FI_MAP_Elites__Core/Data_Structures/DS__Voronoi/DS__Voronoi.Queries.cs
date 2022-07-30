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
        public void Q__Shared_Line__Between_Neighbor_Cells(
            int cell_1,
            int cell_2,
            out bool success,
            out Line_Segment shared_line
            )
        {
            int num_points = Q__Num_Generator_Points();
            if (cell_1 < 0 || cell_1 > num_points - 1)
            {
                throw new IndexOutOfRangeException();
            }
            if (cell_2 < 0 || cell_2 > num_points - 1)
            {
                throw new IndexOutOfRangeException();
            }

            bool cell_1_active = Q__Cell__Is_Active(cell_1);
            bool cell_2_active = Q__Cell__Is_Active(cell_2);
            bool cells_connected = connectivity_graph.Q__Contains_Edge(cell_1, cell_2);
            if (cell_1_active == false || cell_2_active == false || cells_connected == false)
            {
                success = false;
                shared_line = new Line_Segment(0, 0, 0, 0);
                return;
            }
            else
            {
                success = true;
                shared_line =
                    perimeter_lines__per__cell[cell_1]
                    .Find(
                        x => perimeter_lines__per__cell[cell_2].Contains(x)
                        );
            }
        }

        public List<double> Q__Active_Cells__Areas_List()
        {
            int num_pts = Q__Num_Generator_Points();
            List<double> areas = new List<double>(capacity: num_pts);
            for (int c = 0; c < num_pts; c++)
            {
                if (Q__Cell__Is_Active(c))
                {
                    double cell_area = Q__Cell_Area(c);
                    areas.Add(cell_area);
                }
            }
            return areas;
        }

        public List<double> Q__Active_Cells__Sizes_List()
        {
            int num_pts = Q__Num_Generator_Points();
            List<double> areas = new List<double>(capacity: num_pts);
            for (int c = 0; c < num_pts; c++)
            {
                if (Q__Cell__Is_Active(c))
                {
                    double cell_area = Q__Cell_Size(c);
                    areas.Add(cell_area);
                }
            }
            return areas;
        }

        public List<double> Q__Active_Cells__Compactnes_List()
        {
            int num_pts = Q__Num_Generator_Points();
            List<double> compactness_list = new List<double>(capacity: num_pts);
            for (int c = 0; c < num_pts; c++)
            {
                if (Q__Cell__Is_Active(c))
                {
                    double cell_compactness = Q__Cell_Compactness(c);
                    compactness_list.Add(cell_compactness);
                }
            }
            return compactness_list;
        }

        public int Q__Num_Generator_Points()
        {
            return generator_points.Count;
        }

        public int Q__Num_Active_Cells()
        {
            return is_active__per__cell.Q__Sum();
        }

        public List<int> Q__All_Cells()
        {
            int num_cells = generator_points.Count;
            List<int> all_cells = new List<int>(capacity: num_cells);
            for (int c = 0; c < num_cells; c++)
            {
                all_cells.Add(c);
            }
            return all_cells;
        }

        public List<int> Q__Active_Cells()
        {
            int num_cells = Q__Num_Generator_Points();
            List<int> active_cells = new List<int>(capacity: num_cells);
            for (int i = 0; i < num_cells; i++)
            {
                if (Q__Cell__Is_Active(i))
                {
                    active_cells.Add(i);
                }
            }
            return active_cells;
        }

        public List<int> Q__Inactive_Cells()
        {
            int num_cells = Q__Num_Generator_Points();
            List<int> inactive_cells = new List<int>(capacity: num_cells);
            for (int i = 0; i < num_cells; i++)
            {
                if (Q__Cell__Is_Active(i) == false)
                {
                    inactive_cells.Add(i);
                }
            }
            return inactive_cells;
        }
    }
}
