using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Tools
{
    public static class Vec2d__Extensions
    {
        public static Vec2d[] Q__Deep_Copy(this Vec2d[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            Vec2d[] copy_array = new Vec2d[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static List<Vec2d>[] Q__Deep_Copy(this List<Vec2d>[] original_array)
        {
            if (original_array == null)
                return null;

            int length = original_array.Length;

            List<Vec2d>[] copy_array = new List<Vec2d>[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i].Q__Deep_Copy();
            }

            return copy_array;
        }

        public static Dictionary<int, Vec2d> Q__Deep_Copy(
            this Dictionary<int, Vec2d> original_dictionary
            )
        {
            Dictionary<int, Vec2d> dictionary_copy = new Dictionary<int, Vec2d>();

            foreach (var kvp in original_dictionary)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                dictionary_copy.Add(key, value);
            }

            return dictionary_copy;
        }

        public static Dictionary<int, List<Vec2d>> Q__Deep_Copy(
            this Dictionary<int, List<Vec2d>> original_dictionary
            )
        {
            Dictionary<int, List<Vec2d>> copy_dictionary =
                new Dictionary<int, List<Vec2d>>();

            foreach (var key in original_dictionary.Keys)
            {
                List<Vec2d> value = original_dictionary[key].Q__Deep_Copy();
                copy_dictionary.Add(key, value);
            }

            return copy_dictionary;
        }

        public static List<Vec2d> Q__Deep_Copy(this List<Vec2d> original_list)
        {
            return new List<Vec2d>(original_list);
        }

        public static List<List<Vec2d>> Q__Deep_Copy(this List<List<Vec2d>> original_list)
        {
            List<List<Vec2d>> copied_list = new List<List<Vec2d>>(
                capacity: original_list.Count
                );
            foreach (var sublist in original_list)
            {
                copied_list.Add(sublist.Q__Deep_Copy());
            }
            return copied_list;
        }
    }
}
