using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Data_Structures;

using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Voronoi;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public partial class DS__Voronoi : Data_Structure
    {
        public virtual void M__Move_Point(
            int point_index,
            Vec2d new_position,
            bool recalculate_phenotype
            )
        {
            generator_points[point_index] = new_position;

            if (recalculate_phenotype)
            {
                M__Recalculate__Phenotype();
            }
        }

        public virtual void M__Recalculate__Phenotype()
        {
            M__Fix_Generator_Points();

            // calculate the main voronoi structure
            Mesh mesh = DS__Voronoi__Utilities.Extract__Mesh__From__Points_List(
                generator_points,
                bounding_rectangle
                );

            StandardVoronoi tri_vronoi = new StandardVoronoi(mesh);

            is_active__per__cell =
                DS__Voronoi__Utilities.Extract__Is_Active_Per_Cell(
                    tri_voronoi: tri_vronoi,
                    bounding_rectangle: bounding_rectangle
                );

            DS__Voronoi__Utilities.Extract__Voronoi_Points(
                // input
                tri_voronoi: tri_vronoi,
                is_active__per__cell: is_active__per__cell,
                epsilon: epsilon,

                // output
                voronoi_points: out voronoi_points,
                perimeter_points__per__cell: out perimeter_points__per__cell
                );

            DS__Voronoi__Utilities.Extract__Voronoi_Lines(
                // input
                perimeter_points__per__cell: perimeter_points__per__cell,
                is_active__per__cell: is_active__per__cell,

                // output
                perimeter_lines__per__cell: out perimeter_lines__per__cell,
                voronoi_lines: out voronoi_lines
                );

            centroids = 
                DS__Voronoi__Utilities
                .Extract_Centroids(
                    perimeter_points__per__cell: perimeter_points__per__cell,
                    is_active__per__cell: is_active__per__cell,
                    generator_points: generator_points
                    );

            area__per__cell =
                DS__Voronoi__Utilities.Extract__Area__per__Cell(
                    perimeter_points__per__cell,
                    is_active__per__cell
                    );

            connectivity_graph =
                DS__Voronoi__Utilities.Extract__Connectivity_Graph(
                    mesh: mesh,
                    perimeter_lines__per__cell: perimeter_lines__per__cell,
                    is_active__per__cell: is_active__per__cell,
                    epsilon: epsilon,
                    connectivity_threshold: connectivity_threshold
                );

            weighted_connectivity_graph =
                DS__Voronoi__Utilities.Extract__Weighted_Connectivity_Graph(
                    connectivity_graph: connectivity_graph,
                    points: centroids
                    );


        }

        public void M__Fix_Generator_Points()
        {
            int num_points = generator_points.Count;

            // ensure there are no duplicate points
            for (int i = 0; i < num_points - 1; i++)
            {
                for (int j = i + 1; j < num_points; j++)
                {
                    if (generator_points[i] == generator_points[j])
                    {
                        generator_points[i] = new Vec2d(
                            generator_points[i].x + 0.000001,
                            generator_points[i].y
                            );
                        generator_points[j] = new Vec2d(
                            generator_points[j].x - 0.000001,
                            generator_points[j].y
                            );
                    }
                }
            }

            // ensure all points are within bounds
            for (int i = 0; i < num_points; i++)
            {
                while (generator_points[i].x < bounding_rectangle.Min_X)
                {
                    generator_points[i] = new Vec2d(
                        generator_points[i].x + bounding_rectangle.width,
                        generator_points[i].y
                        );
                }
                while (generator_points[i].x > bounding_rectangle.Max_X)
                {
                    generator_points[i] = new Vec2d(
                        generator_points[i].x - bounding_rectangle.width,
                        generator_points[i].y
                        );
                }
                while (generator_points[i].y < bounding_rectangle.Min_Y)
                {
                    generator_points[i] = new Vec2d(
                        generator_points[i].x,
                        generator_points[i].y + bounding_rectangle.height
                        );
                }
                while (generator_points[i].y > bounding_rectangle.Max_Y)
                {
                    generator_points[i] = new Vec2d(
                        generator_points[i].x,
                        generator_points[i].y - bounding_rectangle.height
                        );
                }
            }
        }
    }
}
