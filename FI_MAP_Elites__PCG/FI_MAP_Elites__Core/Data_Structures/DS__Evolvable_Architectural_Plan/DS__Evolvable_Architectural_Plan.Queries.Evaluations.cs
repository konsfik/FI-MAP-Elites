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
    public partial class DS__Architectural_Plan : DS__Evolvable_Geometry
    {
        public double Q__BC__Avg_Dist_Entrances_Connection_Doors__Normalized()
        {
            double half_perimeter = 
                voronoi_tessellation
                .bounding_rectangle
                .Q__Perimeter() / 2.0;

            double bc__avg_dist_entrances_connection_doors = 
                plan_graph.Q__Avg_Dist_Entrances_Connection_Doors();

            double normalized_average_distance
                = bc__avg_dist_entrances_connection_doors / half_perimeter;

            return normalized_average_distance;
        }


        public double Q__BC__Avg_Dist_Connection_Doors_Normalized()
        {
            double half_perimeter =
                voronoi_tessellation
                .bounding_rectangle
                .Q__Perimeter() / 2.0;

            double bc__avg_dist_connection_doors = plan_graph.Q__Avg_Dist_Connection_Doors();

            double normalized_average_distance
                = bc__avg_dist_connection_doors / half_perimeter;

            return normalized_average_distance;
        }

        public double Q__BC__Avg_Dist_Windows_Normalized()
        {
            double half_perimeter =
                voronoi_tessellation
                .bounding_rectangle
                .Q__Perimeter() / 2.0;

            double bc__avg_dist_windows =
                plan_graph.Q__Avg_Dist_Windows();

            double normalized_average_distance = 
                bc__avg_dist_windows / half_perimeter;

            return normalized_average_distance;
        }

        public double Q__BC__Percent_Circulation_Area()
        {
            double total_area = Q__Plan__Total_Area();
            double circulation_area = plan_graph.Q__Circulation_Area(1.0);
            return circulation_area / total_area;
        }


        // constraints

        public double Q__Eval__Openings_Placement_Score()
        {
            int num_total_openings = Q__Num__Existing_Openings();
            int num_prescribed_openings = Q__Num__Prescribed_Openings();
            int num_satisfied_prescribed_openings = Q__Num__Satisfied_Prescribed_Openings();

            double percent_satisfied =
                (double)num_satisfied_prescribed_openings / (double)num_prescribed_openings;

            int mistakes = num_total_openings - num_satisfied_prescribed_openings;
            double percent_mistakes = (double)mistakes / (double)num_total_openings;
            double percent_correct = 1.0 - percent_mistakes;

            double final_score =
                (percent_satisfied + percent_correct) / 2.0;

            return final_score;
        }

    }
}
