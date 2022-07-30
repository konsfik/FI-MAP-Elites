using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static partial class List_Extensions
    {
        public static List<T> Q__Deep_Copy<T>(this List<T> original_list)
            where T : I_Deep_Copyable
        {
            List<T> copied_list = new List<T>(original_list.Count); // initialize the list's capacity
            for (int i = 0; i < original_list.Count; i++)
            {
                copied_list.Add((T)original_list[i].Q__Deep_Copy());
            }
            return copied_list;
        }

        public static List<List<T>> Q__Deep_Copy<T>(this List<List<T>> original_list)
            where T : I_Deep_Copyable
        {
            List<List<T>> copied_list = new List<List<T>>(
                capacity: original_list.Count
                );
            foreach (var sublist in original_list)
            {
                copied_list.Add(sublist.Q__Deep_Copy());
            }
            return copied_list;
        }



        public static List<double> Q__Deep_Copy(this List<double> original_list)
        {
            return new List<double>(original_list);
        }

        public static List<List<double>> Q__Deep_Copy(this List<List<double>> original_list)
        {
            List<List<double>> copy_list = new List<List<double>>(capacity: original_list.Count);
            foreach (var sub_list in original_list)
            {
                copy_list.Add(sub_list.Q__Deep_Copy());
            }
            return copy_list;
        }

        public static List<float> Q__Deep_Copy(this List<float> original_list)
        {
            return new List<float>(original_list);
        }

        public static List<int> Q__Deep_Copy(this List<int> original_list)
        {
            return new List<int>(original_list);
        }

        public static List<string> Q__Deep_Copy(this List<string> original_list)
        {
            return new List<string>(original_list);
        }

        public static List<bool> Q__Deep_Copy(this List<bool> original_list)
        {
            return new List<bool>(original_list);
        }

        public static List<Vec2i> Q__Deep_Copy(this List<Vec2i> original_list)
        {
            return new List<Vec2i>(original_list);
        }

        public static List<List<Line_Segment>> Q__Deep_Copy(this List<List<Line_Segment>> original_list)
        {
            List<List<Line_Segment>> copied_list = new List<List<Line_Segment>>(
                capacity: original_list.Count
                );
            foreach (var sublist in original_list)
            {
                copied_list.Add(sublist.Q__Deep_Copy());
            }
            return copied_list;
        }

        public static List<Line_Segment> Q__Deep_Copy(this List<Line_Segment> original_list)
        {
            return new List<Line_Segment>(original_list);
        }

        public static List<UEdge_i> Q__Deep_Copy(this List<UEdge_i> original_list)
        {
            return new List<UEdge_i>(original_list);
        }

        public static List<Guid> Q__DeepCopy(this List<Guid> original_list)
        {
            return new List<Guid>(original_list);
        }

        public static List<Directions_Ortho_2D> Q__Deep_Copy(this List<Directions_Ortho_2D> original_list)
        {
            return new List<Directions_Ortho_2D>(original_list);
        }

    }
}
