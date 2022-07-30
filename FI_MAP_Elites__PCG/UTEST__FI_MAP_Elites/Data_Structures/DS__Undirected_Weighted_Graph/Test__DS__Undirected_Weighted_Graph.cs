using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;

namespace UTEST__FI_MAP_Elites.Data_Structures
{
    [TestClass]
    public class Test__DS__Undirected_Weighted_Graph
    {
        [TestMethod]
        public void Test__Shortest_Path__Mockup_1()
        {
            DS__Undirected_Weighted_Graph graph = new DS__Undirected_Weighted_Graph();
            graph.M__Add_Edge(0, 1, 2.0);
            graph.M__Add_Edge(1, 2, 2.0);
            graph.M__Add_Edge(2, 3, 1.0);
            graph.M__Add_Edge(0, 3, 3.0);

            graph.Q__Shortest_Path(
                root: 0, 
                destination: 3,
                success: out bool path_success,
                shortest_path: out List<int> shortest_path
                );
            Assert.IsTrue(path_success == true);
            Assert.IsTrue(shortest_path.Count == 2);
            Assert.IsTrue(shortest_path[0] == 0);
            Assert.IsTrue(shortest_path[1] == 3);

            graph.Q__Shortest_Path__Distance(
                root: 0, 
                destination: 3,
                success: out bool distance_success,
                distance: out double shortest_path_distance
                );
            Assert.IsTrue(distance_success);
            Assert.IsTrue(shortest_path_distance == 3.0);


            graph.Q__Shortest_Path(
                root: 0, 
                destination: 1,
                success: out path_success,
                shortest_path: out shortest_path
                );
            Assert.IsTrue(path_success == true);
            Assert.IsTrue(shortest_path.Count == 2);
            Assert.IsTrue(shortest_path[0] == 0);
            Assert.IsTrue(shortest_path[1] == 1);

            graph.Q__Shortest_Path__Distance(
                root: 0,
                destination: 1,
                success: out distance_success,
                distance: out shortest_path_distance
                );
            Assert.IsTrue(distance_success);
            Assert.IsTrue(shortest_path_distance == 2.0);


            graph.Q__Shortest_Path(
                root: 0, 
                destination: 2,
                success: out path_success,
                shortest_path: out shortest_path
                );
            Assert.IsTrue(path_success == true);
            Assert.IsTrue(shortest_path.Count == 3);
            Assert.IsTrue(shortest_path[0] == 0);
            Assert.IsTrue(shortest_path[1] == 1);
            Assert.IsTrue(shortest_path[2] == 2);

            graph.Q__Shortest_Path__Distance(
                root: 0,
                destination: 2,
                success: out distance_success,
                distance: out shortest_path_distance
                );
            Assert.IsTrue(distance_success == true);
            Assert.IsTrue(shortest_path_distance == 4.0);
        }

        [TestMethod]
        public void Test__Shortest_Path__Mockup_2()
        {
            DS__Undirected_Weighted_Graph graph = new DS__Undirected_Weighted_Graph();
            graph.M__Add_Edge(0, 1, 2.0);
            graph.M__Add_Edge(2, 3, 1.0);

            // 0 <-> 1
            graph.Q__Shortest_Path(
                root: 0, 
                destination: 1,
                success: out bool path_success,
                shortest_path: out List<int> shortest_path
                );
            Assert.IsTrue(path_success);
            Assert.IsTrue(shortest_path.Count == 2);
            Assert.IsTrue(shortest_path[0] == 0);
            Assert.IsTrue(shortest_path[1] == 1);

            graph.Q__Shortest_Path__Distance(
                root: 0,
                destination: 1,
                success: out bool distance_success,
                distance: out double shortest_path_distance
                );
            Assert.IsTrue(distance_success);
            Assert.IsTrue(shortest_path_distance == 2.0);

            // 1 <-> 0
            graph.Q__Shortest_Path(
                root: 1,
                destination: 0,
                success: out path_success,
                shortest_path: out shortest_path
                );
            Assert.IsTrue(path_success);
            Assert.IsTrue(shortest_path.Count == 2);
            Assert.IsTrue(shortest_path[0] == 1);
            Assert.IsTrue(shortest_path[1] == 0);

            graph.Q__Shortest_Path__Distance(
                root: 1,
                destination: 0,
                success: out distance_success,
                distance: out shortest_path_distance
                );
            Assert.IsTrue(distance_success);
            Assert.IsTrue(shortest_path_distance == 2.0);

            // 2 <-> 3
            graph.Q__Shortest_Path(
                root: 2,
                destination: 3,
                success: out path_success,
                shortest_path: out shortest_path
                );
            Assert.IsTrue(path_success);
            Assert.IsTrue(shortest_path.Count == 2);
            Assert.IsTrue(shortest_path[0] == 2);
            Assert.IsTrue(shortest_path[1] == 3);

            graph.Q__Shortest_Path__Distance(
                root: 2,
                destination: 3,
                success: out distance_success,
                distance: out shortest_path_distance
                );
            Assert.IsTrue(distance_success);
            Assert.IsTrue(shortest_path_distance == 1.0);

            // 1 <-> 2  (discontinuity here)
            graph.Q__Shortest_Path(
                root: 1,
                destination: 2,
                success: out path_success,
                shortest_path: out shortest_path
                );
            Assert.IsFalse(path_success);
            Assert.IsTrue(shortest_path.Count == 0);

            graph.Q__Shortest_Path__Distance(
                root: 1,
                destination: 2,
                success: out distance_success,
                distance: out shortest_path_distance
                );
            Assert.IsFalse(distance_success);
            Assert.IsTrue(double.IsNaN( shortest_path_distance));
        }
    }
}
