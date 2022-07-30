using Common_Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Repair__L2__Add_Missing_Space_Units<T> : Mutation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public RPM__Single_Cell_Near_Neighbors<T> room_placement_method;

        public MM__Repair__L2__Add_Missing_Space_Units()
        {
            room_placement_method = new RPM__Single_Cell_Near_Neighbors<T>();
        }

        private MM__Repair__L2__Add_Missing_Space_Units(MM__Repair__L2__Add_Missing_Space_Units<T> mm_to_copy)
        {
            this.room_placement_method =
                (RPM__Single_Cell_Near_Neighbors<T>)mm_to_copy.room_placement_method.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Repair__L2__Add_Missing_Space_Units<T>(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            T individual
            )
        {
            List<int> missing_rooms = individual.Q__Missing_Prescribed_Space_Units();
            missing_rooms.M__Shuffle(rand);

            foreach (int missing_room in missing_rooms)
            {
                room_placement_method.Place_Space_Unit(
                    rand,
                    individual,
                    missing_room,
                    false
                    );
            }

            individual.M__Recalculate_Phenotype(starting_level: 2);
        }
    }
}
