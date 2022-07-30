using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Destruction__L1__Move_All_Voronoi_Points<T> : Mutation_Method<T>
        where T : DS__Evolvable_Geometry
    {
        public readonly double cell_mutation_intensity;
        public readonly bool recalculate_phenotype;

        public MM__Destruction__L1__Move_All_Voronoi_Points(
            double cell_mutation_intensity,
            bool recalculate_phenotype
            )
        {
            if (cell_mutation_intensity < 0.0 || cell_mutation_intensity > 1.0)
                throw new Exception("cell_mutation_intensity must be between 0 and 1");

            this.cell_mutation_intensity = cell_mutation_intensity;
            this.recalculate_phenotype = recalculate_phenotype;
        }

        private MM__Destruction__L1__Move_All_Voronoi_Points(
            MM__Destruction__L1__Move_All_Voronoi_Points<T> mm_to_copy
            )
        {
            this.cell_mutation_intensity = mm_to_copy.cell_mutation_intensity;
            this.recalculate_phenotype = mm_to_copy.recalculate_phenotype;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Destruction__L1__Move_All_Voronoi_Points<T>(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            T individual
            )
        {
            Rect2d bounding_rect = individual.voronoi_tessellation.bounding_rectangle;

            double movement_x =
                (rand.NextDouble() * 2 - 1)
                * bounding_rect.width
                * cell_mutation_intensity;

            double movement_y =
                (rand.NextDouble() * 2 - 1)
                * bounding_rect.width
                * cell_mutation_intensity;

            Vec2d movement_vector = new Vec2d(movement_x, movement_y);

            int num_cells = individual.voronoi_tessellation.Q__Num_Generator_Points();
            for (int i = 0; i < num_cells; i++)
            {
                Vec2d point = individual.voronoi_tessellation.generator_points[i];

                point = point + movement_vector;

                individual.M__Move_Point(i, point, false);
            }

            if (recalculate_phenotype)
            {
                individual.M__Recalculate_Phenotype(starting_level: 1);
            }
        }
    }
}
