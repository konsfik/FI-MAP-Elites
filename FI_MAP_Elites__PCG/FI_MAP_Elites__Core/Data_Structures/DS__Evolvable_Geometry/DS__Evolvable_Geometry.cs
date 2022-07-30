using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Voronoi;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Evolvable_Geometry : Data_Structure
    {
        // problem specification
        public DS__Layout_Constraints prescription;

        // state
        public DS__Voronoi voronoi_tessellation;
        public Dictionary<int, int> space_unit__per__cell;

        // phenotype / BCs


        // json constructor
        public DS__Evolvable_Geometry(
            DS__Layout_Constraints prescription,
            // genotype
            DS__Voronoi voronoi_tessellation,
            Dictionary<int, int> space_unit__per__cell
            )
        {
            // context
            this.prescription = (DS__Layout_Constraints)prescription.Q__Deep_Copy();

            // geno
            this.voronoi_tessellation = (DS__Voronoi)voronoi_tessellation.Q__Deep_Copy();
            this.space_unit__per__cell = space_unit__per__cell.Q__Deep_Copy();
        }

        public DS__Evolvable_Geometry(
            DS__Layout_Constraints prescription,
            Rect2d bounding_rect,
            List<Vec2d> point__per__cell_id
            )
        {
            this.prescription = (DS__Layout_Constraints)prescription.Q__Deep_Copy();

            voronoi_tessellation = new DS__Voronoi(
                point__per__cell_id,
                bounding_rect,
                prescription.connectivity_threshold
                );

            int num_cells = voronoi_tessellation.Q__Num_Generator_Points();

            // initialize the space unit ids to value of -1 (unassigned)
            space_unit__per__cell = new Dictionary<int, int>();
            for (int c = 0; c < num_cells; c++)
            {
                space_unit__per__cell.Add(c, -1);
            }
        }

        protected DS__Evolvable_Geometry(DS__Evolvable_Geometry solution_to_copy)
        {
            // context
            this.prescription = (DS__Layout_Constraints)solution_to_copy.prescription.Q__Deep_Copy();

            // geno
            this.voronoi_tessellation = (DS__Voronoi)solution_to_copy.voronoi_tessellation.Q__Deep_Copy();
            this.space_unit__per__cell = solution_to_copy.space_unit__per__cell.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new DS__Evolvable_Geometry(this);
        }
    }
}
