using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Random_Destruction : Mutation_Method<DS__Architectural_Plan>
    {
        public readonly List<Mutation_Method<DS__Architectural_Plan>> destruction_methods;
        public readonly int min_num_mutations;
        public readonly int max_num_mutations;

        public MM__Random_Destruction()
        {
            destruction_methods = new List<Mutation_Method<DS__Architectural_Plan>>() {
                new MM__Destruction__L1__Move_Voronoi_Points<DS__Architectural_Plan>(
                    0.05, 
                    0.05,
                    recalculate_phenotype: false
                    ),
                new MM__Destruction__L2__Blow_Up_Space_Unit<DS__Architectural_Plan>(
                    recalculate_phenotype: false
                    ),
                new MM__Destruction__L2__Blow_Up_Space_Unit__Safe<DS__Architectural_Plan>(
                    recalculate_phenotype: false
                    ),
                new MM__Destruction__L2__Erode_Space_Unit__Safe<DS__Architectural_Plan>(
                    recalculate_phenotype: false
                    ),
                new MM__Destruction__L2__Delete_Space_Unit<DS__Architectural_Plan>(
                    recalculate_phenotype: false
                    ),
                new MM__Destruction__L3__Delete_Random_Openings(
                    0.05,
                    recalculate_phenotype: false
                    )
            };

            min_num_mutations = 1;
            max_num_mutations = 3;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Random_Destruction();
        }

        public override void Mutate_Individual(I_PRNG rand, DS__Architectural_Plan individual)
        {
            int num_mutations = rand.Next(min_num_mutations, max_num_mutations + 1);
            for (int i = 0; i < num_mutations; i++)
            {
                var mutation_method = destruction_methods.Q__Random_Item(rand);
                mutation_method.Mutate_Individual(rand, individual);
            }
        }


    }
}
