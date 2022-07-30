using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Architectural_Plan : DS__Evolvable_Geometry
    {


        public void M__Remove__Connection_Door(
            EG__Connection_Door connection_door,
            bool recalculate_phenotype
            )
        {
            connection_doors.Remove(connection_door);
            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 3);
            }
        }

        public void M__Remove__Entrance_Door(
            EG__Entrance_Door entrance_door,
            bool recalculate_phenotype
            )
        {
            entrance_doors.Remove(entrance_door);
            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 3);
            }
        }

        public void M__Remove__Window(
            EG__Window window,
            bool recalculate_phenotype
            )
        {
            windows.Remove(window);
            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 3);
            }
        }

        public void M__Add__Connection_Door(
            EG__Connection_Door connection_door,
            bool recalculate_phenotype
            )
        {
            connection_doors.Add(connection_door);
            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 3);
            }
        }

        public void M__Add__Entrance_Door(
            EG__Entrance_Door entrance_door,
            bool recalculate_phenotype
            )
        {
            entrance_doors.Add(entrance_door);
            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 3);
            }
        }

        public void M__Add__Window(
            EG__Window window,
            bool recalculate_phenotype
            )
        {
            windows.Add(window);
            if (recalculate_phenotype)
            {
                M__Recalculate_Phenotype(starting_level: 3);
            }
        }

        protected void M__Recalculate_Phenotype__L3__Details()
        {
            plan_graph.Update(this);
        }

        public override void M__Recalculate_Phenotype(int starting_level)
        {
            if (starting_level == 1)
            {
                M__Recalculate__Phenotype__L1__Voronoi();
                M__Recalculate__Phenotype__L2__Geometry();
                M__Recalculate_Phenotype__L3__Details();
            }
            else if (starting_level == 2)
            {
                M__Recalculate__Phenotype__L2__Geometry();
                M__Recalculate_Phenotype__L3__Details();
            }
            else if (starting_level == 3)
            {
                M__Recalculate_Phenotype__L3__Details();
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}
