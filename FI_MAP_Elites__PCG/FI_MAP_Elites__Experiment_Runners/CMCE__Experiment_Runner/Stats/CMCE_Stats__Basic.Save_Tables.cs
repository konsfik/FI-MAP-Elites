using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Visualization;
using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

using SkiaSharp;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class CMCE_Stats__Basic<T>
        where T : Data_Structure
    {
        private void Save_Data_Table__CSV__Individual_Exists__On_FS()
        {
            string local_output_folder =
                output_folder
                + "\\individual_exists__on_FS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\individual_exists__on_FS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                individual_exists__on_FS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }

        private void Save_Data_Table__CSV__Individual_Exists__On_IS()
        {
            string local_output_folder =
                output_folder
                + "\\individual_exists__on_IS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\individual_exists__on_IS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                individual_exists__on_IS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }


        private void Save_Data_Table__CSV__Fitness__On_FS()
        {
            string local_output_folder =
                output_folder
                + "\\fitness__on_FS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\fitness__on_FS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                fitness__on_FS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }

        private void Save_Data_Table__CSV__Fitness__On_IS()
        {
            string local_output_folder =
                output_folder
                + "\\fitness__on_IS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\fitness__on_IS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                fitness__on_IS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }


        private void Save_Data_Table__CSV__Num_Landings__On_FS()
        {
            string local_output_folder =
                output_folder
                + "\\num_landings__on_FS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\num_landings__on_FS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                num_landings__on_FS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }

        private void Save_Data_Table__CSV__Num_Landings__On_IS()
        {
            string local_output_folder =
                output_folder
                + "\\num_landings__on_IS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\num_landings__on_IS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                num_landings__on_IS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }


        private void Save_Data_Table__CSV__Num_Survivals__On_FS()
        {
            string local_output_folder =
                output_folder
                + "\\num_survivals__on_FS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\num_survivals__on_FS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                num_survivals__on_FS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }

        private void Save_Data_Table__CSV__Num_Survivals__On_IS()
        {
            string local_output_folder =
                output_folder
                + "\\num_survivals__on_IS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\num_survivals__on_IS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                num_survivals__on_IS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }


        private void Save_Data_Table__CSV__Num_Selections__On_FS()
        {
            string local_output_folder =
                output_folder
                + "\\num_selections__on_FS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\num_selections__on_FS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                num_selections__on_FS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }

        private void Save_Data_Table__CSV__Num_Selections__On_IS()
        {
            string local_output_folder =
                output_folder
                + "\\num_selections__on_IS";

            IO_Utilities.CreateFolder(local_output_folder);

            string file_path =
                local_output_folder
                + "\\num_selections__on_IS__"
                + num_evals.ToString()
                + ".csv";

            string csv_data =
                num_selections__on_IS.To_CSV(tessellation);

            IO_Utilities.Append_To_File(
                file_path,
                csv_data
                );
        }
    }
}
