using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class GM__Select_Place_Expand_Repeat : Evolvable_Geometry__Generation_Method
    {
        public Space_Unit_Selection_Method<DS__Architectural_Plan> space_unit_selection_method;
        public Single_Space_Unit_Placement_Method<DS__Architectural_Plan> space_unit_placement_method;
        public Space_Unit_Expansion_Method<DS__Architectural_Plan> space_unit_expansion_method;

        public MM__Repair__L3__Openings openings_repair_method;

        public GM__Select_Place_Expand_Repeat(
            DS__Layout_Constraints problem_specification,
            Point_Cloud_Generation_Method point_cloud_generation_method
            ) : base(
                problem_specification,
                point_cloud_generation_method
                )
        {
            this.space_unit_selection_method = new RSM__Prioritize_High_Centrality<DS__Architectural_Plan>();
            this.space_unit_placement_method = new RPM__Single_Cell_Near_Neighbors<DS__Architectural_Plan>();
            this.space_unit_expansion_method = new REM__Free_Adjacent_Cells__Random<DS__Architectural_Plan>();
            this.openings_repair_method = new MM__Repair__L3__Openings();
        }

        private GM__Select_Place_Expand_Repeat(
            GM__Select_Place_Expand_Repeat gm_to_copy
            ) : base(
                gm_to_copy.prescription,
                gm_to_copy.point_cloud_generation_method
                )
        {
            this.space_unit_selection_method = gm_to_copy.space_unit_selection_method;
            this.space_unit_placement_method = gm_to_copy.space_unit_placement_method;
            this.space_unit_expansion_method = gm_to_copy.space_unit_expansion_method;
            this.openings_repair_method = gm_to_copy.openings_repair_method;
        }

        public override object Q__Deep_Copy()
        {
            return new GM__Select_Place_Expand_Repeat(this);
        }

        public override DS__Architectural_Plan Generate_Individual(I_PRNG rand)
        {
            List<int> all_space_units = prescription.Q__Prescribed__Space_Units();
            List<int> unvisited_space_units = all_space_units.Q__Deep_Copy();
            List<int> completed_space_units = new List<int>();

            List<Vec2d> point_cloud = point_cloud_generation_method.Generate_Point_Cloud(rand);


            DS__Architectural_Plan individual = new DS__Architectural_Plan(
                prescription,
                point_cloud_generation_method.bounding_rectangle,
                point_cloud
                );

            while (unvisited_space_units.Count > 0)
            {
                // select a room...
                int current_space_unit = space_unit_selection_method.Select_Space_Unit(
                    rand,
                    individual,
                    completed_space_units
                    );

                unvisited_space_units.Remove(current_space_unit);

                // place the initial cells for that room
                bool space_unit_placed = space_unit_placement_method.Place_Space_Unit(
                    rand,
                    individual,
                    current_space_unit,
                    false
                    );

                if (space_unit_placed == false)
                {
                    completed_space_units.Add(current_space_unit);
                    continue;
                }

                // expand current room
                space_unit_expansion_method.Expand_Space_Unit(
                    rand,
                    individual,
                    current_space_unit,
                    false
                    );
                completed_space_units.Add(current_space_unit);

            }

            openings_repair_method.Mutate_Individual(rand, individual);

            individual.M__Recalculate_Phenotype(starting_level: 1);

            return individual;
        }


    }
}
