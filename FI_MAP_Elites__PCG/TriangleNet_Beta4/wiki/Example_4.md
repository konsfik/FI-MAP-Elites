# Example 4 - Refining regions

This example will show how to refine a mesh in a specific region (this example uses code from [Example 2](Example_2.md)):

```
namespace Examples
{
    using TriangleNet;
    using TriangleNet.Geometry;
    using TriangleNet.Meshing;
    using TriangleNet.Smoothing;
    using TriangleNet.Tools;

    public class Example4
    {
        public static Mesh RefineRegions()
        {
            // Generate the input geometry.
            var poly = new Polygon();
            
            var center = new Point(0, 0);

            // Three concentric circles.
            poly.Add(Example2.Circle(1.0, center, 0.1, 1), center);
            poly.Add(Example2.Circle(2.0, center, 0.1, 2));
            poly.Add(Example2.Circle(3.0, center, 0.3, 3));

            // Define regions.
            poly.Regions.Add(new RegionPointer(1.5, 0.0, 1));
            poly.Regions.Add(new RegionPointer(2.5, 0.0, 2));

            // Set quality and constraint options.
            var options = new ConstraintOptions() { ConformingDelaunay = true };
            var quality = new QualityOptions() { MinimumAngle = 25.0 };

            // Generate mesh.
            var mesh = (Mesh)poly.Triangulate(options, quality);

            var smoother = new SimpleSmoother();

            // Smooth mesh and re-apply quality options.
            smoother.Smooth(mesh);
            mesh.Refine(quality);

            // Calculate mesh quality
            var statistic = new QualityMeasure();

            statistic.Update(mesh);

            // Use the minimum triangle area for region refinement
            double area = 1.75 * statistic.AreaMinimum;

            foreach (var t in mesh.Triangles)
            {
                // Set area constraint for all triangles in region 1
                if (t.Label == 1) t.Area = area;
            }

            // Use per triangle area constraint for next refinement
            quality.VariableArea = true;

            // Refine mesh to meet area constraint.
            mesh.Refine(quality);

            // Smooth once again.
            smoother.Smooth(mesh);

            return mesh;
        }
    }
}
```
