using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.CMCE;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Visualization;

using SkiaSharp;



namespace FI_MAP_Elites__Experiments
{
    public static class FIME__Experiments
    {

        public static void Program__Draw()
        {
            //
            string input_folder = "_jsons";
            var file_paths = IO_Utilities.File_Paths_In_Folder(input_folder);
            var json_file_paths = file_paths.ToList().FindAll(x => x.Contains(".json"));

            CSM__Architectural_Plan_Json serialization_method = new CSM__Architectural_Plan_Json();

            foreach (var json_file_path in json_file_paths)
            {
                string json_data = IO_Utilities.Read_File_Text(json_file_path);
                //DS__Architectural_Plan ap = serialization_method.Deserialize_From_String(json_data);

                JsonSerializerOptions options = new JsonSerializerOptions();

                DS__Architectural_Plan? weatherForecast = JsonSerializer.Deserialize<DS__Architectural_Plan>(json_data);
                int b = 0;
            }



            var a = 0;
        }

        public static void Program__Cycle_Graph_Experiments()
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments__G1__Cycle_Graph),
            };
            delegator.Run(types, "Program_");
        }

        public static void Program__Star_Graph_Experiments()
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments__G2__Star_Graph),
            };
            delegator.Run(types, "Program_");
        }

        public static void Program__Wheel_Graph_Experiments()
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments__G3__Wheel_Graph),
            };
            delegator.Run(types, "Program_");
        }

        public static void Program__Path_Graph_Experiments()
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments__G4__Path_Graph),
            };
            delegator.Run(types, "Program_");
        }

        public static void Program__Double_Cycle_Graph_Experiments()
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments__G5__Double_Cycle_Graph),
            };
            delegator.Run(types, "Program_");
        }

        public static void Program__Double_Star_Graph_Experiments()
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments__G6__Double_Star_Graph),
            };
            delegator.Run(types, "Program_");
        }

        public static void Program__Double_Wheel_Graph_Experiments()
        {
            Delegator delegator = new Delegator();
            List<Type> types = new List<Type>() {
                typeof(FIME__Experiments__G7__Double_Wheel_Graph),
            };
            delegator.Run(types, "Program_");
        }
    }
}
