using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace Common_Tools
{
    public delegate void Del();

    /// <summary>
    /// A class that uses reflection to automatically convert all the static methods of a class
    /// into a console programme that displays a number of options to the user. It does so using 
    /// delegates, hence the name.
    /// </summary>
    public class Delegator
    {
        public void Run(
            Type static_class_type,
            string methods_common_string
            )
        {
            var methods = static_class_type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .ToList()
                .FindAll(x => x.Name.Contains(methods_common_string));

            Dictionary<int, Del> menuMethods = new Dictionary<int, Del>();
            Dictionary<int, string> menuMethodNames = new Dictionary<int, string>();
            for (int i = 0; i < methods.Count; i++)
            {
                var method = methods[i];
                menuMethods.Add(i, (Del)Del.CreateDelegate(typeof(Del), methods[i]));
                menuMethodNames.Add(i, methods[i].Name);
            }

            while (true)
            {
                int selection = -1;
                bool parseSuccessful = false;
                while (parseSuccessful == false || menuMethods.ContainsKey(selection) == false)
                {
                    Console.Clear();

                    Console.WriteLine("Select:");

                    foreach (var key in menuMethodNames.Keys)
                    {
                        Console.WriteLine(key.ToString() + ": " + menuMethodNames[key] + "\n");
                    }

                    string userInput = Console.ReadLine();
                    parseSuccessful = int.TryParse(userInput, out selection);
                }

                menuMethods[selection].Invoke();

                Console.ReadKey();
            }
        }

        public void Run(
            List<Type> static_class_types,
            string methods_common_string
            )
        {
            List<List<MethodInfo>> methods_list = new List<List<MethodInfo>>();

            foreach (var static_class_type in static_class_types) {
                var methods = static_class_type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .ToList()
                    .FindAll(x => x.Name.Contains(methods_common_string));
                methods_list.Add(methods);
            }

            Dictionary<int, Del> menuMethods = new Dictionary<int, Del>();
            Dictionary<int, string> menuMethodNames = new Dictionary<int, string>();

            int cnt = 0;
            foreach (var mmm in methods_list) {
                foreach (var m in mmm) {
                    menuMethods.Add(cnt, (Del)Del.CreateDelegate(typeof(Del), m));
                    menuMethodNames.Add(cnt, m.Name);
                    cnt++;
                }
            }

            while (true)
            {
                int selection = -1;
                bool parseSuccessful = false;
                while (parseSuccessful == false || menuMethods.ContainsKey(selection) == false)
                {
                    Console.Clear();

                    Console.WriteLine("Select:");

                    foreach (var key in menuMethodNames.Keys)
                    {
                        Console.WriteLine(key.ToString() + ": " + menuMethodNames[key] + "\n");
                    }

                    string userInput = Console.ReadLine();
                    parseSuccessful = int.TryParse(userInput, out selection);
                }

                menuMethods[selection].Invoke();

                Console.ReadKey();
            }
        }
    }
}
