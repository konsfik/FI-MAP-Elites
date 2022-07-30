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
using System.Text.Json;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class CMCE__Experiment_Runner<T>
        where T : Data_Structure
    {
        public void On_Individual_Generated(
            object sender,
            FIME__Individual_Generated__EventArgs<T> event_args)
        {
            int num_evals = ((FI_MAP_Elites<T>)sender).num_evaluations;
            Util__Process_Data(num_evals);
        }

        public void On_Offspring_Generated(
            object sender,
            FIME__Offspring_Generated__EventArgs<T> event_args
            )
        {
            int num_evals = ((FI_MAP_Elites<T>)sender).num_evaluations;
            Util__Process_Data(num_evals);
        }
    }
}
