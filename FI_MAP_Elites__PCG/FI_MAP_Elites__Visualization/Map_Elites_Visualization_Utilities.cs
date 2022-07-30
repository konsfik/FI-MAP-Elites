using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using SkiaSharp;

using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Algorithms.CMCE;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public static class Map_Elites_Visualization_Utilities
    {
        public static SKBitmap Draw<T>(
            this FIME__Archive<T> state,
            Visualization_Method<T> solution_visualizer,
            int gap_size,
            Selection_Window selection_window
            ) where T : Data_Structure
        {
            SKColor empty_cell_color = new SKColor(160, 120, 120);
            SKColor grid_lines_color = new  SKColor(64, 48, 48);

            T[,] individuals_table = state.Q__Individuals__Table_2D();
            bool[,] individual_exists_table = state.Q__Individual_Exists__Table_2D();
            double[,] fitness_table = state.Q__Fitness__Table_2D();

            T sample_individual = null;
            foreach (var ind in individuals_table)
            {
                if (ind != null)
                {
                    sample_individual = ind;
                    break;
                }
            }
            if (sample_individual == null)
            {
                SKBitmap b = new SKBitmap(10, 10);
                return b;
            }

            int data_table_width = state.tessellation.num_cells__per__bc[0];
            int data_table_height = state.tessellation.num_cells__per__bc[1];

            int single_image_width = solution_visualizer.Q__Image_Width(sample_individual);
            int single_image_height = solution_visualizer.Q__Image_Height(sample_individual);

            int full_image_width = (int)(data_table_width * single_image_width + data_table_width * gap_size);
            int full_image_height = (int)(data_table_height * single_image_height + data_table_height * gap_size);

            SKBitmap full_bitmap = new SKBitmap(full_image_width, full_image_height);

            using (SKCanvas full_canvas = new SKCanvas(full_bitmap))
            using (SKPaint paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 0 })
            {
                full_canvas.Clear(grid_lines_color);

                for (int x = 0; x < data_table_width; x++)
                {
                    for (int y = 0; y < data_table_height; y++)
                    {
                        float offset_x = x * single_image_width + x * gap_size;
                        float offset_y = y * single_image_height + y * gap_size;
                        if (individual_exists_table[x, y] == false)
                        {
                            paint.Color = empty_cell_color;
                            SKRect rect = new SKRect(
                                offset_x,
                                offset_y,
                                offset_x + single_image_width,
                                offset_y + single_image_height
                                );
                            full_canvas.DrawRect(rect, paint);
                        }
                        else
                        {
                            double fit = fitness_table[x, y];
                            byte c = (byte)(fit * 255);
                            SKColor cc = new SKColor(c, c, c);
                            paint.Color = cc;
                            SKRect rect = new SKRect(
                                offset_x,
                                offset_y,
                                offset_x + single_image_width,
                                offset_y + single_image_height
                                );
                            full_canvas.DrawRect(rect, paint);
                        }
                    }
                }

                for (int x = 0; x < data_table_width; x++)
                {
                    for (int y = 0; y < data_table_height; y++)
                    {
                        float offset_x = x * single_image_width + x * gap_size;
                        float offset_y = y * single_image_height + y * gap_size;

                        T ind = individuals_table[x, y];
                        if (ind != null)
                        {
                            SKBitmap individual_bitmap = solution_visualizer.Generate_Bitmap(ind);
                            SKPoint p = new SKPoint(offset_x, offset_y);
                            full_canvas.DrawBitmap(individual_bitmap, p);
                        }
                    }
                }

                if (selection_window != null)
                {
                    using (SKPaint selection_window_paint = new SKPaint { IsStroke = true, Color = SKColors.Red, StrokeWidth = 2 })
                    {
                        int[] min_coords = selection_window.Q__Min_Coords();
                        int[] max_coords = selection_window.Q__Max_Coords();

                        int min_x = min_coords[0];
                        int min_y = min_coords[1];
                        float fmin_x = min_x * single_image_width + min_x * gap_size;
                        float fmin_y = min_y * single_image_height + min_y * gap_size;

                        int max_x = max_coords[0] + 1;
                        int max_y = max_coords[1] + 1;
                        float fmax_x = max_x * single_image_width + max_x * gap_size;
                        float fmax_y = max_y * single_image_height + max_y * gap_size;

                        SKRect rect = new SKRect(
                                fmin_x,
                                fmin_y,
                                fmax_x,
                                fmax_y
                                );
                        full_canvas.DrawRect(rect, selection_window_paint);
                    }
                }
            }

            return full_bitmap;
        }

    }
}
