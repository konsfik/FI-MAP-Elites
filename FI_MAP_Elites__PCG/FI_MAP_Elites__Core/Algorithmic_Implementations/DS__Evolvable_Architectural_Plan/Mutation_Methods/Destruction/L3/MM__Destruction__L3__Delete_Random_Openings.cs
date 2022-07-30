using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Destruction__L3__Delete_Random_Openings : Mutation_Method<DS__Architectural_Plan>
    {
        public readonly double mutation_rate;
        public readonly bool recalculate_phenotype;

        public MM__Destruction__L3__Delete_Random_Openings(
            double mutation_rate,
            bool recalculate_phenotype
            )
        {
            this.mutation_rate = mutation_rate;
            this.recalculate_phenotype = recalculate_phenotype;
        }

        private MM__Destruction__L3__Delete_Random_Openings(
            MM__Destruction__L3__Delete_Random_Openings mm_to_copy)
        {
            this.mutation_rate = mm_to_copy.mutation_rate;
            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Destruction__L3__Delete_Random_Openings(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            DS__Architectural_Plan individual
            )
        {
            bool at_least_one = false;

            int num_interior_doors = individual.connection_doors.Count;
            for (int i = num_interior_doors - 1; i >= 0; i--)
            {
                double dice_roll = rand.NextDouble();
                if (dice_roll < mutation_rate)
                {
                    individual.connection_doors.RemoveAt(i);
                    at_least_one = true;
                }
            }

            int num_exterior_doors = individual.entrance_doors.Count;
            for (int i = num_exterior_doors - 1; i >= 0; i--)
            {
                double dice_roll = rand.NextDouble();
                if (dice_roll < mutation_rate)
                {
                    individual.entrance_doors.RemoveAt(i);
                    at_least_one = true;
                }
            }

            int num_windows = individual.windows.Count;
            for (int i = num_windows - 1; i >= 0; i--)
            {
                double dice_roll = rand.NextDouble();
                if (dice_roll < mutation_rate)
                {
                    individual.windows.RemoveAt(i);
                    at_least_one = true;
                }
            }

            if (at_least_one == false)
            {
                List<int> available_types = new List<int>();
                if (num_interior_doors > 0) available_types.Add(0);
                if (num_exterior_doors > 0) available_types.Add(1);
                if (num_windows > 0) available_types.Add(2);

                if (available_types.Count == 0) return;

                int opening_type = available_types.Q__Random_Item(rand);

                if (opening_type == 0)
                {
                    individual.connection_doors.Pop_Random_Item(rand);
                }
                else if (opening_type == 1)
                {
                    individual.entrance_doors.Pop_Random_Item(rand);
                }
                else if (opening_type == 2)
                {
                    individual.windows.Pop_Random_Item(rand);
                }
            }

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: 3);
            }
        }
    }
}
