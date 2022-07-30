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
        public List<int> save_stats_evals;
        BS_Ortho_Tessellation tessellation;
        string output_folder;

        public int[] individual_exists__on_FS;
        public int[] individual_exists__on_IS;

        public double[] fitness__on_FS;
        public double[] fitness__on_IS;

        public int[] num_landings__on_FS;
        public int[] num_landings__on_IS;

        public int[] num_survivals__on_FS;
        public int[] num_survivals__on_IS;

        public int[] num_selections__on_FS;
        public int[] num_selections__on_IS;


        public int num_evals;
        public bool first_feasible_found;

        public CMCE_Stats__Basic(
            List<int> save_stats_evals,
            BS_Ortho_Tessellation tessellation,
            string output_folder
            )
        {

            this.save_stats_evals = save_stats_evals.Q__Deep_Copy();
            this.tessellation = (BS_Ortho_Tessellation)tessellation.Q__Deep_Copy();
            this.output_folder = output_folder;

            int num_cells = tessellation.num_cells;

            this.individual_exists__on_FS = new int[num_cells];
            this.individual_exists__on_IS = new int[num_cells];

            this.fitness__on_FS = new double[num_cells];
            this.fitness__on_IS = new double[num_cells];

            this.num_landings__on_FS = new int[num_cells];
            this.num_landings__on_IS = new int[num_cells];

            this.num_survivals__on_FS = new int[num_cells];
            this.num_survivals__on_IS = new int[num_cells];

            this.num_selections__on_FS = new int[num_cells];
            this.num_selections__on_IS = new int[num_cells];


            for (int i = 0; i < num_cells; i++)
            {
                this.individual_exists__on_FS[i] = 0;
                this.individual_exists__on_IS[i] = 0;

                this.fitness__on_FS[i] = double.NaN;
                this.fitness__on_IS[i] = double.NaN;

                this.num_landings__on_FS[i] = 0;
                this.num_landings__on_IS[i] = 0;

                this.num_survivals__on_FS[i] = 0;
                this.num_survivals__on_IS[i] = 0;

                this.num_selections__on_FS[i] = 0;
                this.num_selections__on_IS[i] = 0;
            }

            num_evals = 0;
            first_feasible_found = false;
        }

        public void Save_Data_Tables_And_Summary()
        {
            Save_Data_Table__CSV__Individual_Exists__On_FS();
            Save_Data_Table__CSV__Individual_Exists__On_IS();

            Save_Data_Table__CSV__Fitness__On_FS();
            Save_Data_Table__CSV__Fitness__On_IS();

            Save_Data_Table__CSV__Num_Landings__On_FS();
            Save_Data_Table__CSV__Num_Landings__On_IS();

            Save_Data_Table__CSV__Num_Survivals__On_FS();
            Save_Data_Table__CSV__Num_Survivals__On_IS();

            Save_Data_Table__CSV__Num_Selections__On_FS();
            Save_Data_Table__CSV__Num_Selections__On_IS();

            Calculate_And_Save_Summary();
        }

    }
}
