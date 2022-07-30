using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using SkiaSharp;
using FI_MAP_Elites__PCG.Data_Structures.Voronoi;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public static class Architectural_Plan__Visualization_Utilities
    {
        public static SKBitmap Generate_Bitmap(
            DS__Architectural_Plan solution,

            float offset_x,
            float offset_y,
            float scale,

            bool fill_cells,
            bool fill_room_colors,

            bool room_outlines,
            float room_outlines__pen_width,

            bool cell_boundaries,
            float cell_boundaries__pen_width,

            float delaunay_edges_pen_width,

            bool legal_connections,
            float legal_connections_pen_width,

            bool draw_points,
            float points_radius
            )
        {
            SKBitmap image = new SKBitmap(
                (int)(solution.voronoi_tessellation.bounding_rectangle.width * scale),
                (int)(solution.voronoi_tessellation.bounding_rectangle.height * scale)
                );


            SKColor cells_fill_color = new SKColor(255, 255, 255, 255);

            SKColor cells_stroke_color = new SKColor(0, 0, 0, 255);
            SKColor delaunay_connectivity_color = new SKColor(255, 0, 0, 255);
            SKColor legal_connectivity_color = new SKColor(0, 0, 0, 255);
            SKColor points_color = new SKColor(0, 0, 0, 255);

            using (SKCanvas graphics = new SKCanvas(image))
            using (SKPaint delaunay_edges_pen = new SKPaint()
            {
                Color = delaunay_connectivity_color,
                StrokeWidth = delaunay_edges_pen_width
            })
            using (SKPaint legal_connectivity_pen = new SKPaint()
            {
                Color = legal_connectivity_color,
                StrokeWidth = legal_connections_pen_width
            })
            using (SKPaint cell_boundaries_pen = new SKPaint()
            {
                Color = cells_stroke_color,
                StrokeWidth = cell_boundaries__pen_width
            })
            using (SKPaint points_brush = new SKPaint()
            {
                Color = points_color
            })
            using (SKPaint cells_brush = new SKPaint()
            {
                Color = cells_fill_color
            })
            using (SKPaint room_perimeter_pen = new SKPaint()
            {
                Color = SKColors.Black,
                StrokeWidth = room_outlines__pen_width
            })
            {
                graphics.Clear(SKColors.White);

                float[] dashValues = { 8, 4, 8, 4 };

                //delaunay_edges_pen.DashPattern = dashValues;

                float[] legal_connectivity_pen_dash_values = { 8, 4, 8, 4 };
                //legal_connectivity_pen.DashPattern = legal_connectivity_pen_dash_values;

                if (fill_cells)
                {
                    solution.voronoi_tessellation.Fill_Cells(
                        graphics,
                        offset_x,
                        offset_y,
                        scale,
                        cells_brush
                        );
                }

                if (fill_room_colors)
                {
                    // draw the rooms
                    foreach (var kvp in solution.space_unit__per__cell)
                    {
                        int cell_id = kvp.Key;
                        int room_id = kvp.Value;

                        if (room_id == -1)
                        {
                            continue;
                        }

                        RGB_Color acolor = solution.prescription.Q__Prescribed__Space_Unit__Color(room_id);
                        SKColor room_color = new SKColor((byte)acolor.r, (byte)acolor.g, (byte)acolor.b);

                        List<Vec2d> boundary =
                            solution
                            .voronoi_tessellation
                            .Q__Cell__Boundary_Points(cell_id);

                        List<SKPoint> boundary_points =
                            boundary
                            .Convert__To__SKPoint_List(
                                offset_x,
                                offset_y,
                                scale
                                );

                        using (SKPath room_path = new SKPath())
                        using (SKPaint room_brush = new SKPaint()
                        {
                            Color = room_color
                        })
                        {
                            foreach (var bp in boundary_points)
                            {
                                room_path.MoveTo(bp);
                            }
                            room_path.Close();

                            graphics.DrawPath(room_path, room_brush);

                        }
                    }
                }

                if (cell_boundaries)
                {
                    solution.voronoi_tessellation.Draw_Cell_Boundaries(
                        graphics,
                        offset_x,
                        offset_y,
                        scale,
                        cell_boundaries_pen
                        );
                }

                if (room_outlines)
                {
                    List<int> existing_rooms = solution.Q__Existing_Space_Units();
                    foreach (var room in existing_rooms)
                    {
                        var perimeter_lines = solution.Q__Space_Unit__Perimeter_Lines__Unordered(room);

                        foreach (var line in perimeter_lines)
                        {
                            graphics.DrawLine(
                                line.p0.Convert__To__System_Drawing_PointF(offset_x, offset_y, scale),
                                line.p1.Convert__To__System_Drawing_PointF(offset_x, offset_y, scale),
                                room_perimeter_pen
                                );
                        }
                    }
                }

                if (legal_connections)
                {
                    solution.voronoi_tessellation.Draw_Centroids_Connections(
                        graphics,
                        legal_connectivity_pen,
                        offset_x,
                        offset_y,
                        scale
                        );
                }

                if (draw_points)
                {
                    solution.voronoi_tessellation.Draw_Points(
                        graphics,
                        points_brush,
                        offset_x: offset_x,
                        offset_y: offset_y,
                        scale: scale,
                        radius: points_radius
                        );
                }

            }

            return image;
        }

        public static void Draw_Solution(
            this SKBitmap image,
            DS__Architectural_Plan solution,

            float offset_x,
            float offset_y,
            float scale,

            bool fill_cells,
            bool fill_room_colors,

            bool room_outlines,
            float room_outlines__pen_width,

            bool cell_boundaries,
            float cell_boundaries__pen_width,

            float delaunay_edges_pen_width,

            bool legal_connections,
            float legal_connections_pen_width,

            bool draw_points,
            float points_radius
            )
        {
            //Color background_color = Color.FromArgb(255, 64, 64, 64);
            //image.Clear(background_color);

            SKColor good_cells_fill_color = new SKColor(255, 255, 255, 255);
            SKColor bad_cells_fill_color = new SKColor(255, 160, 255, 255);
            SKColor out_cells_fill_color = new SKColor(200, 200, 200, 255);

            SKColor cells_stroke_color = new SKColor(0, 0, 0, 255);
            SKColor delaunay_connectivity_color = new SKColor(255, 0, 0, 255);
            SKColor legal_connectivity_color = new SKColor(0, 0, 0, 255);
            SKColor points_color = new SKColor(0, 0, 0, 255);

            using (SKCanvas graphics = new SKCanvas(image))
            using (SKPaint delaunay_edges_pen = new SKPaint()
            {
                Color = delaunay_connectivity_color,
                StrokeWidth = delaunay_edges_pen_width
            })
            using (SKPaint legal_connectivity_pen = new SKPaint()
            {
                Color = legal_connectivity_color,
                StrokeWidth = legal_connections_pen_width
            })
            using (SKPaint cell_boundaries_pen = new SKPaint()
            {
                Color = cells_stroke_color,
                StrokeWidth = cell_boundaries__pen_width
            })
            using (SKPaint points_brush = new SKPaint()
            {
                Color = points_color
            })
            using (SKPaint cells_brush = new SKPaint()
            {
                Color = good_cells_fill_color
            })
            //using (HatchBrush room_brush = new HatchBrush(HatchStyle.Horizontal, Color.White, Color.White))
            using (SKPaint room_perimeter_pen = new SKPaint()
            {
                Color = SKColors.Black,
                StrokeWidth = room_outlines__pen_width
            })
            {
                //float[] dashValues = { 8, 4, 8, 4 };
                //delaunay_edges_pen.DashPattern = dashValues;

                //float[] legal_connectivity_pen_dash_values = { 8, 4, 8, 4 };
                //legal_connectivity_pen.DashPattern = legal_connectivity_pen_dash_values;

                if (fill_cells)
                {
                    solution.voronoi_tessellation.Fill_Cells(
                        graphics,
                        offset_x,
                        offset_y,
                        scale,
                        cells_brush
                        );
                }

                if (fill_room_colors)
                {
                    // draw the rooms
                    foreach (var kvp in solution.space_unit__per__cell)
                    {
                        int cell_id = kvp.Key;
                        int room_id = kvp.Value;

                        if (room_id == -1)
                        {
                            continue;
                        }

                        RGB_Color acolor = solution.prescription.Q__Prescribed__Space_Unit__Color(room_id);
                        SKColor room_color = new SKColor((byte)acolor.r, (byte)acolor.g, (byte)acolor.b);

                        List<Vec2d> boundary =
                            solution
                            .voronoi_tessellation
                            .Q__Cell__Boundary_Points(cell_id);

                        List<SKPoint> boundary_points =
                            boundary
                            .Convert__To__SKPoint_List(
                                offset_x,
                                offset_y,
                                scale
                                );

                        using (SKPaint room_brush = new SKPaint() { Color = room_color })
                        using (SKPath room_path = new SKPath())
                        {
                            foreach (var bp in boundary_points)
                                room_path.MoveTo(bp);
                            room_path.Close();

                            graphics.DrawPath(room_path, room_brush);
                        }
                    }
                }

                if (cell_boundaries)
                {
                    solution.voronoi_tessellation.Draw_Cell_Boundaries(
                        graphics,
                        offset_x,
                        offset_y,
                        scale,
                        cell_boundaries_pen
                        );
                }

                if (room_outlines)
                {
                    List<int> existing_rooms = solution.Q__Existing_Space_Units();
                    foreach (var room in existing_rooms)
                    {
                        var perimeter_lines = solution.Q__Space_Unit__Perimeter_Lines__Unordered(room);

                        foreach (var line in perimeter_lines)
                        {
                            graphics.DrawLine(
                                line.p0.Convert__To__System_Drawing_PointF(offset_x, offset_y, scale),
                                line.p1.Convert__To__System_Drawing_PointF(offset_x, offset_y, scale),
                                room_perimeter_pen
                                );
                        }
                    }
                }

                if (legal_connections)
                {
                    solution.voronoi_tessellation.Draw_Centroids_Connections(
                        graphics,
                        legal_connectivity_pen,
                        offset_x,
                        offset_y,
                        scale
                        );
                }

                if (draw_points)
                {
                    solution.voronoi_tessellation.Draw_Points(
                        graphics,
                        points_brush,
                        offset_x: offset_x,
                        offset_y: offset_y,
                        scale: scale,
                        radius: points_radius
                        );
                }

            }
        }


        public static void Draw_Region_On_Graphics(
            SKCanvas graphics,
            List<Vec2d> region,
            SKColor color,
            float stroke_width,
            float scale
            )
        {
            using (SKPaint region_pen = new SKPaint()
            {
                Color = color,
                StrokeWidth = stroke_width
            })
            {
                for (int i = 0; i < region.Count; i++)
                {
                    Vec2d p0 = region[i];

                    Vec2d p1;
                    if (i < region.Count - 1)
                    {
                        p1 = region[i + 1];
                    }
                    else
                    {
                        p1 = region[0];
                    }
                    graphics.DrawLine(
                        new SKPoint(
                            (int)(p0.x * scale),
                            (int)(p0.y * scale)
                            ),
                        new SKPoint(
                            (int)(p1.x * scale),
                            (int)(p1.y * scale)
                            ),
                        region_pen
                    );
                    // draw line from p0 to p1

                }
            }

        }
    }


}
