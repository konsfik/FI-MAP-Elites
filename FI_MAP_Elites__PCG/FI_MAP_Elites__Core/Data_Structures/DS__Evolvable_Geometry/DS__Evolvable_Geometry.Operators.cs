using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Evolvable_Geometry : Data_Structure
    {
        // L1
        public virtual void M__Move_Point(
            int point_index,
            Vec2d new_location,
            bool recalculate_phenotype
            )
        {
            voronoi_tessellation.M__Move_Point(
                point_index,
                new_location,
                false
                );

            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 1);
            }
        }

        // L2
        public virtual void M__Assign_Cell_To_Space_Unit(
            int cell,
            int space_unit,
            bool recalculate_phenotype
            )
        {
            space_unit__per__cell[cell] = space_unit;
            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 2);
            }
        }

        public virtual void M__Assign_Cells_To_Space_Unit(
            List<int> cells,
            int space_unit,
            bool recalculate_phenotype
            )
        {
            foreach (int cell in cells)
            {
                M__Assign_Cell_To_Space_Unit(
                    cell: cell,
                    space_unit: space_unit,
                    recalculate_phenotype: false
                    );
            }

            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 2);
            }
        }

        public virtual void M__Clear_Cell(
            int cell,
            bool recalculate_phenotype
            )
        {
            space_unit__per__cell[cell] = -1;

            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 2);
            }
        }

        /// <summary>
        /// Disassociates all cells from a specific space_unit. 
        /// In other words, deletes that space_unit.
        /// </summary>
        /// <param name="space_unit"></param>
        public void M__Delete_Space_Unit(
            int space_unit,
            bool recalculate_phenotype
            )
        {
            var cells_to_clear = Q__Space_Unit__Cells(space_unit);

            foreach (var cell in cells_to_clear)
            {
                M__Clear_Cell(cell, false);
            }

            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 2);
            }
        }

        protected virtual void M__Recalculate__Phenotype__L1__Voronoi()
        {
            voronoi_tessellation.M__Recalculate__Phenotype();
        }

        protected virtual void M__Recalculate__Phenotype__L2__Geometry()
        {
            int num_points = voronoi_tessellation.Q__Num_Generator_Points();
            for (int i = 0; i < num_points; i++)
            {
                if (Q__Is_Cell__Active(i) == false)
                {
                    space_unit__per__cell[i] = -1;
                }
            }
        }

        public virtual void M__Recalculate_Phenotype(
            int starting_level
            )
        {
            if (starting_level == 1)
            {
                M__Recalculate__Phenotype__L1__Voronoi();
                M__Recalculate__Phenotype__L2__Geometry();
            }
            else if (starting_level == 2)
            {
                M__Recalculate__Phenotype__L2__Geometry();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}
