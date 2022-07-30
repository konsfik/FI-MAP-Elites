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
        public List<UEdge_i> Q__Legal_Connectivity_Graph__Edges_Connecting_Space_Units(
            int space_unit_1,
            int space_unit_2
            )
        {
            List<UEdge_i> edges = new List<UEdge_i>();
            if (
                Q__Space_Unit_Exists(space_unit_1) == false
                ||
                Q__Space_Unit_Exists(space_unit_2) == false
                )
            {
                return edges;
            }
            else
            {
                var unit_1_cells = Q__Space_Unit__Cells(space_unit_1);
                var unit_2_cells = Q__Space_Unit__Cells(space_unit_2);

                foreach (int unit_1_cell in unit_1_cells)
                {
                    foreach (int unit_2_cell in unit_2_cells)
                    {
                        if (
                            voronoi_tessellation
                            .connectivity_graph
                            .neighbors__per__vertex[unit_1_cell]
                            .Contains(unit_2_cell))
                        {
                            UEdge_i connection = new UEdge_i(unit_1_cell, unit_2_cell);
                            if (edges.Contains(connection) == false)
                            {
                                edges.Add(connection);
                            }
                        }
                    }
                }

                return edges;
            }
        }


        /// <summary>
        /// Returns the surrounding cells of a set of reference cells.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="reference_cells"></param>
        /// <returns></returns>
        public List<int> Q__Reference_Cells__Surrounding_Cells(List<int> reference_cells)
        {
            List<int> surrounding_cells = new List<int>();

            foreach (int rc in reference_cells)
            {
                List<int> cell_neighbors = Q__Cell__Neighbors(rc).FindAll(
                    x =>
                    surrounding_cells.Contains(x) == false
                    &&
                    reference_cells.Contains(x) == false
                    );

                surrounding_cells.AddRange(cell_neighbors);
            }

            return surrounding_cells;
        }

        /// <summary>
        /// Returns the surrounding cells of a set of reference cells.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="reference_cells"></param>
        /// <returns></returns>
        public List<int> Q__Reference_Cells__Surrounding_Cells__Free(List<int> reference_cells)
        {
            List<int> free_surrounding_cells = new List<int>();

            foreach (int rc in reference_cells)
            {
                List<int> cell_neighbors = Q__Cell__Neighbors(rc).FindAll(
                    x =>
                    free_surrounding_cells.Contains(x) == false
                    &&
                    reference_cells.Contains(x) == false
                    &&
                    Q__Is_Cell__Free_And_Active(x)
                    );

                free_surrounding_cells.AddRange(cell_neighbors);
            }

            return free_surrounding_cells;
        }

        public bool Q__Is_Cell__Free_And_Active(int cell_id)
        {
            bool is_free = Q__Is_Cell__Free(cell_id);
            bool is_legal = Q__Is_Cell__Active(cell_id);

            return is_free && is_legal;
        }

        public bool Q__Is_Cell__Free(int cell_id)
        {
            return space_unit__per__cell[cell_id] == -1;
        }

        public bool Q__Is_Cell__Active(int cell_id)
        {
            return
                voronoi_tessellation
                .connectivity_graph
                .neighbors__per__vertex
                .ContainsKey(cell_id);
        }

        public bool Q__Is_Cell__Exterior_Space_Unit(int cell) {
            int space_unit = space_unit__per__cell[cell];

            if (space_unit == -1) return false;

            return prescription.type__per__space_unit[space_unit] == Space_Unit__Type.EXTERIOR;
        }

        /// <summary>
        /// Returns the neighbors of a single cell.
        /// </summary>
        /// <param name="individual"></param>
        /// <param name="cell_id"></param>
        /// <returns></returns>
        public List<int> Q__Cell__Neighbors(int cell_id)
        {
            return
                voronoi_tessellation
                .connectivity_graph
                .Q__Neighbors(cell_id)
                .Q__Deep_Copy();
        }

        public double Q__Cell__Perimeter_Length(int cell_id)
        {
            List<Vec2d> cell_region = Q__Cell__Perimeter_Points(cell_id);

            double cell_perimeter = 0.0;

            int num_pts = cell_region.Count;

            for (int i = 0; i < num_pts; i++)
            {
                Vec2d p1 = cell_region[i];
                Vec2d p2;
                if (i < num_pts - 1)
                {
                    p2 = cell_region[i + 1];
                }
                else
                {
                    p2 = cell_region[0];
                }

                double len = (p2 - p1).Q__Magnitude();
                cell_perimeter += len;
            }

            return cell_perimeter;
        }

        public double Q__Cell__Area(int cell_id)
        {
            return
                voronoi_tessellation
                .area__per__cell[cell_id];
        }

        public List<Vec2d> Q__Cell__Perimeter_Points(int cell_id)
        {
            return
                voronoi_tessellation
                .perimeter_points__per__cell[cell_id]
                .Q__Deep_Copy();
        }

        public List<Line_Segment> Q__Cell__Perimeter_Lines(int cell_id)
        {
            return
                voronoi_tessellation
                .perimeter_lines__per__cell[cell_id]
                .Q__Deep_Copy();
        }

        public bool Q__Cells_Connection_Exists_And_Is_Legal(Undirected_Edge edge)
        {
            return
                voronoi_tessellation
                .connectivity_graph
                .Q__Contains_Edge(edge);
        }
    }
}
