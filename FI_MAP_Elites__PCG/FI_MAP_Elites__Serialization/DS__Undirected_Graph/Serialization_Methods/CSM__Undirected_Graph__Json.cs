using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class CSM__Undirected_Graph__Json : Custom_Serialization_Method<DS__Undirected_Graph>
    {
        public override object Q__Deep_Copy()
        {
            return new CSM__Undirected_Graph__Json();
        }

        public override DS__Undirected_Graph Deserialize_From_String(string serialized_individual)
        {
            throw new NotImplementedException();
            //DS__Undirected_Graph undircted_graph =
            //    (DS__Undirected_Graph)JsonConvert.DeserializeObject(serialized_individual);
            //return undircted_graph;
        }

        public override string Serialize_To_String(DS__Undirected_Graph individual)
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
