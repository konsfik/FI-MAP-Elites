using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class MM__Repair__L2__Space_Unit_Area__Increase_Decrease_Threshold<T> : Mutation_Method<T>
        where T: DS__Evolvable_Geometry
    {
        //
        // margin is a percnetage (of being smaller or larger)
        public readonly double error_margin;

        public MM__Repair__L2__Space_Unit_Area__Increase_Decrease_Threshold(
            double error_margin
            )
        {
            if (error_margin < 0.0 || error_margin > 1.0)
                throw new Exception("area threschold should be between 0...1");

            this.error_margin = error_margin;
        }

        private MM__Repair__L2__Space_Unit_Area__Increase_Decrease_Threshold(
            MM__Repair__L2__Space_Unit_Area__Increase_Decrease_Threshold<T> mm_to_copy)
        {
            this.error_margin = mm_to_copy.error_margin;
        }

        public override object Q__Deep_Copy()
        {
            return new MM__Repair__L2__Space_Unit_Area__Increase_Decrease_Threshold<T>(this);
        }

        public override void Mutate_Individual(
            I_PRNG rand,
            T individual
            )
        {
            List<int> space_units = individual.Q__Existing_Space_Units();

            space_units.M__Shuffle(rand);

            foreach (int space_unit in space_units)
            {
                double space_unit_area = individual.Q__Space_Unit__Area(space_unit);
                double target_space_unit_area = individual.Q__Space_Unit__Prescribed_Area(space_unit);

                double similarity = space_unit_area.Q__Fractional_Similarity(target_space_unit_area);
                double error = 1.0 - similarity;

                if (error > error_margin)
                {
                    if (space_unit_area < target_space_unit_area)
                    {
                        while (
                            space_unit_area < target_space_unit_area
                            &&
                            error > error_margin
                            )
                        {
                            // add one cell
                            List<int> expansion_cells =
                                individual
                                .Q__Space_Unit__Surrounding_Cells__Free(space_unit);

                            if (expansion_cells.Count > 0)
                            {
                                int expansion_cell = expansion_cells.Q__Random_Item(rand);
                                individual.M__Assign_Cell_To_Space_Unit(
                                    expansion_cell, 
                                    space_unit,
                                    false
                                    );
                            }
                            else break;

                            // recalculate the area
                            space_unit_area = individual.Q__Space_Unit__Area(space_unit);
                            // recalculate the error
                            similarity = space_unit_area.Q__Fractional_Similarity(target_space_unit_area);
                            error = 1.0 - similarity;
                        }
                    }
                    else if (space_unit_area > target_space_unit_area)
                    {
                        while (
                            space_unit_area > target_space_unit_area
                            &&
                            error > error_margin
                            )
                        {
                            List<int> safely_destroyable_cells =
                                individual
                                .Q__Safely_Destroyable_Cells(space_unit);

                            if (safely_destroyable_cells.Count > 0)
                            {
                                int cell = safely_destroyable_cells.Q__Random_Item(rand);
                                individual.M__Clear_Cell(cell, false);
                            }
                            else break;

                            // recalculate the area
                            space_unit_area = individual.Q__Space_Unit__Area(space_unit);
                            // recalculate the error
                            similarity = space_unit_area.Q__Fractional_Similarity(target_space_unit_area);
                            error = 1.0 - similarity;
                        }
                    }
                }
                else
                {
                    continue;
                }

            }

            individual.M__Recalculate_Phenotype(starting_level: 2);
        }
    }
}
