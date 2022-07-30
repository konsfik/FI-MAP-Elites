using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using FI_MAP_Elites__PCG.Shared_Elements;

using Common_Tools;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace UTEST__FI_MAP_Elites.Data_Structures
{
    [TestClass()]
    public class Undirected_Graph__Tests
    {
        [TestMethod]
        public void Test__Bridges()
        {
            DS__Undirected_Graph graph = new DS__Undirected_Graph();
            graph.M__Add_Edge(0, 1);
            graph.M__Add_Edge(1, 2);
            graph.M__Add_Edge(2, 3);
            graph.M__Add_Edge(3, 4);
            var bridges = graph.Q__Bridges();

            Assert.IsTrue(bridges.Count == 4);
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(0, 1)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(1, 2)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(2, 3)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(3, 4)));

            graph.M__Add_Edge(0, 2);
            bridges = graph.Q__Bridges();

            Assert.IsTrue(bridges.Count == 2);
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(2, 3)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(3, 4)));

            graph.M__Add_Edge(2, 4);
            bridges = graph.Q__Bridges();

            Assert.IsTrue(bridges.Count == 0);

            graph = new DS__Undirected_Graph();
            graph.M__Add_Edge(0, 1);
            graph.M__Add_Edge(1, 2);
            graph.M__Add_Edge(2, 3);
            graph.M__Add_Edge(4, 5);
            graph.M__Add_Edge(5, 6);
            graph.M__Add_Edge(6, 7);
            bridges = graph.Q__Bridges();

            Assert.IsTrue(bridges.Count == 6);
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(0, 1)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(1, 2)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(2, 3)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(4, 5)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(5, 6)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(6, 7)));

            graph.M__Add_Edge(0, 2);
            bridges = graph.Q__Bridges();
            Assert.IsTrue(bridges.Count == 4);

            Assert.IsTrue(bridges.Contains(new Undirected_Edge(2, 3)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(4, 5)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(5, 6)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(6, 7)));

            graph.M__Add_Edge(5, 7);
            bridges = graph.Q__Bridges();
            Assert.IsTrue(bridges.Count == 2);

            Assert.IsTrue(bridges.Contains(new Undirected_Edge(2, 3)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(4, 5)));

            graph.M__Add_Edge(3, 4);
            bridges = graph.Q__Bridges();
            Assert.IsTrue(bridges.Count == 3);

            Assert.IsTrue(bridges.Contains(new Undirected_Edge(2, 3)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(3, 4)));
            Assert.IsTrue(bridges.Contains(new Undirected_Edge(4, 5)));
        }


        [TestMethod]
        public void Test__UGraph_i__Usage()
        {
            DS__Undirected_Graph graph = new DS__Undirected_Graph();
            graph.M__Add_Edge(0, 1);
            graph.M__Add_Edge(1, 2);
            graph.M__Add_Edge(2, 3);
            graph.M__Add_Edge(3, 4);

            Assert.IsTrue(graph.Q__Is_Cyclic() == false);
            Assert.IsTrue(graph.Q__Num_Vertices() == 5);
            Assert.IsTrue(graph.Q__Degree_Sum() == 8);
            Assert.IsTrue(graph.Q__Num_Edges() == 4);



            graph.M__Add_Edge(4, 0);

            Assert.IsTrue(graph.Q__Is_Cyclic() == true);
            Assert.IsTrue(graph.Q__Num_Vertices() == 5);
            Assert.IsTrue(graph.Q__Degree_Sum() == 10);
            Assert.IsTrue(graph.Q__Num_Edges() == 5);

        }


    }
}
