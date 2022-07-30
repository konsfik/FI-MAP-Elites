using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static partial class Array_1D_Extensions
    {
        public static T First_Item<T>(this T[] items)
        {
            return items[0];
        }

        public static T Last_Item<T>(this T[] items)
        {
            return items[items.Length - 1];
        }

        public static T Random_Item<T>(
            this T[] items,
            I_PRNG rand
            )
        {
            int random_selection_index = rand.Next(0, items.Length);
            return items[random_selection_index];
        }
    }
}
