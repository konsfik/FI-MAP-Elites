using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class GM__Random_Placement_Repair : Evolvable_Geometry__Generation_Method
    {
        public MM__Total_Repair repair_method;

        public GM__Random_Placement_Repair(
            DS__Layout_Constraints prescription,
            Point_Cloud_Generation_Method point_cloud_generation_method
            ) : base(
                prescription,
                point_cloud_generation_method
                )
        {
            repair_method = new MM__Total_Repair();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="gm_to_copy"></param>
        private GM__Random_Placement_Repair(GM__Random_Placement_Repair gm_to_copy)
            :base(
                 gm_to_copy.prescription,
                 gm_to_copy.point_cloud_generation_method
                 )
        {
            
        }

        public override object Q__Deep_Copy()
        {
            return new GM__Random_Placement_Repair(this);
        }

        public override DS__Architectural_Plan Generate_Individual(I_PRNG rand)
        {
            List<Vec2d> point_cloud = point_cloud_generation_method.Generate_Point_Cloud(rand);

            DS__Architectural_Plan solution = new DS__Architectural_Plan(
                prescription,
                point_cloud_generation_method.bounding_rectangle,
                point_cloud
                );

            // assign the random rooms, here... 

            List<int> active_cells = solution.Q__Active_Cells();
            List<int> all_rooms = solution.prescription.Q__Prescribed__Space_Units();

            int num_rooms = all_rooms.Count;
            for (int i = 0; i < num_rooms; i++) {
                all_rooms.Add(-1);
            }

            foreach (var cell in active_cells) {
                int random_room_id = all_rooms.Q__Random_Item(rand);
                solution.M__Assign_Cell_To_Space_Unit(cell, random_room_id, false);
            }

            solution.M__Recalculate_Phenotype(starting_level: 2);

            return solution;
        }

        
    }
}
