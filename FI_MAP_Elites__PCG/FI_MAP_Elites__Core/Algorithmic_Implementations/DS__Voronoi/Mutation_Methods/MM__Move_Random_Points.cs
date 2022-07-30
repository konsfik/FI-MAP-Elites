using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class MM__Move_Random_Points : Mutation_Method<DS__Voronoi>
    {
        public readonly double mutation_chance;
        public readonly double mutation_intensity;

        public MM__Move_Random_Points(
            double mutation_chance,
            double mutation_intensity
            )
        {
            this.mutation_chance = mutation_chance;
            this.mutation_intensity = mutation_intensity;
        }

        private MM__Move_Random_Points(
            MM__Move_Random_Points mm_to_copy
            )
        {
            this.mutation_chance = mm_to_copy.mutation_chance;
            this.mutation_intensity = mm_to_copy.mutation_intensity;
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            DS__Voronoi individual
            )
        {
            double width = individual.bounding_rectangle.width;
            double height = individual.bounding_rectangle.height;
            int num_points = individual.Q__Num_Generator_Points();
            bool at_least_one_moved = false;
            for (int i = 0; i < num_points; i++)
            {
                double dice_roll = rand.NextDouble();
                if (dice_roll < mutation_chance)
                {
                    at_least_one_moved = true;
                    double width_offset =
                        (rand.NextDouble() * 2.0 - 1.0)
                        * width
                        * mutation_intensity;
                    double height_offset =
                        (rand.NextDouble() * 2.0 - 1.0)
                        * height
                        * mutation_intensity;
                    Vec2d p = individual.generator_points[i];
                    Vec2d new_p = new Vec2d(
                        p.x + width_offset,
                        p.y + height_offset
                        );
                    individual.M__Move_Point(i, new_p, false);
                }
            }

            if (at_least_one_moved == false) {
                int random_index = rand.Next(num_points);
                double width_offset =
                        (rand.NextDouble() * 2.0 - 1.0)
                        * width
                        * mutation_intensity;
                double height_offset =
                    (rand.NextDouble() * 2.0 - 1.0)
                    * height
                    * mutation_intensity;
                Vec2d p = individual.generator_points[random_index];
                Vec2d new_p = new Vec2d(
                    p.x + width_offset,
                    p.y + height_offset
                    );
                individual.M__Move_Point(random_index, new_p, false);
            }

            individual.M__Recalculate__Phenotype();
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Move_Random_Points(this);
        }
    }
}
