using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class FIME__Experiments__G7__Double_Wheel_Graph
    {
        public static void Program__Wheel_Star_Graph__All()
        {
            Program__Double_Wheel_Graph__Ortho_Tessellation__Size_All();
            Program__Double_Wheel_Graph__Hex_Tessellation__Size_All();
            Program__Double_Wheel_Graph__Random_Tessellation__Size_All();
        }

        public static void Program__Double_Wheel_Graph__Ortho_Tessellation__Size_All()
        {
            Program__Double_Wheel_Graph__Ortho_Tessellation__Half_Size_4();
            Program__Double_Wheel_Graph__Ortho_Tessellation__Half_Size_5();
        }

        public static void Program__Double_Wheel_Graph__Hex_Tessellation__Size_All()
        {
            Program__Double_Wheel_Graph__Hex_Tessellation__Half_Size_4();
            Program__Double_Wheel_Graph__Hex_Tessellation__Half_Size_5();
        }

        public static void Program__Double_Wheel_Graph__Random_Tessellation__Size_All()
        {
            Program__Double_Wheel_Graph__Random_Tessellation__Half_Size_4();
            Program__Double_Wheel_Graph__Random_Tessellation__Half_Size_5();
        }

        public static void Program__Double_Wheel_Graph__Ortho_Tessellation__Half_Size_4()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(4);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.ORTHO,
                    output_folder: output_folder_path
                    );
            }
        }

        public static void Program__Double_Wheel_Graph__Ortho_Tessellation__Half_Size_5()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(5);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.ORTHO,
                    output_folder: output_folder_path
                    );
            }
        }

        public static void Program__Double_Wheel_Graph__Ortho_Tessellation__Half_Size_6()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(6);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.ORTHO,
                    output_folder: output_folder_path
                    );
            }
        }


        public static void Program__Double_Wheel_Graph__Hex_Tessellation__Half_Size_4()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(4);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.HEX,
                    output_folder: output_folder_path
                    );
            }
        }

        public static void Program__Double_Wheel_Graph__Hex_Tessellation__Half_Size_5()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(5);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.HEX,
                    output_folder: output_folder_path
                    );
            }
        }

        public static void Program__Double_Wheel_Graph__Hex_Tessellation__Half_Size_6()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(6);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.HEX,
                    output_folder: output_folder_path
                    );
            }
        }


        public static void Program__Double_Wheel_Graph__Random_Tessellation__Half_Size_4()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(4);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.RANDOM,
                    output_folder: output_folder_path
                    );
            }
        }

        public static void Program__Double_Wheel_Graph__Random_Tessellation__Half_Size_5()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(5);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.RANDOM,
                    output_folder: output_folder_path
                    );
            }
        }

        public static void Program__Double_Wheel_Graph__Random_Tessellation__Half_Size_6()
        {
            for (int i = 0; i < FIME_Experiments_Utils.Q__Num_Repetitions(); i++)
            {
                string output_folder_path =
                System.Reflection.MethodBase.GetCurrentMethod().Name + "__" + DateTime.Now.Ticks.ToString();
                IO_Utilities.CreateFolder(output_folder_path);

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(6);

                FIME_Experiments_Utils.Run__CMCE__Experiment(
                    layout_constraints: layout_constraints,
                    tessellation_type: Tessellation_Type.RANDOM,
                    output_folder: output_folder_path
                    );
            }
        }
    }
}
