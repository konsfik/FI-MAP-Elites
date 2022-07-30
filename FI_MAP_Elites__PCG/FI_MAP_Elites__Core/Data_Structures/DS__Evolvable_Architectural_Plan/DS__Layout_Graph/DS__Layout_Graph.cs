using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Layout_Constraints : Data_Structure
    {
        public string name;

        // graph structure
        public DS__Undirected_Graph graph;

        // graph properties
        public Dictionary<int, string> name__per__space_unit;
        public Dictionary<int, RGB_Color> color__per__space_unit;
        public Dictionary<int, Space_Unit__Type> type__per__space_unit;
        public Dictionary<int, double> area__per__space_unit;
        public Dictionary<int, int> num_windows__per__space_unit;
        public Dictionary<int, int> num_entrance_doors__per__space_unit;

        // extra properties
        public double openings__minimum_wall_length;
        public double connectivity_threshold;

        public DS__Layout_Constraints(
            string name,
            double openings__minimum_wall_length,
            double connectivity_threshold
            )
        {
            this.name = name;
            this.graph = new DS__Undirected_Graph();

            this.name__per__space_unit = new Dictionary<int, string>();
            this.color__per__space_unit = new Dictionary<int, RGB_Color>();
            this.type__per__space_unit = new Dictionary<int, Space_Unit__Type>();
            this.area__per__space_unit = new Dictionary<int, double>();
            this.num_windows__per__space_unit = new Dictionary<int, int>();
            this.num_entrance_doors__per__space_unit = new Dictionary<int, int>();
            this.openings__minimum_wall_length = openings__minimum_wall_length;
            this.connectivity_threshold = connectivity_threshold;
        }

        /// <summary>
        /// Private copy constructor: used for deep copy purposes, only.
        /// </summary>
        /// <param name="prescription_to_copy"></param>
        private DS__Layout_Constraints(
            DS__Layout_Constraints prescription_to_copy
            )
        {
            this.name = prescription_to_copy.name;
            this.graph =
                (DS__Undirected_Graph)prescription_to_copy.graph.Q__Deep_Copy();

            this.name__per__space_unit = prescription_to_copy.name__per__space_unit.Q__Deep_Copy();
            this.color__per__space_unit = prescription_to_copy.color__per__space_unit.Q__Deep_Copy();
            this.type__per__space_unit = prescription_to_copy.type__per__space_unit.Q__Deep_Copy();
            this.area__per__space_unit = prescription_to_copy.area__per__space_unit.Q__Deep_Copy();
            this.num_windows__per__space_unit = prescription_to_copy.num_windows__per__space_unit.Q__Deep_Copy();
            this.num_entrance_doors__per__space_unit = prescription_to_copy.num_entrance_doors__per__space_unit.Q__Deep_Copy();
            this.openings__minimum_wall_length = prescription_to_copy.openings__minimum_wall_length;
            this.connectivity_threshold = prescription_to_copy.connectivity_threshold;
        }

        // json constructor
        public DS__Layout_Constraints(
            string name,
            DS__Undirected_Graph connectivity_graph,
            Dictionary<int, string> name__per__space_unit,
            Dictionary<int, RGB_Color> color__per__space_unit,
            Dictionary<int, Space_Unit__Type> type__per__space_unit,
            Dictionary<int, double> area__per__space_unit,
            Dictionary<int, int> num_windows__per__space_unit,
            Dictionary<int, int> num_entrance_doors__per__space_unit,
            double openings__minimum_wall_length,
            double connectivity_threshold
            )
        {
            this.name = name;
            this.graph = (DS__Undirected_Graph)connectivity_graph.Q__Deep_Copy();
            this.name__per__space_unit = name__per__space_unit.Q__Deep_Copy();
            this.color__per__space_unit = color__per__space_unit.Q__Deep_Copy();
            this.type__per__space_unit = type__per__space_unit.Q__Deep_Copy();
            this.area__per__space_unit = area__per__space_unit.Q__Deep_Copy();
            this.num_windows__per__space_unit = num_windows__per__space_unit.Q__Deep_Copy();
            this.num_entrance_doors__per__space_unit = num_entrance_doors__per__space_unit.Q__Deep_Copy();
            this.openings__minimum_wall_length = openings__minimum_wall_length;
            this.connectivity_threshold = connectivity_threshold;
        }

        public override object Q__Deep_Copy()
        {
            return new DS__Layout_Constraints(this);
        }

    }
}
