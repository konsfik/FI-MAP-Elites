using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using SkiaSharp;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class VM__Layout_Constraints : Visualization_Method<DS__Layout_Constraints>
    {
        public readonly int image_width;
        public readonly int image_height;

        public VM__Layout_Constraints()
        {
            this.image_width = 1000;
            this.image_height = 1000;
        }

        public VM__Layout_Constraints(VM__Layout_Constraints vm_simple)
        {
            this.image_width = vm_simple.image_width;
            this.image_height = vm_simple.image_height;
        }

        public override object Q__Deep_Copy()
        {
            return new VM__Layout_Constraints(this);
        }

        public override void Draw_On_Bitmap(
            SKBitmap image, 
            DS__Layout_Constraints individual
            )
        {
            float node_radius = 20.0f;
            using (SKCanvas bufferGraphics = new SKCanvas(image))
            using (SKPaint edge_pen = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 2
            }
            )
            using (SKFontStyle font_style = SKFontStyle.Normal)
            using (SKTypeface type_face = SKTypeface.FromFamilyName("Arial", font_style))
            using (SKFont text_font = new SKFont(type_face, size: 18))
            using (SKPaint ellipse_fill_brush = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red
            }
            )
            using (SKPaint ellipse_stroke_brush = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 2
            }
            )
            using (SKPaint text_brush = new SKPaint() { Color = SKColors.Black })
            {
                bufferGraphics.Clear(SKColors.White);

                // draw the title...
                bufferGraphics.DrawText(
                    individual.name,
                    new SKPoint(500, 64),
                    text_brush
                    );

                List<Undirected_Edge> edges = individual.graph.Q__Edges();

                for (int i = 0; i < edges.Count; i++)
                {
                    Undirected_Edge edge = edges[i];

                    double room_1_angle =
                        (double)edge.v1 / (double)individual.name__per__space_unit.Count;
                    room_1_angle = room_1_angle * 2.0 * Math.PI;
                    double x1 = Math.Sin(room_1_angle) * 300 + 500;
                    double y1 = Math.Cos(room_1_angle) * 300 + 500;

                    double room_2_angle =
                        (double)edge.v2 / (double)individual.name__per__space_unit.Count;
                    room_2_angle = room_2_angle * 2.0 * Math.PI;
                    double x2 = Math.Sin(room_2_angle) * 300 + 500;
                    double y2 = Math.Cos(room_2_angle) * 300 + 500;

                    bufferGraphics.DrawLine(
                        new SKPoint((int)x1, (int)y1),
                        new SKPoint((int)x2, (int)y2),
                        edge_pen
                        );
                }

                int cnt = 0;
                foreach (var kvp in individual.color__per__space_unit)
                {
                    int room_id = kvp.Key;
                    string room_name = individual.name__per__space_unit[room_id];
                    RGB_Color acolor = kvp.Value;
                    double room_area = individual.area__per__space_unit[room_id];

                    double angle = (double)cnt / (double)individual.color__per__space_unit.Count;
                    angle = angle * 2.0 * Math.PI;

                    float x = (float)Math.Sin(angle) * 300.0f + 500.0f;
                    float y = (float)Math.Cos(angle) * 300.0f + 500.0f;

                    ellipse_fill_brush.Color = new SKColor(
                        (byte)acolor.r,
                        (byte)acolor.g,
                        (byte)acolor.b
                        );

                    SKPoint circle_center = new SKPoint((float)x, (float)y);
                    bufferGraphics.DrawCircle(circle_center, node_radius, ellipse_fill_brush);
                    bufferGraphics.DrawCircle(circle_center, node_radius, ellipse_stroke_brush);

                    string room_description = "(" + room_id.ToString() + "): " + room_name + "  ";
                    room_description += room_area.ToString() + " s.m.";

                    SKPoint text_point = new SKPoint(
                        (float)x,
                        (float)y - node_radius * 3.0f
                        );

                    bufferGraphics.DrawText(room_description, text_point, text_brush);

                    cnt++;
                }
            }
        }

        public override SKBitmap Generate_Bitmap(
            DS__Layout_Constraints individual
            )
        {
            SKBitmap image = new SKBitmap(image_width, image_height);
            Draw_On_Bitmap(image, individual);
            return image;
        }

        public override int Q__Image_Height(DS__Layout_Constraints data_structure)
        {
            return image_height;
        }

        public override int Q__Image_Width(DS__Layout_Constraints data_structure)
        {
            return image_width;
        }
    }
}
