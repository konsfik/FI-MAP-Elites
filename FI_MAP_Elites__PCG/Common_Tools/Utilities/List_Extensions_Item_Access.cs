using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class List_Extensions_Item_Access
    {
        public static T Q__First<T>(this List<T> items) {
            return items[0];
        }

        public static T Q__Last<T>(this List<T> items)
        {
            return items[items.Count - 1];
        }

        public static T Q__Random_Item<T>(
            this List<T> items,
            I_PRNG rand
            )
        {
            int random_selection_index = rand.Next(0, items.Count);
            return items[random_selection_index];
        }

        public static List<T> Q__Random_Items<T>(
            this List<T> items,
            I_PRNG rand,
            int num_items)
        {
            List<T> random_items = new List<T>();

            for (int i = 0; i < num_items; i++)
            {
                int random_index = rand.Next(items.Count);
                random_items.Add(items[random_index]);
            }

            return random_items;
        }

        public static List<T> Q__Random_Unique_Items<T>(
            this List<T> items,
            I_PRNG rand,
            int num_items
            )
        {
            List<T> temp_list = new List<T>(items);

            List<T> random_unique_elements = new List<T>();

            for (int i = 0; i < num_items; i++)
            {
                if (temp_list.Count == 0)
                    break;
                else
                    random_unique_elements.Add(
                        temp_list.Pop_Random_Item(rand)
                        );
            }

            return random_unique_elements;
        }

    }
}
