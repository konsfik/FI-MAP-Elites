# Example 7 - Boolean operations on meshes

This example will show how to use regions to perform boolean operations on meshes. The code uses the [RegionIterator](/source/Triangle.NET/Triangle/Meshing/Iterators/RegionIterator.cs) class:

```
namespace Examples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TriangleNet;
    using TriangleNet.Geometry;
    using TriangleNet.Meshing.Iterators;
    using TriangleNet.Tools;
    using TriangleNet.Topology;

    public static class Example7
    {
        public static void FindRegions()
        {
            // Generate the input geometry.
            var polygon = new Polygon(8, true);

            // Two intersecting rectangles.
            var A = CreateRectangle(0.0, 4.0, 4.0, 0.0, 1);
            var B = CreateRectangle(1.0, 5.0, 3.0, 1.0, 2);

            polygon.Add(A);
            polygon.Add(B);

            // Generate mesh.
            var mesh = (Mesh)polygon.Triangulate();

            // Find a seeding triangle (in this case, the point (2, 2) lies in
            // both rectangles).
            var seed = (new TriangleQuadTree(mesh)).Query(2.0, 2.0) as Triangle;

            var iterator = new RegionIterator(mesh);

            iterator.Process(seed, t => t.Label ^= 1, 1);
            iterator.Process(seed, t => t.Label ^= 2, 2);

            // At this point, all triangles will have label 1, 2 or 3 (= 1 xor 2).

            // The intersection of A and B.
            var intersection = mesh.Triangles.Where(t => t.Label == 3);

            // The difference A \ B.
            var difference = mesh.Triangles.Where(t => t.Label == 1);

            // The xor of A and B.
            var xor = mesh.Triangles.Where(t => t.Label == 1 || t.Label == 2);
        }

        private static Contour CreateRectangle(double left, double top,
            double right, double bottom, int mark)
        {
            var points = new List<Vertex>(4);

            points.Add(new Vertex(left, top, mark));
            points.Add(new Vertex(right, top, mark));
            points.Add(new Vertex(right, bottom, mark));
            points.Add(new Vertex(left, bottom, mark));

            return new Contour(points, mark, true);
        }
    }
}
```
