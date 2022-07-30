using Microsoft.VisualStudio.TestTools.UnitTesting;

using FI_MAP_Elites__Experiments;
using Common_Tools;

namespace UTEST__PCG_Workshop__Experiments
{
    [TestClass]
    public class Basic_Tests
    {
        public const double comparison_epsilon = 0.0000_0001;

        [TestMethod]
        public void Test__Q__LC__Circle_Graph()
        {
            int num_elements = 9;

            int[] num_nodes = new int[] 
            {
                4, 5, 6, 7, 8, 9, 10, 11, 12
            };

            int[] num_edges = new int[] 
            {
                4, 5, 6, 7, 8, 9, 10, 11, 12
            };

            double[] areas = new double[] 
            {
                28, 35, 42, 49, 56, 63, 70, 77, 84
            };

            for (int i = 0; i < num_elements; i++)
            {

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Cycle_Graph(
                    num_space_units: num_nodes[i]
                    );
                Assert.IsTrue(layout_constraints.graph.Q__Num_Vertices() == num_nodes[i]);
                Assert.IsTrue(layout_constraints.graph.Q__Num_Edges() == num_edges[i]);
                Assert.IsTrue(layout_constraints.Q__Area_Sum().Q__Approximately_Equal(areas[i], comparison_epsilon));
            }
        }

        [TestMethod]
        public void Test__Q__LC__Double_Circle_Graph()
        {
            int num_elements = 3;

            int[] num_half_nodes = new int[]
            {
                4, 5, 6
            };

            int[] num_nodes = new int[]
            {
                8, 10, 12
            };

            int[] num_edges = new int[]
            {
                9, 11, 13
            };

            double[] areas = new double[]
            {
                58, 72, 86
            };

            for (int i = 0; i < num_elements; i++)
            {

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Cycle_Graph(
                    half_num_space_units: num_half_nodes[i]
                    );
                Assert.IsTrue(layout_constraints.graph.Q__Is_Connected());
                Assert.IsTrue(layout_constraints.graph.Q__Num_Vertices() == num_nodes[i]);
                Assert.IsTrue(layout_constraints.graph.Q__Num_Edges() == num_edges[i]);
                Assert.IsTrue(layout_constraints.Q__Area_Sum().Q__Approximately_Equal(areas[i], comparison_epsilon));
            }
        }

        [TestMethod]
        public void Test__Q__LC__Star_Graph()
        {
            int num_elements = 9;

            int[] num_nodes = new int[]
            {
                4, 5, 6, 7, 8, 9, 10, 11, 12
            };

            int[] num_edges = new int[]
            {
                3, 4, 5, 6, 7, 8, 9, 10, 11
            };

            double[] areas = new double[]
            {
                26, 33, 40, 47, 54, 61, 68, 75, 82
            };

            for (int i = 0; i < num_elements; i++)
            {

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Star_Graph(
                    num_space_units: num_nodes[i]
                    );
                Assert.IsTrue(layout_constraints.graph.Q__Num_Vertices() == num_nodes[i]);
                Assert.IsTrue(layout_constraints.graph.Q__Num_Edges() == num_edges[i]);
                Assert.IsTrue(layout_constraints.Q__Area_Sum().Q__Approximately_Equal(areas[i], comparison_epsilon));
            }
        }

        [TestMethod]
        public void Test__Q__LC__Double_Star_Graph()
        {
            int num_elements = 3;

            int[] num_half_nodes = new int[]
            {
                4, 5, 6
            };

            int[] num_nodes = new int[]
            {
                8, 10, 12
            };

            int[] num_edges = new int[]
            {
                7, 9, 11
            };

            double[] areas = new double[]
            {
                54, 68, 82
            };

            for (int i = 0; i < num_elements; i++)
            {

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Star_Graph(
                    half_num_space_units: num_half_nodes[i]
                    );
                Assert.IsTrue(layout_constraints.graph.Q__Is_Connected());
                Assert.IsTrue(layout_constraints.graph.Q__Num_Vertices() == num_nodes[i]);
                Assert.IsTrue(layout_constraints.graph.Q__Num_Edges() == num_edges[i]);
                Assert.IsTrue(layout_constraints.Q__Area_Sum().Q__Approximately_Equal(areas[i], comparison_epsilon));
            }
        }

        [TestMethod]
        public void Test__Q__LC__Bike_Wheel_Graph()
        {
            int num_elements = 9;

            int[] num_nodes = new int[]
            {
                4, 5, 6, 7, 8, 9, 10, 11, 12
            };

            int[] num_edges = new int[]
            {
                6, 8, 10, 12, 14, 16, 18, 20, 22
            };

            double[] areas = new double[]
            {
                32, 41, 50, 59, 68, 77, 86, 95, 104
            };

            for (int i = 0; i < num_elements; i++)
            {

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Wheel_Graph(
                    num_space_units: num_nodes[i]
                    );
                Assert.IsTrue(layout_constraints.graph.Q__Num_Vertices() == num_nodes[i]);
                Assert.IsTrue(layout_constraints.graph.Q__Num_Edges() == num_edges[i]);
                Assert.IsTrue(layout_constraints.Q__Area_Sum().Q__Approximately_Equal(areas[i], comparison_epsilon));
            }
        }

        [TestMethod]
        public void Test__Q__LC__Double_Bike_Wheel_Graph()
        {
            int num_elements = 3;

            int[] nums__half_nodes = new int[]
            {
                4, 5, 6
            };

            int[] nums__nodes = new int[]
            {
                8, 10, 12
            };

            int[] nums__edges = new int[]
            {
                13, 17, 21
            };

            double[] areas = new double[]
            {
                66, 84, 102
            };

            for (int i = 0; i < num_elements; i++)
            {

                var layout_constraints = Templates__PCG_Workshop.Q__LC__Double_Wheel_Graph(
                    half_num_space_units: nums__half_nodes[i]
                    );
                int num_nodes = nums__nodes[i];
                int num_edges = nums__edges[i];
                double area = areas[i];
                Assert.IsTrue(layout_constraints.graph.Q__Is_Connected());
                Assert.IsTrue(layout_constraints.graph.Q__Num_Vertices() == num_nodes);
                Assert.IsTrue(layout_constraints.graph.Q__Num_Edges() == num_edges);
                Assert.IsTrue(layout_constraints.Q__Area_Sum().Q__Approximately_Equal(area, comparison_epsilon));
            }
        }

        [TestMethod]
        public void Test__Q__LC__Line_Graph()
        {
            int num_elements = 9;

            int[] nums_nodes = new int[]
            {
                4, 5, 6, 7, 8, 9, 10, 11, 12
            };

            int[] nums_edges = new int[]
            {
                3, 4, 5, 6, 7, 8, 9, 10, 11
            };

            double[] areas = new double[]
            {
                26, 33, 40, 47, 54, 61, 68, 75, 82
            };

            for (int i = 0; i < num_elements; i++)
            {
                var layout_constraints = Templates__PCG_Workshop.Q__LC__Path_Graph(
                    num_space_units: nums_nodes[i]
                    );
                int num_nodes = nums_nodes[i];
                int num_edges = nums_edges[i];
                double area = areas[i];
                Assert.IsTrue(layout_constraints.graph.Q__Num_Vertices() == num_nodes);
                Assert.IsTrue(layout_constraints.graph.Q__Num_Edges() == num_edges);
                Assert.IsTrue(layout_constraints.Q__Area_Sum().Q__Approximately_Equal(area, comparison_epsilon));
            }
        }
    }
}
