using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public partial class DS__Voronoi
        : Data_Structure
    {
        ///////////////////////////////
        // context
        ///////////////////////////////
        public readonly double epsilon;
        public Rect2d bounding_rectangle;
        public readonly double connectivity_threshold;

        ///////////////////////////////
        // genotype
        ///////////////////////////////
        public List<Vec2d> generator_points;


        ///////////////////////////////
        // phenotype
        ///////////////////////////////
        public List<Vec2d> voronoi_points;
        public List<Line_Segment> voronoi_lines;
        public List<List<Vec2d>> perimeter_points__per__cell;
        public List<Vec2d> centroids;
        public List<List<Line_Segment>> perimeter_lines__per__cell;
        public List<double> area__per__cell;
        public List<bool> is_active__per__cell;
        public DS__Undirected_Graph connectivity_graph;
        public DS__Undirected_Weighted_Graph weighted_connectivity_graph;

        public DS__Voronoi(
            List<Vec2d> generator_points,
            Rect2d bounding_rectangle,
            double connectivity_threshold
            ):
            this(
                generator_points, 
                bounding_rectangle,
                connectivity_threshold,
                0.0000_0000_01
                )
        {

        }

        public DS__Voronoi(
            List<Vec2d> generator_points,
            Rect2d bounding_rectangle,
            double connectivity_threshold,
            double epsilon
            )
        {
            // context
            this.bounding_rectangle = bounding_rectangle;
            this.connectivity_threshold = connectivity_threshold;
            this.epsilon = epsilon;

            // geno
            this.generator_points = generator_points.Q__Deep_Copy();

            // pheno
            this.M__Recalculate__Phenotype();

        }

        private DS__Voronoi(
            DS__Voronoi tess_to_copy
            )
        {
            // context
            this.bounding_rectangle = tess_to_copy.bounding_rectangle;
            this.connectivity_threshold = tess_to_copy.connectivity_threshold;
            this.epsilon = tess_to_copy.epsilon;
            
            // geno
            this.generator_points = tess_to_copy.generator_points.Q__Deep_Copy();

            // pheno
            this.voronoi_points = tess_to_copy.voronoi_points.Q__Deep_Copy();
            this.voronoi_lines = tess_to_copy.voronoi_lines.Q__Deep_Copy();
            this.perimeter_points__per__cell = tess_to_copy.perimeter_points__per__cell.Q__Deep_Copy();
            this.centroids = tess_to_copy.centroids.Q__Deep_Copy();
            this.perimeter_lines__per__cell = tess_to_copy.perimeter_lines__per__cell.Q__Deep_Copy();
            this.area__per__cell = tess_to_copy.area__per__cell.Q__Deep_Copy();
            this.is_active__per__cell = tess_to_copy.is_active__per__cell.Q__Deep_Copy();
            this.connectivity_graph = (DS__Undirected_Graph)tess_to_copy.connectivity_graph.Q__Deep_Copy();
            this.weighted_connectivity_graph = (DS__Undirected_Weighted_Graph)tess_to_copy.weighted_connectivity_graph.Q__Deep_Copy();
        }

        // json constructor
        public DS__Voronoi(
            // context
            Rect2d bounding_rectangle,
            double connectivity_threshold,
            double epsilon,

            // geno
            List<Vec2d> generator_points,

            // pheno
            List<Vec2d> voronoi_points,
            List<Line_Segment> voronoi_lines,
            List<List<Vec2d>> perimeter_points__per__cell,
            List<Vec2d> centroids,
            List<List<Line_Segment>> perimeter_lines__per__cell,
            List<double> area__per__cell,
            List<bool> is_active__per__cell,
            DS__Undirected_Graph connectivity_graph,
            DS__Undirected_Weighted_Graph weighted_connectivity_graph
            )
        {
            // context
            this.bounding_rectangle = bounding_rectangle;
            this.connectivity_threshold = connectivity_threshold;
            this.epsilon = epsilon;

            // geno
            this.generator_points = generator_points.Q__Deep_Copy();

            // pheno
            this.voronoi_points = voronoi_points.Q__Deep_Copy();
            this.voronoi_lines = voronoi_lines.Q__Deep_Copy();
            this.perimeter_points__per__cell = perimeter_points__per__cell.Q__Deep_Copy();
            this.centroids = centroids.Q__Deep_Copy();
            this.perimeter_lines__per__cell = perimeter_lines__per__cell.Q__Deep_Copy();
            this.area__per__cell = area__per__cell.Q__Deep_Copy();
            this.is_active__per__cell = is_active__per__cell.Q__Deep_Copy();
            this.connectivity_graph = (DS__Undirected_Graph)connectivity_graph.Q__Deep_Copy();
            this.weighted_connectivity_graph = (DS__Undirected_Weighted_Graph)weighted_connectivity_graph.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new DS__Voronoi(this);
        }
    }
}
