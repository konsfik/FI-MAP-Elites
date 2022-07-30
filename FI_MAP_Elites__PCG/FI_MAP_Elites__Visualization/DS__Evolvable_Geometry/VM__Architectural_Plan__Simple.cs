using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;
using SkiaSharp;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class VM__Architectural_Plan__Simple : Visualization_Method<DS__Architectural_Plan>
    {
        public SKColor room_perimeter__color;
        public readonly float room_perimeter__pen_width;
        public readonly float scale;

        public VM__Architectural_Plan__Simple(
            SKColor room_perimeter__color,
            float room_perimeter__pen_width,
            float scale
            )
        {
            this.room_perimeter__color = room_perimeter__color;
            this.room_perimeter__pen_width = room_perimeter__pen_width;
            this.scale = scale;
        }

        private VM__Architectural_Plan__Simple(VM__Architectural_Plan__Simple vm_to_copy)
        {
            this.room_perimeter__color = vm_to_copy.room_perimeter__color;
            this.room_perimeter__pen_width = vm_to_copy.room_perimeter__pen_width;
            this.scale = vm_to_copy.scale;
        }

        public override object Q__Deep_Copy()
        {
            return new VM__Architectural_Plan__Simple(this);
        }

        public override void Draw_On_Bitmap(SKBitmap bitmap, DS__Architectural_Plan individual)
        {
            int image_width = bitmap.Width;
            int image_height = bitmap.Height;

            using (SKCanvas canvas = new SKCanvas(bitmap))
            using (SKPaint room_brush = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black
            })
            using (SKPaint room_perimeter_paint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = room_perimeter__color
            })
            {
                canvas.Scale(1, -1, 0, image_height / 2.0f);

                // draw the rooms
                foreach (var kvp in individual.space_unit__per__cell)
                {
                    int cell_id = kvp.Key;
                    int room_id = kvp.Value;

                    if (room_id == -1)
                    {
                        continue;
                    }

                    RGB_Color acolor = individual.prescription.color__per__space_unit[room_id];
                    SKColor room_color = new SKColor((byte)acolor.r, (byte)acolor.g, (byte)acolor.b);

                    room_brush.Color = room_color;

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

                    using (SKPaint room_paint = new SKPaint()
                    {
                        Style = SKPaintStyle.Fill,
                        Color = room_color
                    }
                    )
                    {
                        canvas.DrawPath(path, room_paint);
                    }

                }

                List<int> existing_rooms = individual.Q__Existing_Space_Units();

                using (SKPaint lines_paint = new SKPaint()
                {
                    Style = SKPaintStyle.Stroke,
                    Color = room_perimeter__color
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
                        Color = SKColors.Black
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
                            0.5f * scale,
                            doors_paint
                            );
                        canvas.DrawCircle(
                            x,
                            y,
                            0.5f * scale,
                            doors_stroke
                            );
                    }
                }

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
                        Color = SKColors.Black
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
                            0.75f * scale,
                            doors_paint
                            );
                        canvas.DrawCircle(
                            x,
                            y,
                            0.75f * scale,
                            doors_stroke
                            );
                    }
                }


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
                        Color = SKColors.Black
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
                            0.5f * scale,
                            windows_paint
                            );
                        canvas.DrawCircle(
                            x,
                            y,
                            0.5f * scale,
                            windows_stroke
                            );
                    }
                }


            }
        }

        public override SKBitmap Generate_Bitmap(DS__Architectural_Plan individual)
        {

            int image_width = Q__Image_Width(individual);
            int image_height = Q__Image_Height(individual);

            SKBitmap bitmap = new SKBitmap(image_width, image_height);

            Draw_On_Bitmap(bitmap, individual);

            
            return bitmap;
        }

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
