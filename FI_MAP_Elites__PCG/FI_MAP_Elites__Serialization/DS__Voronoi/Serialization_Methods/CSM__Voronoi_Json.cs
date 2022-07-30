using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class CSM__Voronoi_Json : Custom_Serialization_Method<DS__Voronoi>
    {
        public override object Q__Deep_Copy()
        {
            return new CSM__Voronoi_Json();
        }

        public override DS__Voronoi Deserialize_From_String(string serialized_individual)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = true;
            options.WriteIndented = true;

            DS__Voronoi voronoi = JsonSerializer.Deserialize<DS__Voronoi>(serialized_individual, options);

            return voronoi;
        }

        public override string Serialize_To_String(DS__Voronoi individual)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = true;
            options.WriteIndented = true;

            string serialized_geom = JsonSerializer.Serialize(individual, options);

            return serialized_geom;
        }

        public override string Q__File__Dot_Extension()
        {
            return ".json";
        }
    }
}
