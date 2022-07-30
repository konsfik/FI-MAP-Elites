using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;
using SkiaSharp;
using FI_MAP_Elites__PCG.Data_Structures.Voronoi;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class VM__Architectural_Plan__Detailed : Visualization_Method<DS__Architectural_Plan>
    {
        public SKColor room_perimeter__color;
        public readonly float room_perimeter__pen_width;
        public readonly float scale;

        public readonly bool plan_graph;
        public readonly float plan_graph__stroke_width;
        public readonly float plan_graph__dash_size;
        public readonly float plan_graph__points_radius;
        public readonly SKColor plan_graph__color;

        VM__Voronoi_Simple voronoi_visualization_method;

        public VM__Architectural_Plan__Detailed(
            SKColor room_perimeter__color,
            float room_perimeter__pen_width,
            float scale,
            bool plan_graph,
            float plan_graph__stroke_width,
            float plan_graph__dash_size,
            float plan_graph__points_radius,
            SKColor plan_graph__color
            )
        {
            this.room_perimeter__color = room_perimeter__color;
            this.room_perimeter__pen_width = room_perimeter__pen_width;
            this.scale = scale;

            this.plan_graph = plan_graph;
            this.plan_graph__stroke_width = plan_graph__stroke_width;
            this.plan_graph__dash_size = plan_graph__dash_size;
            this.plan_graph__points_radius = plan_graph__points_radius;
            this.plan_graph__color = plan_graph__color;

            voronoi_visualization_method = new VM__Voronoi_Simple(
                scale: scale,

                background: false,
                background_color: SKColors.Black,

                cells_boundary: true,
                cells_boundary_width: 1.0f,
                cells_boundary_color: new SKColor(0, 0, 0, 64),

                cells_fill: false,
                cells_fill_color: SKColors.Black,

                points: false,
                points_radius: 2.0f,
                points_color: SKColors.Red,

                points_connections: false,
                points_connections_stroke_width: 1.0f,
                points_connections_dash_size: 4.0f,
                points_connections_color: new SKColor(0, 0, 0, 32),

                centroids: true,
                centroids_radius: 5.0f,
                centroids_color: SKColors.Black,

                centroids_connections: true,
                centroids_connections_stroke_width: 1.0f,
                centroids_connections_dash_size: 4.0f,
                centroids_connections_color: new SKColor(0, 0, 0, 32)
                ) ;
        }

        public VM__Architectural_Plan__Detailed(VM__Architectural_Plan__Detailed vm_to_copy)
        {
            this.room_perimeter__color = vm_to_copy.room_perimeter__color;
            this.room_perimeter__pen_width = vm_to_copy.room_perimeter__pen_width;
            this.scale = vm_to_copy.scale;

            this.plan_graph = vm_to_copy.plan_graph;
            this.plan_graph__stroke_width = vm_to_copy.plan_graph__stroke_width;
            this.plan_graph__dash_size = vm_to_copy.plan_graph__dash_size;
            this.plan_graph__points_radius = vm_to_copy.plan_graph__points_radius;
            this.plan_graph__color = vm_to_copy.plan_graph__color;

            this.voronoi_visualization_method = 
                (VM__Voronoi_Simple)vm_to_copy.voronoi_visualization_method.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new VM__Architectural_Plan__Detailed(this);
        }

        public override void Draw_On_Bitmap(SKBitmap bitmap, DS__Architectural_Plan individual)
        {
            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                canvas.Scale(1, -1, 0, bitmap.Height / 2.0f);

                Draw__Rooms(canvas, individual);
                Draw__Interior_Doors(canvas, individual);
                Draw__Exterior_Doors(canvas, individual);
                Draw__Windows(canvas, individual);
                if (plan_graph)
                {
                    Draw__Plan_Graph(canvas, individual);
                }
            }

            voronoi_visualization_method.Draw_On_Bitmap(bitmap, individual.voronoi_tessellation);
        }

        public override SKBitmap Generate_Bitmap(DS__Architectural_Plan individual)
        {

            int image_width = Q__Image_Width(individual);
            int image_height = Q__Image_Height(individual);

            SKBitmap bitmap = new SKBitmap(image_width, image_height);

            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);
            }

            Draw_On_Bitmap(bitmap, individual);


            return bitmap;
        }

        private void Draw__Rooms(
            SKCanvas canvas,
            DS__Architectural_Plan individual
            )
        {

            var hatch = new SKPath();
            //hatch.AddCircle(0, 0, 2);
            hatch.AddRect(new SKRect(-2, -2, 2, 2));

            // draw the rooms
            using (SKPaint room_fill_brush = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black
            })
            using (SKPaint room_hatch_brush = new SKPaint()
            {
                PathEffect = SKPathEffect.Create2DPath(SKMatrix.CreateScale(6, 4), hatch),
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black,
                //StrokeWidth = 1
            })
            using (SKPaint room_perimeter_paint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = room_perimeter__color
            })
            {

                foreach (var kvp in individual.space_unit__per__cell)
                {
                    int cell_id = kvp.Key;
                    int room_id = kvp.Value;

                    if (room_id == -1)
                    {
                        continue;
                    }

                    List<Vec2d> boundary =
                        individual
                        .voronoi_tessellation
                        .Q__Cell__Boundary_Points(cell_id);

                    List<SKPoint> boundary_points = new List<SKPoint>(boundary.Count);
                    foreach (var p in boundary)
                    {
                        SKPoint pp = new SKPoint(
                            (float)p.x * scale,
                            (float)p.y * scale
                            );
                        boundary_points.Add(pp);
                    }

                    SKPath path = new SKPath();
                    path.AddPoly(boundary_points.ToArray());

                    RGB_Color acolor = individual.prescription.color__per__space_unit[room_id];
                    SKColor room_color = new SKColor((byte)acolor.r, (byte)acolor.g, (byte)acolor.b);

                    var room_type = individual.prescription.type__per__space_unit[room_id];

                    if (room_type == Space_Unit__Type.EXTERIOR)
                    {
                        room_hatch_brush.Color = room_color;
                        canvas.DrawPath(path, room_hatch_brush);
                    }
                    else
                    {
                        room_fill_brush.Color = room_color;
                        canvas.DrawPath(path, room_fill_brush);
                    }

                }
            }

            List<int> existing_rooms = individual.Q__Existing_Space_Units();

            using (SKPaint lines_paint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = room_perimeter__color,
                StrokeWidth = room_perimeter__pen_width
            }
            )
            {
                foreach (var room in existing_rooms)
                {

                    var room_perimeter_lines = individual.Q__Space_Unit__Perimeter_Lines__Unordered(room);

                    foreach (var line in room_perimeter_lines)
                    {
                        SKPoint p0 = new SKPoint(
                            (float)line.p0.x * scale,
                            (float)line.p0.y * scale
                            );

                        SKPoint p1 = new SKPoint(
                            (float)line.p1.x * scale,
                            (float)line.p1.y * scale
                            );

                        canvas.DrawLine(p0, p1, lines_paint);
                    }


                }
            }
        }

        private void Draw__Interior_Doors(
            SKCanvas canvas,
            DS__Architectural_Plan individual
            )
        {
            // draw the interior doors
            using (
                SKPaint doors_paint = new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.Red
                }
            )
            using (
                SKPaint doors_stroke = new SKPaint()
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Black,
                    StrokeWidth = 3.0f
                }
            )
            {
                var interior_doors = individual.Q__Connection_Doors();

                foreach (var interior_door in interior_doors)
                {
                    var cells_edge = interior_door.cells_connection;

                    var lines_1 =
                        individual
                        .voronoi_tessellation
                        .perimeter_lines__per__cell[cells_edge.v1];

                    var lines_2 =
                        individual
                        .voronoi_tessellation
                        .perimeter_lines__per__cell[cells_edge.v2];

                    Line_Segment common_line = lines_1.Find(x => lines_2.Contains(x));

                    Vec2d pc = (common_line.p0 + common_line.p1) / 2.0;

                    float x = (float)(pc.x) * scale;
                    float y = (float)(pc.y) * scale;

                    canvas.DrawCircle(
                        x,
                        y,
                        0.25f * scale,
                        doors_paint
                        );
                    canvas.DrawCircle(
                        x,
                        y,
                        0.25f * scale,
                        doors_stroke
                        );
                }
            }
        }

        private void Draw__Exterior_Doors(
            SKCanvas canvas,
            DS__Architectural_Plan individual
            )
        {
            // draw the exterior doors
            using (
                SKPaint doors_paint = new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.Green
                }
            )
            using (
                SKPaint doors_stroke = new SKPaint()
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Black,
                    StrokeWidth = 2.0f
                }
            )
            {
                var exterior_doors = individual.Q__Entrance_Doors();

                foreach (var door in exterior_doors)
                {
                    var cells_edge = door.cells_connection;

                    var lines_1 =
                        individual
                        .voronoi_tessellation
                        .perimeter_lines__per__cell[cells_edge.v1];

                    var lines_2 =
                        individual
                        .voronoi_tessellation
                        .perimeter_lines__per__cell[cells_edge.v2];

                    Line_Segment common_line = lines_1.Find(x => lines_2.Contains(x));

                    Vec2d pc = (common_line.p0 + common_line.p1) / 2.0;

                    float x = (float)(pc.x) * scale;
                    float y = (float)(pc.y) * scale;

                    canvas.DrawCircle(
                        x,
                        y,
                        0.4f * scale,
                        doors_paint
                        );
                    canvas.DrawCircle(
                        x,
                        y,
                        0.4f * scale,
                        doors_stroke
                        );
                }
            }
        }

        private void Draw__Windows(
            SKCanvas canvas,
            DS__Architectural_Plan individual
            )
        {
            // draw the windows
            using (
                SKPaint windows_paint = new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.Cyan
                }
            )
            using (
                SKPaint windows_stroke = new SKPaint()
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Black,
                    StrokeWidth = 2.0f
                }
            )
            {
                var windows = individual.Q__Windows();

                foreach (var window in windows)
                {
                    var cells_edge = window.cells_connection;

                    var lines_1 =
                        individual
                        .voronoi_tessellation
                        .perimeter_lines__per__cell[cells_edge.v1];

                    var lines_2 =
                        individual
                        .voronoi_tessellation
                        .perimeter_lines__per__cell[cells_edge.v2];

                    Line_Segment common_line = lines_1.Find(x => lines_2.Contains(x));

                    Vec2d pc = (common_line.p0 + common_line.p1) / 2.0;

                    float x = (float)(pc.x) * scale;
                    float y = (float)(pc.y) * scale;

                    canvas.DrawCircle(
                        x,
                        y,
                        0.25f * scale,
                        windows_paint
                        );
                    canvas.DrawCircle(
                        x,
                        y,
                        0.25f * scale,
                        windows_stroke
                        );
                }
            }
        }

        private void Draw__Plan_Graph(
            SKCanvas canvas,
            DS__Architectural_Plan individual
            )
        {
            using (SKPaint plan_graph_lines_paint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = plan_graph__color,
                StrokeWidth = plan_graph__stroke_width,
                PathEffect =
                    SKPathEffect.CreateDash(new float[] { plan_graph__dash_size, plan_graph__dash_size }, 0)
            })
            using (SKPaint plan_graph_circles_paint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = plan_graph__color
            })
            {
                var edges = individual.plan_graph.graph.Q__Edges();

                foreach (var edge in edges)
                {
                    Vec2d p1 = individual.plan_graph.location__per__vertex[edge.v1];
                    Vec2d p2 = individual.plan_graph.location__per__vertex[edge.v2];

                    canvas.DrawLine(
                        new SKPoint((float)p1.x * scale, (float)p1.y * scale),
                        new SKPoint((float)p2.x * scale, (float)p2.y * scale),
                        plan_graph_lines_paint
                        );
                }

                foreach (var kvp in individual.plan_graph.location__per__vertex)
                {
                    int vertex = kvp.Key;

                    var vertex_type = individual.plan_graph.type__per__vertex[vertex];
                    float radius;
                    if (vertex_type == PWG__Vertex_Type.CENTROID)
                    {
                        radius = 4.0f;
                    }
                    else
                    {
                        radius = 2.5f;
                    }

                    var point = kvp.Value;
                    canvas.DrawCircle(
                        new SKPoint((float)point.x * scale, (float)point.y * scale),
                        radius,
                        plan_graph_circles_paint
                        );
                }

                //var plan_graph = individual.Q__Plan__Sub_Graph();

                //var edges = plan_graph.Q__Edges();

                //foreach (var edge in edges)
                //{
                //    Vec2d p1 = individual.voronoi_tessellation.centroids[edge.v1];
                //    Vec2d p2 = individual.voronoi_tessellation.centroids[edge.v2];

                //    canvas.DrawLine(
                //        new SKPoint((float)p1.x * scale, (float)p1.y * scale),
                //        new SKPoint((float)p2.x * scale, (float)p2.y * scale),
                //        plan_graph_lines_paint
                //        );
                //}
            }
        }

        /*
         var hatch = new SKPath();
hatch.AddCircle(0, 0, 1);
var hatchPaint = new SKPaint {
   PathEffect = SKPathEffect.Create2DPath(SKMatrix.MakeScale(7, 7), hatch),
   Color = SKColors.RosyBrown,
   Style = SKPaintStyle.Stroke,
   StrokeWidth = 3
};
         */

        public override int Q__Image_Width(DS__Architectural_Plan data_structure)
        {
            return (int)(data_structure.voronoi_tessellation.bounding_rectangle.width * scale);
        }

        public override int Q__Image_Height(DS__Architectural_Plan data_structure)
        {
            return (int)(data_structure.voronoi_tessellation.bounding_rectangle.height * scale);
        }


    }
}
