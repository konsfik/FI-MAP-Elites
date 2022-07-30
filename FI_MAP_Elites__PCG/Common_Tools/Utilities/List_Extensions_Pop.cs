using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common_Tools
{
    public static class List_Extensions_Pop
    {
        public static T Pop_Item<T>(
            this List<T> items,
            int index
            )
        {
            T selected_item = items[index];
            items.RemoveAt(index);
            return selected_item;
        }

        public static T Pop_First_Item<T>(this List<T> items)
        {
            int first_item_index = 0;

            T first_item = items[first_item_index];
            items.RemoveAt(first_item_index);

            return first_item;
        }

        public static T Pop_Last_Item<T>(this List<T> items)
        {
            int last_item_index = items.Count - 1;

            T last_item = items[last_item_index];
            items.RemoveAt(last_item_index);

            return last_item;
        }

        public static T Pop_Random_Item<T>(
            this List<T> items,
            System.Random rand
            )
        {
            int randomIndex = rand.Next(items.Count);
            T drawnItem = items[randomIndex];
            items.RemoveAt(randomIndex);

            return drawnItem;
        }

        public static T Pop_Random_Item<T>(
            this List<T> items,
            I_PRNG rand
            )
        {
            int randomIndex = rand.Next(items.Count);
            T drawnItem = items[randomIndex];
            items.RemoveAt(randomIndex);

            return drawnItem;
        }

        public static T Pop_Random_Item<T>(
            this HashSet<T> items,
            System.Random rand
            )
        {
            int random_selection_index = rand.Next(items.Count);

            T drawnItem = items.ElementAt(random_selection_index);
            items.Remove(drawnItem);

            return drawnItem;
        }

        public static T Pop_Random_Item<T>(
            this HashSet<T> items,
            I_PRNG rand
            )
        {
            int random_selection_index = rand.Next(items.Count);

            T drawnItem = items.ElementAt(random_selection_index);
            items.Remove(drawnItem);

            return drawnItem;
        }

        public static List<T> Pop_All_Items<T>(this List<T> items)
        {
            List<T> drawnItems = new List<T>(items);
            items.Clear();
            return drawnItems;
        }

        public static List<T> Pop_Random_Unique_Items<T>(
            this List<T> items,
            System.Random rand,
            int num_items
            )
        {
            List<T> selected_items = new List<T>();

            for (int i = 0; i < num_items; i++)
            {
                selected_items.Add(
                    items.Pop_Random_Item(rand)
                    );
            }

            return selected_items;
        }

        public static List<T> Pop_Random_Unique_Items<T>(
            this List<T> items,
            I_PRNG rand,
            int num_items
            )
        {
            List<T> selected_items = new List<T>();

            for (int i = 0; i < num_items; i++)
            {
                selected_items.Add(
                    items.Pop_Random_Item(rand)
                    );
            }

            return selected_items;
        }
    }
}
