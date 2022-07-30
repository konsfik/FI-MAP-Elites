using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using SkiaSharp;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class VM__Voronoi_Simple : Visualization_Method<DS__Voronoi>
    {
        public readonly float scale;

        public readonly bool background;
        public readonly SKColor background_color;

        public readonly bool cells_boundary;
        public readonly float cells_boundary_width;
        public readonly SKColor cells_boundary_color;

        public readonly bool cells_fill;
        public readonly SKColor cells_fill_color;

        public readonly bool points;
        public readonly float points_radius;
        public readonly SKColor points_color;

        public readonly bool points_connections;
        public readonly float points_connections_stroke_width;
        public readonly float points_connections_dash_size;
        public readonly SKColor points_connections_color;

        public readonly bool centroids;
        public readonly float centroids_radius;
        public readonly SKColor centroids_color;

        public readonly bool centroids_connections;
        public readonly float centroids_connections_stroke_width;
        public readonly float centroids_connections_dash_size;
        public readonly SKColor centroids_connections_color;

        public VM__Voronoi_Simple(
            float scale,

            bool background,
            SKColor background_color,

            bool cells_boundary,
            float cells_boundary_width,

            bool cells_fill,

            bool points,
            float points_radius,

            bool points_connections,
            float points_connections_stroke_width,
            float points_connections_dash_size,

            bool centroids,
            float centroids_radius,

            bool centroids_connections,
            float centroids_connections_stroke_width,
            float centroids_connections_dash_size
            ) : this(
                scale: scale,

                background: background,
                background_color: background_color,

                cells_boundary: cells_boundary,
                cells_boundary_width: cells_boundary_width,
                cells_boundary_color: SKColors.Black,

                cells_fill: cells_fill,
                cells_fill_color: SKColors.White,

                points: points,
                points_radius: points_radius,
                points_color: SKColors.Black,

                points_connections: points_connections,
                points_connections_stroke_width: points_connections_stroke_width,
                points_connections_dash_size: points_connections_dash_size,
                points_connections_color: SKColors.Black,

                centroids: centroids,
                centroids_radius: centroids_radius,
                centroids_color: SKColors.Black,

                centroids_connections: centroids_connections,
                centroids_connections_stroke_width: centroids_connections_stroke_width,
                centroids_connections_dash_size: centroids_connections_dash_size,
                centroids_connections_color: SKColors.Black
                )
        {

        }

        public VM__Voronoi_Simple(
            float scale,

            bool background,
            SKColor background_color,

            bool cells_boundary,
            float cells_boundary_width,
            SKColor cells_boundary_color,

            bool cells_fill,
            SKColor cells_fill_color,

            bool points_connections,
            float points_connections_stroke_width,
            float points_connections_dash_size,
            SKColor points_connections_color,

            bool points,
            float points_radius,
            SKColor points_color,

            bool centroids,
            float centroids_radius,
            SKColor centroids_color,

            bool centroids_connections,
            float centroids_connections_stroke_width,
            float centroids_connections_dash_size,
            SKColor centroids_connections_color
            )
        {
            this.scale = scale;

            this.background = background;
            this.background_color = background_color;

            this.cells_boundary = cells_boundary;
            this.cells_boundary_width = cells_boundary_width;
            this.cells_boundary_color = cells_boundary_color;

            this.cells_fill = cells_fill;
            this.cells_fill_color = cells_fill_color;

            this.points = points;
            this.points_radius = points_radius;
            this.points_color = points_color;

            this.points_connections = points_connections;
            this.points_connections_stroke_width = points_connections_stroke_width;
            this.points_connections_dash_size = points_connections_dash_size;
            this.points_connections_color = points_connections_color;

            this.centroids = centroids;
            this.centroids_radius = centroids_radius;
            this.centroids_color = centroids_color;

            this.centroids_connections = centroids_connections;
            this.centroids_connections_stroke_width = centroids_connections_stroke_width;
            this.centroids_connections_dash_size = centroids_connections_dash_size;
            this.centroids_connections_color = centroids_connections_color;
        }

        private VM__Voronoi_Simple(VM__Voronoi_Simple vm_to_copy)
        {
            this.scale = vm_to_copy.scale;

            this.background = vm_to_copy.background;
            this.background_color = vm_to_copy.background_color;

            this.cells_boundary = vm_to_copy.cells_boundary;
            this.cells_boundary_width = vm_to_copy.cells_boundary_width;
            this.cells_boundary_color = vm_to_copy.cells_boundary_color;

            this.cells_fill = vm_to_copy.cells_fill;
            this.cells_fill_color = vm_to_copy.cells_fill_color;

            this.points = vm_to_copy.points;
            this.points_radius = vm_to_copy.points_radius;
            this.points_color = vm_to_copy.points_color;

            this.points_connections = vm_to_copy.points_connections;
            this.points_connections_stroke_width = vm_to_copy.points_connections_stroke_width;
            this.points_connections_dash_size = vm_to_copy.points_connections_dash_size;
            this.points_connections_color = vm_to_copy.points_connections_color;

            this.centroids = vm_to_copy.centroids;
            this.centroids_radius = vm_to_copy.centroids_radius;
            this.centroids_color = vm_to_copy.centroids_color;

            this.centroids_connections = vm_to_copy.centroids_connections;
            this.centroids_connections_stroke_width = vm_to_copy.centroids_connections_stroke_width;
            this.centroids_connections_dash_size = vm_to_copy.centroids_connections_dash_size;
            this.centroids_connections_color = vm_to_copy.centroids_connections_color;
        }

        public override object Q__Deep_Copy()
        {
            return new VM__Voronoi_Simple(this);
        }

        public override void Draw_On_Bitmap(SKBitmap image, DS__Voronoi individual)
        {
            DS__Voronoi__Visualization_Methods.Draw_Voronoi_Structure(
                image: image,
                voronoi_structure: individual,
                offset_x: 0.0f,
                offset_y: 0.0f,
                scale: scale,

                background: background,
                background_color: background_color,

                cells_boundary: cells_boundary,
                cells_boundary_width: cells_boundary_width,
                cells_boundary_color: cells_boundary_color,

                cells_fill: cells_fill,
                cells_fill_color: cells_fill_color,

                points_connections: points_connections,
                points_connections_stroke_width: points_connections_stroke_width,
                points_connections_dash_size: points_connections_dash_size,
                points_connections_color: points_connections_color,

                points: points,
                points_radius: points_radius,
                points_color: points_color,

                centroids: centroids,
                centroids_radius: centroids_radius,
                centroids_color: centroids_color,

                centroids_connections: centroids_connections,
                centroids_connections_stroke_width: centroids_connections_stroke_width,
                centroids_connections_dash_size: centroids_connections_dash_size,
                centroids_connections_color: centroids_connections_color
                );
        }

        public override SKBitmap Generate_Bitmap(DS__Voronoi individual)
        {
            int image_width = Q__Image_Width(individual);
            int image_height = Q__Image_Height(individual);

            SKBitmap bitmap = new SKBitmap(image_width, image_height);

            Draw_On_Bitmap(bitmap, individual);

            return bitmap;
        }

        public override int Q__Image_Height(DS__Voronoi data_structure)
        {
            int width = (int)(data_structure.bounding_rectangle.width * scale);
            return width;
        }

        public override int Q__Image_Width(DS__Voronoi data_structure)
        {
            int height = (int)(data_structure.bounding_rectangle.height * scale);
            return height;
        }
    }
}
