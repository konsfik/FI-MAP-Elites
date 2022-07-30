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
        #region constraints
        public bool Q__CEM__L1__Voronoi_Connected()
        {
            return
                voronoi_tessellation
                .connectivity_graph
                .Q__Is_Connected();
        }

        public bool Q__CEM__L1__Voronoi_Min_Line_Length(double min_line_length)
        {
            return
                voronoi_tessellation
                .connectivity_graph
                .Q__Is_Connected();
        }

        public bool Q__CEM__L1__Voronoi_Min_Percent_Active_Cells(double min_percent_active_cells)
        {
            int num_generator_points = voronoi_tessellation.Q__Num_Generator_Points();
            int num_active_cells = voronoi_tessellation.Q__Num_Active_Cells();

            double percent_active_cells = (double)num_active_cells / (double)num_generator_points;

            if (percent_active_cells >= min_percent_active_cells)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region gradient constraints
        public double Q__EM__CEM__L1__Voronoi_Connected()
        {
            int num_islands = 
                voronoi_tessellation
                .connectivity_graph
                .Q__Graph_Islands()
                .Count;
            double score 
                = 1.0 / num_islands;
            return score;
        }

        public double Q__EM__CEM__Space_Units_Exist()
        {
            int num_prescribed_space_units = Q__Num_Prescribed_Space_Units();
            int num_existing_space_units = Q__Existing_Space_Units().Count;

            return
                (double)num_existing_space_units
                / (double)num_prescribed_space_units;
        }

        /// <summary>
        /// Returns the coherence score of the plan.
        /// The coherence score is calculated per room in the following way:
        /// - If a room is completely missing, its coherence score is 0.
        /// - If a room exists in one piece, its coherence score is 1.
        /// - If a room exists in more than one pieces, its coherence score is
        ///     1 / num_pieces.
        /// </summary>
        /// <returns></returns>
        public double Q__EM__CEM__Space_Units_Are_Coherent()
        {
            List<int> prescribed_space_units = Q__Prescribed_Space_Units();

            double coherence_sum = 0;

            foreach (int space_unit in prescribed_space_units)
            {
                bool exists = Q__Space_Unit_Exists(space_unit);
                if (!exists) continue;

                DS__Undirected_Graph space_unit_sub_graph = Q__Space_Unit__Sub_Graph(space_unit);
                int num_islands = space_unit_sub_graph.Q__Graph_Islands().Count;
                coherence_sum += 1.0 / num_islands;
            }

            double coherence_score = coherence_sum / (double)prescribed_space_units.Count;

            return coherence_score;
        }

        public double Q__EM__CEM__Prescribed_Connections_Exist()
        {
            List<Undirected_Edge> prescribed_connections = Q__Prescribed_Connections();

            double prescribed_connections_score = 0.0;

            foreach (Undirected_Edge connection in prescribed_connections)
            {
                if (Q__Space_Units_Connection_Exists(connection))
                {
                    prescribed_connections_score += 1.0;
                }
            }

            prescribed_connections_score /= (double)prescribed_connections.Count;

            return prescribed_connections_score;
        }
        #endregion
    }
}
