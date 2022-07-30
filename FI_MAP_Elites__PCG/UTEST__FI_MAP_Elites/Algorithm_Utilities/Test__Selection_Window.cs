using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace UTEST__FI_MAP_Elites.Algorithms.Shared_Elements
{
    [TestClass]
    public class Test__Selection_Window
    {
        [TestMethod]
        public void Test__Basic()
        {
            Selection_Window w = new Selection_Window(
                num_dimensions: 2,
                center_coordinates: new int[] { 3,3 },
                half_size: 2
                );

            var center_coords = w.Q__Center_Coords();
            Assert.IsTrue(center_coords[0] == 3);
            Assert.IsTrue(center_coords[1] == 3);

            Assert.IsTrue(w.min_coords[0] == 1);
            Assert.IsTrue(w.Q__Min_Coords()[0] == 1);
            Assert.IsTrue(w.Q__Min_X() == 1);

            Assert.IsTrue(w.min_coords[1] == 1);
            Assert.IsTrue(w.Q__Min_Coords()[1] == 1);
            Assert.IsTrue(w.Q__Min_Y() == 1);

            Assert.IsTrue(w.max_coords[0] == 5);
            Assert.IsTrue(w.Q__Max_Coords()[0] == 5);
            Assert.IsTrue(w.Q__Max_X() == 5);

            Assert.IsTrue(w.max_coords[1] == 5);
            Assert.IsTrue(w.Q__Max_Coords()[1] == 5);
            Assert.IsTrue(w.Q__Max_Y() == 5);

            Assert.IsTrue(w.Q__Width() == 5);
            Assert.IsTrue(w.Q__Height() == 5);
        }

        [TestMethod]
        public void Test__Triangle_Coords()
        {
            Selection_Window window = new Selection_Window(
                num_dimensions: 2,
                center_coordinates: new int[] { 3, 3 },
                half_size: 2
                );

            var coords = window.Q__Left_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c=> c[0] == 1 && c[1] == 1));
            Assert.IsTrue(coords.Any(c=> c[0] == 1 && c[1] == 2));
            Assert.IsTrue(coords.Any(c=> c[0] == 1 && c[1] == 3));
            Assert.IsTrue(coords.Any(c=> c[0] == 1 && c[1] == 4));
            Assert.IsTrue(coords.Any(c=> c[0] == 1 && c[1] == 5));

            Assert.IsTrue(coords.Any(c=> c[0] == 2 && c[1] == 2));
            Assert.IsTrue(coords.Any(c=> c[0] == 2 && c[1] == 3));
            Assert.IsTrue(coords.Any(c=> c[0] == 2 && c[1] == 4));

            Assert.IsTrue(coords.Any(c=> c[0] == 3 && c[1] == 3));

            coords = window.Q__Right_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 1));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 4));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 5));

            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 4));

            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 3));

            coords = window.Q__Down_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c => c[0] == 1 && c[1] == 1));
            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 1));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 1));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 1));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 1));

            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 2));

            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 3));

            coords = window.Q__Up_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c => c[0] == 1 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 5));

            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 4));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 4));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 4));

            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 3));




            window = new Selection_Window(
                num_dimensions: 2,
                center_coordinates: new int[] { 4, 4 },
                half_size: 2
                );


            coords = window.Q__Left_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 4));
            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 6));

            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 4));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 5));

            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 4));


            coords = window.Q__Right_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c => c[0] == 6 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 6 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 6 && c[1] == 4));
            Assert.IsTrue(coords.Any(c => c[0] == 6 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 6 && c[1] == 6));

            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 4));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 5));

            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 4));

            coords = window.Q__Down_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 2));
            Assert.IsTrue(coords.Any(c => c[0] == 6 && c[1] == 2));

            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 3));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 3));

            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 4));

            coords = window.Q__Up_Triangle_Coords();

            Assert.IsTrue(coords.Count == 9); // 5 + 3 + 1 = 9

            Assert.IsTrue(coords.Any(c => c[0] == 2 && c[1] == 6));
            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 6));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 6));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 6));
            Assert.IsTrue(coords.Any(c => c[0] == 6 && c[1] == 6));

            Assert.IsTrue(coords.Any(c => c[0] == 3 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 5));
            Assert.IsTrue(coords.Any(c => c[0] == 5 && c[1] == 5));

            Assert.IsTrue(coords.Any(c => c[0] == 4 && c[1] == 4));

        }

    }
}
