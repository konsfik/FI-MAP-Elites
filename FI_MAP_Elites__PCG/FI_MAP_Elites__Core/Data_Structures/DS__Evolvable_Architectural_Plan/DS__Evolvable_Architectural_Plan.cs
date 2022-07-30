using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Voronoi;


namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Architectural_Plan : DS__Evolvable_Geometry
    {
        // L3 Genotype
        public List<EG__Connection_Door> connection_doors;
        public List<EG__Entrance_Door> entrance_doors;
        public List<EG__Window> windows;

        // L3 Phenotype / bcs
        public DS__Plan_Weighted_Graph plan_graph;

        // json constructor
        public DS__Architectural_Plan(
            // context
            DS__Layout_Constraints prescription,
            // L1
            DS__Voronoi voronoi_tessellation,
            // L2 genotype
            Dictionary<int, int> space_unit__per__cell,
            // L3 genotype
            List<EG__Connection_Door> connection_doors,
            List<EG__Entrance_Door> entrance_doors,
            List<EG__Window> windows,
            // L3 phenotype
            DS__Plan_Weighted_Graph plan_graph
            ):base(
                // context
                prescription,
                // L1
                voronoi_tessellation,
                // L2 genotype
                space_unit__per__cell
                )
        {
            // L3 genotype
            this.connection_doors = new List<EG__Connection_Door>(connection_doors);
            this.entrance_doors = new List<EG__Entrance_Door>(entrance_doors);
            this.windows = new List<EG__Window>(windows);

            // L3 phenotype
            this.plan_graph = (DS__Plan_Weighted_Graph)plan_graph.Q__Deep_Copy();
        }

        public DS__Architectural_Plan(
            DS__Layout_Constraints prescription,
            Rect2d bounding_rect,
            List<Vec2d> point__per__cell_id
            ):base(
                prescription,
                bounding_rect,
                point__per__cell_id
                )
        {
            connection_doors = new List<EG__Connection_Door>();
            entrance_doors = new List<EG__Entrance_Door>();
            windows = new List<EG__Window>();
            plan_graph = new DS__Plan_Weighted_Graph();
        }



        protected DS__Architectural_Plan(DS__Architectural_Plan solution_to_copy)
            :base(
                 solution_to_copy
                 )
        {
            // genotype
            this.connection_doors = new List<EG__Connection_Door>(solution_to_copy.connection_doors);
            this.entrance_doors = new List<EG__Entrance_Door>(solution_to_copy.entrance_doors);
            this.windows = new List<EG__Window>(solution_to_copy.windows);

            // phenotype / bcs
            this.plan_graph = (DS__Plan_Weighted_Graph)solution_to_copy.plan_graph.Q__Deep_Copy();
        }
        
        public override object Q__Deep_Copy()
        {
            return new DS__Architectural_Plan(this);
        }
    }
}
