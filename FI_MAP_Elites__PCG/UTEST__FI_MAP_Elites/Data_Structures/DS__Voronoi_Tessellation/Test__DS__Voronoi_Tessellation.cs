using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;
using FI_MAP_Elites__PCG.Data_Structures.Voronoi;
using FI_MAP_Elites__PCG.Data_Structures;

using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Voronoi;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTEST__FI_MAP_Elites.Data_Structures
{
    [TestClass]
    public class Test__DS__Voronoi_Tessellation
    {
        [TestMethod]
        public void Test__Legal_Cells()
        {
            List<double> scaling_factors = new List<double>() {
                0.9999,
                1.0,
                1.0001
            };
            double epsilon = double.Epsilon;

            foreach (var scaling_factor in scaling_factors)
            {
                List<Vec2d> points = new List<Vec2d>() {
                    new Vec2d(10*scaling_factor,10*scaling_factor),
                    new Vec2d(10*scaling_factor,90*scaling_factor),
                    new Vec2d(90*scaling_factor,90*scaling_factor),
                    new Vec2d(90*scaling_factor,10*scaling_factor),
                    new Vec2d(50*scaling_factor,50*scaling_factor),
                };

                Rect2d rectangle = new Rect2d(
                    0 * scaling_factor,
                    0 * scaling_factor,
                    100 * scaling_factor,
                    100 * scaling_factor);

                DS__Voronoi voronoi =
                    new DS__Voronoi(
                        points,
                        rectangle,
                        0.1 * scaling_factor,
                        epsilon
                        );

                Assert.IsFalse(voronoi.is_active__per__cell[0]);
                Assert.IsFalse(voronoi.is_active__per__cell[1]);
                Assert.IsFalse(voronoi.is_active__per__cell[2]);
                Assert.IsFalse(voronoi.is_active__per__cell[3]);
                Assert.IsTrue(voronoi.is_active__per__cell[4]);

                Assert.IsTrue(voronoi.connectivity_graph.Q__Num_Vertices() == 1);
                Assert.IsTrue(voronoi.connectivity_graph.Q__Num_Edges() == 0);
            }


        }

        [TestMethod]
        public void Test__Legal_Cells__v2()
        {

            List<double> scaling_factors = new List<double>() {
                0.000001,
                0.00001,
                0.0001,
                0.001,
                0.01,
                0.1,
                0.9,
                0.99,
                0.999,
                0.9999,
                0.99999,
                0.999999,
                0.9999999,
                1.0,
                1.0000001,
                1.000001,
                1.00001,
                1.0001,
                1.001,
                1.01,
                1.1,
                1000.0,
                1000000.0
            };
            double epsilon = 0.0000_0000_01;

            foreach (var scaling_factor in scaling_factors)
            {
                List<Vec2d> points = new List<Vec2d>();

                double w = 1.0 * scaling_factor;
                double h = 1.0 * scaling_factor;
                double connectivity_threshold = 0.001 * scaling_factor;

                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        double x = w / 8.0 + i * w / 4.0;
                        double y = h / 8.0 + j * h / 4.0;
                        points.Add(new Vec2d(x, y));
                    }
                }

                Rect2d rectangle = new Rect2d(
                    0.0,
                    0.0,
                    w,
                    h
                    );

                DS__Voronoi voronoi =
                    new DS__Voronoi(
                        generator_points: points,
                        bounding_rectangle: rectangle,
                        connectivity_threshold: connectivity_threshold,
                        epsilon: epsilon
                        );

                Assert.IsFalse(voronoi.is_active__per__cell[0]);
                Assert.IsFalse(voronoi.is_active__per__cell[1]);
                Assert.IsFalse(voronoi.is_active__per__cell[2]);
                Assert.IsFalse(voronoi.is_active__per__cell[3]);

                Assert.IsFalse(voronoi.is_active__per__cell[4]);
                Assert.IsTrue(voronoi.is_active__per__cell[5]);
                Assert.IsTrue(voronoi.is_active__per__cell[6]);
                Assert.IsFalse(voronoi.is_active__per__cell[7]);

                Assert.IsFalse(voronoi.is_active__per__cell[8]);
                Assert.IsTrue(voronoi.is_active__per__cell[9]);
                Assert.IsTrue(voronoi.is_active__per__cell[10]);
                Assert.IsFalse(voronoi.is_active__per__cell[11]);

                Assert.IsFalse(voronoi.is_active__per__cell[12]);
                Assert.IsFalse(voronoi.is_active__per__cell[13]);
                Assert.IsFalse(voronoi.is_active__per__cell[14]);
                Assert.IsFalse(voronoi.is_active__per__cell[15]);

                Assert.IsTrue(voronoi.connectivity_graph.Q__Num_Vertices() == 4);
                Assert.IsTrue(voronoi.connectivity_graph.Q__Num_Edges() == 4);


                Assert.IsTrue(voronoi.voronoi_points.Count == 9);
                Assert.IsTrue(voronoi.voronoi_lines.Count == 12);
                int num_cells = voronoi.Q__Num_Generator_Points();
                for (int c = 0; c < num_cells; c++)
                {
                    bool is_active = voronoi.Q__Cell__Is_Active(c);
                    if (is_active)
                    {
                        var perimeter_points = voronoi.perimeter_points__per__cell[c];
                        Assert.IsTrue(perimeter_points.Count == 4);
                        foreach (var p in perimeter_points)
                        {
                            Assert.IsTrue(voronoi.voronoi_points.Contains(p));
                        }

                        var perimeter_lines = voronoi.perimeter_lines__per__cell[c];
                        Assert.IsTrue(perimeter_lines.Count == 4);
                        foreach (var l in perimeter_lines)
                        {
                            Assert.IsTrue(voronoi.voronoi_lines.Contains(l));
                        }
                    }
                }
            }

        }

    }
}
