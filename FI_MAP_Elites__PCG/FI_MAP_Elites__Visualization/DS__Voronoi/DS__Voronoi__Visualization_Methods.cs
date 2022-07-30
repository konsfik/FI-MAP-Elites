using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Data_Structures.Voronoi;

using SkiaSharp;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public static class DS__Voronoi__Visualization_Methods
    {
        public static void Draw_Voronoi_Structure(
            this SKBitmap image,
            DS__Voronoi voronoi_structure,
            float offset_x,
            float offset_y,
            float scale,

            bool background, 
            SKColor background_color,

            bool cells_boundary,
            float cells_boundary_width,
            SKColor cells_boundary_color,

            bool cells_fill,
            SKColor cells_fill_color,

            bool points,
            float points_radius,
            SKColor points_color,

            bool points_connections,
            float points_connections_stroke_width,
            float points_connections_dash_size,
            SKColor points_connections_color,

            bool centroids,
            float centroids_radius,
            SKColor centroids_color,

            bool centroids_connections,
            float centroids_connections_stroke_width,
            float centroids_connections_dash_size,
            SKColor centroids_connections_color
            )
        {
            //SKColor background_color = new SKColor(127, 127, 127, 255);


            using (SKCanvas canvas = new SKCanvas(image))
            using (SKPaint background_brush = new SKPaint() { 
                Color = background_color
            })
            using (SKPaint cell_boundaries_pen = new SKPaint()
            {
                Color = cells_boundary_color,
                StrokeWidth = cells_boundary_width
            })
            using (SKPaint points_brush = new SKPaint() { Color = points_color })
            using (SKPaint points_connections_brush = new SKPaint()
            {
                Color = points_connections_color,
                StrokeWidth = points_connections_stroke_width,
                PathEffect = SKPathEffect.CreateDash(
                    new float[] { points_connections_dash_size, points_connections_dash_size }, 
                    0
                    )
            })
            using (SKPaint centroids_brush = new SKPaint() { Color = centroids_color })
            using (SKPaint centroids_connections_brush = new SKPaint()
            {
                Color = centroids_connections_color,
                StrokeWidth = centroids_connections_stroke_width,
                PathEffect = SKPathEffect.CreateDash(
                    new float[] { centroids_connections_dash_size, centroids_connections_dash_size }, 
                    0
                    )
            })
            using (SKPaint cells_brush = new SKPaint() { 
                Color = cells_fill_color 
            })
            {
                
                canvas.Scale(1, -1, 0, image.Height / 2.0f);

                if (background) {
                    canvas.Clear(background_color);
                }

                if (cells_fill)
                {
                    voronoi_structure.Fill_Cells(
                        canvas,
                        offset_x,
                        offset_y,
                        scale,
                        cells_brush
                        );
                }

                if (cells_boundary)
                {
                    voronoi_structure.Draw_Cell_Boundaries(
                        canvas,
                        offset_x,
                        offset_y,
                        scale,
                        cell_boundaries_pen
                        );
                }

                if (points)
                {
                    voronoi_structure.Draw_Points(
                        canvas,
                        points_brush,
                        offset_x: offset_x,
                        offset_y: offset_y,
                        scale: scale,
                        radius: points_radius
                        );
                }

                if (points_connections)
                {
                    voronoi_structure.Draw_Points_Connections(
                        canvas,
                        points_connections_brush,
                        offset_x,
                        offset_y,
                        scale
                        );
                }

                if (centroids)
                {
                    voronoi_structure.Draw_Centroids(
                        canvas,
                        centroids_connections_brush,
                        offset_x: offset_x,
                        offset_y: offset_y,
                        scale: scale,
                        radius: centroids_radius
                        );
                }

                if (centroids_connections)
                {
                    voronoi_structure.Draw_Centroids_Connections(
                        canvas,
                        centroids_connections_brush,
                        offset_x,
                        offset_y,
                        scale
                        );
                }

            }
        }

        public static void Draw_Cell_Boundaries(
            this DS__Voronoi voronoi,
            SKCanvas graphics,
            float offset_x,
            float offset_y,
            float scale,
            SKPaint cells_pen
            )
        {
            foreach (var ln in voronoi.voronoi_lines)
            {
                var p0 = ln.p0;
                var p1 = ln.p1;
                SKPoint skp0 = p0.Convert__To__System_Drawing_PointF(offset_x,offset_y,scale);
                SKPoint skp1 = p1.Convert__To__System_Drawing_PointF(offset_x, offset_y, scale);
                graphics.DrawLine(
                    skp0,
                    skp1,
                    cells_pen
                    );
            }
        }

        public static void Fill_Cells(
            this DS__Voronoi voronoi,
            SKCanvas graphics,
            float offset_x,
            float offset_y,
            float scale,
            SKPaint cells_brush
            )
        {
            

            int num_cells = voronoi.Q__Num_Generator_Points();

            for (int c = 0; c < num_cells; c++)
            {
                var boundary_points =
                    voronoi
                    .Q__Cell__Boundary_Points(c);

                if (boundary_points.Count == 0)
                {
                    continue;
                }

                List<SKPoint> points =
                    boundary_points.Convert__To__SKPoint_List(
                        offset_x,
                        offset_y,
                        scale
                        );

                using (SKPath path = new SKPath())
                {
                    path.AddPoly(points.ToArray(), true);
                    //foreach (var p in points)
                    //    path.MoveTo(p);
                    //path.Close();

                    graphics.DrawPath(path, cells_brush);
                }
            }

        }

        public static void Draw_Points(
            this DS__Voronoi voronoi,
            SKCanvas graphics,
            SKPaint points_brush,
            float offset_x = 0,
            float offset_y = 0,
            float scale = 1,
            float radius = 4
            )
        {
            foreach (var p in voronoi.generator_points)
            {
                SKPoint point = p.Convert__To__System_Drawing_PointF(
                    offset_x,
                    offset_y,
                    scale
                    );

                graphics.DrawCircle(
                    point,
                    radius,
                    points_brush
                    );

            }
        }

        public static void Draw_Centroids(
            this DS__Voronoi voronoi,
            SKCanvas graphics,
            SKPaint centroids_brush,
            float offset_x = 0,
            float offset_y = 0,
            float scale = 1,
            float radius = 4
            )
        {
            int num_cells = voronoi.Q__Num_Generator_Points();

            for (int c = 0; c < num_cells; c++) {
                if (voronoi.Q__Cell__Is_Active(c) == false) continue;
                Vec2d centroid = voronoi.centroids[c];

                SKPoint point = centroid.Convert__To__System_Drawing_PointF(
                    offset_x,
                    offset_y,
                    scale
                    );

                graphics.DrawCircle(
                    point,
                    radius,
                    centroids_brush
                    );
            }
        }

        public static void Draw_Centroids_Connections(
            this DS__Voronoi voronoi,
            SKCanvas graphics,
            SKPaint legal_edges_pen,
            float offset_x,
            float offset_y,
            float scale
            )
        {
            List<Undirected_Edge> legal_edges =
                voronoi
                .connectivity_graph
                .Q__Edges();

            foreach (var edge in legal_edges)
            {
                SKPoint p0 =
                    voronoi
                    .centroids[edge.v1]
                    .Convert__To__System_Drawing_PointF(
                        offset_x,
                        offset_y,
                        scale
                        );

                SKPoint p1 =
                    voronoi
                    .centroids[edge.v2]
                    .Convert__To__System_Drawing_PointF(
                        offset_x,
                        offset_y,
                        scale
                        );

                graphics.DrawLine(
                    p0,
                    p1,
                    legal_edges_pen
                    );
            }
        }

        public static void Draw_Points_Connections(
            this DS__Voronoi voronoi,
            SKCanvas graphics,
            SKPaint legal_edges_pen,
            float offset_x,
            float offset_y,
            float scale
            )
        {
            List<Undirected_Edge> legal_edges =
                voronoi
                .connectivity_graph
                .Q__Edges();

            foreach (var edge in legal_edges)
            {
                SKPoint p0 =
                    voronoi
                    .generator_points[edge.v1]
                    .Convert__To__System_Drawing_PointF(
                        offset_x,
                        offset_y,
                        scale
                        );

                SKPoint p1 =
                    voronoi
                    .generator_points[edge.v2]
                    .Convert__To__System_Drawing_PointF(
                        offset_x,
                        offset_y,
                        scale
                        );

                graphics.DrawLine(
                    p0,
                    p1,
                    legal_edges_pen
                    );
            }
        }

    }

}
