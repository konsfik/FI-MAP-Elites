using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;
using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public abstract class PSM__Windowed<T> : Parent_Selection_Method<T>
        where T : Data_Structure
    {
        public Selection_Window selection_window;

        public PSM__Windowed()
        {
            this.selection_window = null;
        }

        public PSM__Windowed(Selection_Window selection_window)
        {
            if (selection_window == null)
                this.selection_window = null;
            else
                this.selection_window = (Selection_Window)selection_window.Q__Deep_Copy();
        }

        public abstract override object Q__Deep_Copy();

        public virtual void M__Set_Selection_Window(Selection_Window new_selection_window)
        {
            this.selection_window = (Selection_Window)new_selection_window.Q__Deep_Copy();
        }

        public abstract override FIME__Location Select__Parent_Location(I_PRNG rand, FI_MAP_Elites<T> algorithm);
    }
}
