using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Common_Tools
{
    public static partial class List_Extensions
    {

        public static T Q__Random_Item<T>(
            this HashSet<T> set,
            I_PRNG rand
            )
        {
            int random_selection_index = rand.Next(set.Count);
            return set.ElementAt(random_selection_index);
        }

        public static bool Q__Contains_All<T>(this List<T> mainList, List<T> containedList)
        {
            foreach (T item in containedList)
            {
                if (mainList.Contains(item) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<List<T>> Q__Get_Non_Same_Pairs<T>(this List<T> originalList)
        {
            if (originalList.Count < 2)
            {
                return new List<List<T>>();
            }
            else
            {
                int originalListSize = originalList.Count;
                List<List<T>> listOfPairs = new List<List<T>>();
                for (int i = 0; i < originalListSize; i++)
                {
                    for (int j = 0; j < originalListSize; j++)
                    {
                        if (i != j)
                        {
                            T element1 = originalList[i];
                            T element2 = originalList[j];
                            List<T> pair = new List<T>() {
                                element1, element2
                            };
                            listOfPairs.Add(pair);
                        }
                    }
                }
                return listOfPairs;
            }
        }

        public static void SetFirstItemByIndex<T>(this List<T> items, int index)
        {
            List<T> reorderedItems = new List<T>();
            for (int i = index; i < items.Count; i++)
            {
                reorderedItems.Add(items[i]);
            }
            for (int i = 0; i < index; i++)
            {
                reorderedItems.Add(items[i]);
            }

            items.Clear();

            foreach (var rItem in reorderedItems)
            {
                items.Add(rItem);
            }
        }

        public static void M__Shuffle<T>(
            this List<T> items,
            I_PRNG rand
            )
        {
            List<T> shuffledItems = new List<T>();

            for (int i = items.Count - 1; i >= 0; i--)
            {
                shuffledItems.Add(items.Pop_Random_Item(rand));
            }

            items.Clear();
            for (int i = shuffledItems.Count - 1; i >= 0; i--)
            {
                items.Add(shuffledItems.Pop_Item(i));
            }
        }
    }
}