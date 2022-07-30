using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class FIME__Archive<T> where T : Data_Structure
    {
        public void M__Place_Individual(
            int cell,
            T individual,
            double fitness,
            double[] feature_vector
            )
        {
            individuals__archive[cell] = (T)individual.Q__Deep_Copy();
            fitness__archive[cell] = fitness;
            feature_vectors__archive[cell] = feature_vector.Q__Deep_Copy();
        }

    }
}
