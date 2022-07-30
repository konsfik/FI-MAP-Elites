using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class CSM__Architectural_Plan_Json : Custom_Serialization_Method<DS__Architectural_Plan>
    {
        public override object Q__Deep_Copy()
        {
            return new CSM__Architectural_Plan_Json();
        }

        public override DS__Architectural_Plan Deserialize_From_String(string serialized_individual)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = true;
            options.WriteIndented = true;

            DS__Architectural_Plan voronoi = JsonSerializer.Deserialize<DS__Architectural_Plan>(serialized_individual, options);

            return voronoi;
        }

        public override string Serialize_To_String(DS__Architectural_Plan individual)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = true;
            options.WriteIndented = true;

            string serialized_plan = JsonSerializer.Serialize(individual, options);

            return serialized_plan;
        }

        public override string Q__File__Dot_Extension()
        {
            return ".json";
        }
    }
}
