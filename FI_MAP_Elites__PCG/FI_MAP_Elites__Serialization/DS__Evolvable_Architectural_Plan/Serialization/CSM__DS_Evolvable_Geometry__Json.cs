using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class CSM__DS_Evolvable_Geometry__Json : 
        Custom_Serialization_Method<DS__Architectural_Plan>
    {
        public override object Q__Deep_Copy()
        {
            return new CSM__DS_Evolvable_Geometry__Json();
        }

        public override DS__Architectural_Plan Deserialize_From_String(string serialized_individual)
        {
            throw new NotImplementedException();
            //JsonSerializerOptions options = new JsonSerializerOptions();
            //options.IncludeFields = true;
            //options.Converters.Add(
                
            //    );

            //DS__Evolvable_Geometry indi =
            //    (DS__Evolvable_Geometry)JsonConvert.DeserializeObject(serialized_individual);

            //return table;
        }

        public override string Serialize_To_String(DS__Architectural_Plan individual)
        {
            throw new NotImplementedException();
            //JsonSerializerSettings serializer_settings = new JsonSerializerSettings()
            //{
            //    Formatting = Formatting.Indented,
            //    TypeNameHandling = TypeNameHandling.All
            //};
            //string json_string = JsonConvert.SerializeObject(individual, serializer_settings);
            //return json_string;
        }

        public override string Q__File__Dot_Extension()
        {
            return ".json";
        }
    }
}
