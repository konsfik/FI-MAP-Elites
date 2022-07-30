using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Destruction__L2__Random_Space_Units_Cells : Mutation_Method<DS__Architectural_Plan>
    {
        public readonly double selection_probability;
        public readonly bool recalculate_phenotype;

        public MM__Destruction__L2__Random_Space_Units_Cells(
            double selection_probability,
            bool recalculate_phenotype
            )
        {
            this.selection_probability = selection_probability;
            this.recalculate_phenotype = recalculate_phenotype;
        }

        public MM__Destruction__L2__Random_Space_Units_Cells(
            MM__Destruction__L2__Random_Space_Units_Cells mm_to_copy)
        {
            this.selection_probability = mm_to_copy.selection_probability;
            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Destruction__L2__Random_Space_Units_Cells(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            DS__Architectural_Plan individual
            )
        {
            List<int> all_room_ids = individual.prescription.Q__Prescribed__Space_Units();
            all_room_ids.Add(-1); // add the no room id

            List<int> all_cell_ids =
                individual
                .voronoi_tessellation
                .connectivity_graph
                .Q__Vertices();

            foreach (var cell in all_cell_ids)
            {
                double dice_roll = rand.Next();
                if (dice_roll < selection_probability)
                {
                    int random_room_id = all_room_ids.Q__Random_Item(rand);
                    individual.M__Assign_Cell_To_Space_Unit(
                        cell,
                        random_room_id,
                        false
                        );
                }
            }

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: 2);
            }
        }
    }
}
