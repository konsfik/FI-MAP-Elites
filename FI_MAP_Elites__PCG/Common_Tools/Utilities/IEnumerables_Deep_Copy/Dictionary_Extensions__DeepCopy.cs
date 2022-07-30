using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools
{
    public static class Dictionary_Extensions__DeepCopy
    {
        

        public static Dictionary<int, int> Q__Deep_Copy(
            this Dictionary<int, int> original_dictionary
            )
        {
            return new Dictionary<int, int>(original_dictionary);
        }

        public static Dictionary<int, bool> Q__Deep_Copy(
            this Dictionary<int, bool> original_dictionary
            )
        {
            return new Dictionary<int, bool>(original_dictionary);
        }

        public static Dictionary<int, double> Q__Deep_Copy(
            this Dictionary<int, double> original_dictionary
            )
        {
            return new Dictionary<int, double>(original_dictionary);
        }

        public static Dictionary<int, Dictionary<int, double>> Q__Deep_Copy(
            this Dictionary<int, Dictionary<int, double>> original_dictionary
            )
        {
            Dictionary<int, Dictionary<int, double>> copy = new Dictionary<int, Dictionary<int, double>>();
            foreach (var kvp in original_dictionary) {
                copy.Add(
                    kvp.Key,
                    kvp.Value.Q__Deep_Copy()
                    );
            }

            return copy;
        }

        public static Dictionary<int, string> Q__Deep_Copy(
            this Dictionary<int, string> original_dictionary
            )
        {
            return new Dictionary<int, string>(original_dictionary);
        }

        public static Dictionary<int, T> Q__Deep_Copy<T>(
            this Dictionary<int, T> original_dictionary
            )
            where T:struct
        {
            return new Dictionary<int, T>(original_dictionary);
        }

        public static Dictionary<int, List<int>> Q__Deep_Copy(
            this Dictionary<int, List<int>> original_dictionary
            )
        {
            Dictionary<int, List<int>> copy_dictionary = 
                new Dictionary<int, List<int>>();

            foreach (var key in original_dictionary.Keys)
            {
                List<int> value = original_dictionary[key].Q__Deep_Copy();
                copy_dictionary.Add(key, value);
            }

            return copy_dictionary;
        }



        public static Dictionary<int, List<Line_Segment>> DeepCopy(
            this Dictionary<int, List<Line_Segment>> original_dictionary
            )
        {
            Dictionary<int, List<Line_Segment>> copy_dictionary = 
                new Dictionary<int, List<Line_Segment>>();

            foreach (var key in original_dictionary.Keys)
            {
                List<Line_Segment> value = original_dictionary[key].Q__Deep_Copy();
                copy_dictionary.Add(key, value);
            }

            return copy_dictionary;
        }

        public static Dictionary<string, string> Q__Deep_Copy(
            this Dictionary<string, string> original_dictionary
            )
        {
            return new Dictionary<string, string>(original_dictionary);
        }

        public static Dictionary<string, List<string>> Q__Deep_Copy(
            this Dictionary<string, List<string>> original_dictionary
            )
        {
            Dictionary<string, List<string>> copied_dictionary = new Dictionary<string, List<string>>();

            foreach (var kvp in original_dictionary)
            {
                copied_dictionary.Add(
                    kvp.Key,
                    kvp.Value.Q__Deep_Copy()
                    );
            }

            return copied_dictionary;
        }

        public static Dictionary<string, List<double>> Q__Deep_Copy(
            this Dictionary<string, List<double>> original_dictionary
            )
        {
            Dictionary<string, List<double>> copied_dictionary =
                new Dictionary<string, List<double>>();

            foreach (var kvp in original_dictionary)
            {
                copied_dictionary.Add(
                    kvp.Key,
                    kvp.Value.Q__Deep_Copy()
                    );
            }

            return copied_dictionary;
        }

        public static Dictionary<Guid, Guid> Q__Deep_Copy(
            this Dictionary<Guid, Guid> original_dictionary
            )
        {
            return new Dictionary<Guid, Guid>(original_dictionary);
        }

        public static Dictionary<Guid, int> Q__Deep_Copy(
            this Dictionary<Guid, int> original_dictionary
            )
        {
            return new Dictionary<Guid, int>(original_dictionary);
        }

        public static Dictionary<Guid, List<Guid>> Q__Deep_Copy(
            this Dictionary<Guid, List<Guid>> original_dictionary
            )
        {
            Dictionary<Guid, List<Guid>> copied_dictionary =
                new Dictionary<Guid, List<Guid>>();

            foreach (var kvp in original_dictionary)
            {
                copied_dictionary.Add(
                    kvp.Key,
                    kvp.Value.Q__DeepCopy()
                    );
            }

            return copied_dictionary;
        }
    }
}
