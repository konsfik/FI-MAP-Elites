using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using SkiaSharp;

namespace FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph
{
    public class VM__Circular_Layout_Simple : Visualization_Method<DS__Undirected_Graph>
    {
        public readonly int image_width;
        public readonly int image_height;

        public VM__Circular_Layout_Simple()
        {
            image_width = 128;
            image_height = 128;
        }

        public VM__Circular_Layout_Simple(
            int image_size
            )
        {
            image_width = image_size;
            image_height = image_size;
        }

        private VM__Circular_Layout_Simple(VM__Circular_Layout_Simple vm_to_copy)
        {
            this.image_width = vm_to_copy.image_width;
            this.image_height = vm_to_copy.image_height;
        }

        public override object Q__Deep_Copy()
        {
            return new VM__Circular_Layout_Simple(this);
        }

        public override void Draw_On_Bitmap(SKBitmap bitmap, DS__Undirected_Graph individual)
        {
            List<List<int>> islands = individual.Q__Vertex_Islands();

            List<int> ordered_vertices_by_island = new List<int>();
            foreach (var island in islands)
            {
                foreach (var v in island)
                {
                    ordered_vertices_by_island.Add(v);
                }
            }

            double node_radius = (double)image_width / 24.0;

            double graph_circle_radius = (double)image_width / 2.0 - node_radius * 3.0;

            Dictionary<int, SKPoint> location__per__vertex = new Dictionary<int, SKPoint>();
            int num_vertices = ordered_vertices_by_island.Count;
            for (int i = 0; i < num_vertices; i++)
            {
                int vertex = ordered_vertices_by_island[i];
                double percentage = (double)i / (double)num_vertices;
                double angle = percentage * 2.0 * Math.PI;

                double x_val = (double)image_width / 2.0 + Math.Sin(angle) * graph_circle_radius;
                double y_val = (double)image_height / 2.0 + Math.Cos(angle) * graph_circle_radius;

                SKPoint p = new SKPoint((float)x_val, (float)y_val);
                location__per__vertex.Add(vertex, p);
            }

            using (SKCanvas canvas = new SKCanvas(bitmap))
            using (SKPaint vertex_paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 0 })
            using (SKPaint edge_paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 1 })
            {
                canvas.Clear(SKColors.White);

                foreach (var v in ordered_vertices_by_island)
                {
                    // draw the cirle
                    SKPoint circle_center = location__per__vertex[v];
                    canvas.DrawCircle(circle_center, (float)node_radius, vertex_paint);

                    // draw connections
                    List<int> neighbors = individual.Q__Neighbors(v);

                    foreach (var n in neighbors)
                    {
                        SKPoint neighbor_location = location__per__vertex[n];
                        canvas.DrawLine(circle_center, neighbor_location, edge_paint);
                    }
                }
            }
        }

        public override SKBitmap Generate_Bitmap(DS__Undirected_Graph individual)
        {
            SKBitmap bitmap = new SKBitmap(image_width, image_height);

            Draw_On_Bitmap(bitmap, individual);

            return bitmap;
        }

        public override int Q__Image_Height(DS__Undirected_Graph data_structure)
        {
            return image_width;
        }

        public override int Q__Image_Width(DS__Undirected_Graph data_structure)
        {
            return image_height;
        }
    }
}
