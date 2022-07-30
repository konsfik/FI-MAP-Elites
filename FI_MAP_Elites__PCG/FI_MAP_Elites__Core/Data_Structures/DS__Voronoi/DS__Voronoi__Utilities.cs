using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Voronoi;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;
using FI_MAP_Elites__PCG.Shared_Elements;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public static class DS__Voronoi__Utilities
    {

        public static List<bool> Extract__Is_Active_Per_Cell(
            this VoronoiBase tri_voronoi,
            Rect2d bounding_rectangle
            )
        {
            int num_points = tri_voronoi.Faces.Count;

            List<bool> is_active_per_cell = new List<bool>(capacity:num_points);
            for (int f = 0; f < num_points; f++)
            {
                var face = tri_voronoi.Faces[f];

                bool is_bounded = face.Bounded && face.Edge != null;
                bool is_within_bounds = true;

                if (is_bounded)
                {
                    foreach (var e in face.EnumerateEdges())
                    {
                        var p = e.Origin;
                        bool p_within =
                            bounding_rectangle
                            .Contains_Point(p.X, p.Y, false);

                        if (p_within == false)
                        {
                            is_within_bounds = false;
                            break;
                        }
                    }
                }

                is_active_per_cell.Add(is_bounded && is_within_bounds);
            }

            return is_active_per_cell;
        }

        public static Dictionary<Undirected_Edge, Line_Segment> Extract__Line__Per__Connection(
            DS__Undirected_Graph connectivity_graph,
            List<Line_Segment>[] perimeter_lines__per__cell
            )
        {
            Dictionary<Undirected_Edge, Line_Segment> line_segment__per__edge =
                new Dictionary<Undirected_Edge, Line_Segment>();

            var edges = connectivity_graph.Q__Edges();

            int num_edges = edges.Count;

            for (int i = 0; i < num_edges; i++)
            {
                Undirected_Edge edge = edges[i];
                List<Line_Segment> lines_1 = perimeter_lines__per__cell[edge.v1];
                List<Line_Segment> lines_2 = perimeter_lines__per__cell[edge.v2];

                Line_Segment common_line =
                    lines_1.Find(
                        x =>
                        lines_2.Contains(x)
                        );

                line_segment__per__edge.Add(edge, common_line);
            }

            return line_segment__per__edge;
        }

        public static DS__Undirected_Graph Extract__Connectivity_Graph(
            Mesh mesh,
            List<List<Line_Segment>> perimeter_lines__per__cell,
            List<bool> is_active__per__cell,
            double epsilon,
            double connectivity_threshold
            )
        {
            DS__Undirected_Graph connectivity_graph = new DS__Undirected_Graph();

            foreach (var vertex in mesh.Vertices)
            {
                bool is_active_vertex = is_active__per__cell[vertex.ID];

                if (is_active_vertex)
                {
                    connectivity_graph.M__Add_Vertex(vertex.ID);
                }
            }

            foreach (var e in mesh.Edges)
            {
                int v1 = e.P0;
                int v2 = e.P1;

                bool is_active_edge =
                    is_active__per__cell[e.P0] && is_active__per__cell[e.P1];

                if (is_active_edge)
                {
                    var face_1__lines = perimeter_lines__per__cell[v1];
                    var face_2__lines = perimeter_lines__per__cell[v2];

                    Line_Segment common_line = new Line_Segment(0, 0, 0, 0);
                    bool common_line_found = false;

                    foreach (var line_1 in face_1__lines) {
                        foreach (var line_2 in face_2__lines) {
                            if (line_1 == line_2) {
                                common_line = line_1;
                                common_line_found = true;
                                break;
                            }
                        }
                        if (common_line_found) break;
                    }

                    if (common_line_found == false) continue;
                    else {
                        double len = common_line.Q__Magnitude();
                        if (len >= connectivity_threshold) {
                            connectivity_graph.M__Add_Edge(e.P0, e.P1);
                        }
                    }
                }
            }

            return connectivity_graph;
        }

        public static void Extract__Voronoi_Points(
            this VoronoiBase tri_voronoi,
            List<bool> is_active__per__cell,
            double epsilon,
            out List<Vec2d> voronoi_points,
            out List<List<Vec2d>> perimeter_points__per__cell
            )
        {
            voronoi_points =
                tri_voronoi.Q__Approximately_Unique_Points(
                    is_active__per__cell,
                    epsilon
                    );

            perimeter_points__per__cell =
                tri_voronoi.Extract__Perimeter_Points__Per__Cell(
                    epsilon,
                    is_active__per__cell,
                    voronoi_points
                    );
        }

        private static List<List<Vec2d>> Extract__Perimeter_Points__Per__Cell(
            this VoronoiBase voronoi,
            double epsilon,
            List<bool> is_active__per__cell,
            List<Vec2d> voronoi_points
            )
        {
            int num_voronoi_faces = voronoi.Faces.Count();

            List<List<Vec2d>> perimeter_points__per__cell =
                new List<List<Vec2d>>(capacity: num_voronoi_faces);

            for (int f = 0; f < num_voronoi_faces; f++)
            {
                var face = voronoi.Faces[f];
                if (is_active__per__cell[f] == false)
                {
                    perimeter_points__per__cell.Add(new List<Vec2d>());
                }
                else
                {
                    var face_edges = face.EnumerateEdges().ToList();

                    List<Vec2d> perimeter_points = new List<Vec2d>();

                    int num_pts = face_edges.Count;

                    for (int i = 0; i < num_pts; i++)
                    {
                        Vec2d this_point =
                            new Vec2d(
                                face_edges[i].Origin.X,
                                face_edges[i].Origin.Y
                                );
                        bool is_first = (i == 0);
                        bool is_intermediate = (i > 0) && (i < num_pts - 1);
                        bool is_last = (i == num_pts - 1);
                        if (is_first)
                        {
                            perimeter_points.Add(this_point);

                        }
                        else if (is_intermediate)
                        {
                            Vec2d previous_point =
                                new Vec2d(
                                    face_edges[i - 1].Origin.X,
                                    face_edges[i - 1].Origin.Y
                                    );

                            if (this_point.Q__Approximately_Equal_To(previous_point, epsilon))
                            {
                                continue;
                            }
                            else
                            {
                                perimeter_points.Add(this_point);
                            }
                        }
                        else if (is_last)
                        {
                            Vec2d previous_point =
                                new Vec2d(
                                    face_edges[i - 1].Origin.X,
                                    face_edges[i - 1].Origin.Y
                                    );

                            Vec2d first_point =
                                new Vec2d(
                                    face_edges[0].Origin.X,
                                    face_edges[0].Origin.Y
                                    );

                            if (
                                this_point.Q__Approximately_Equal_To(previous_point, epsilon)
                                ||
                                this_point.Q__Approximately_Equal_To(first_point, epsilon)
                                )
                            {
                                continue;
                            }
                            else
                            {
                                perimeter_points.Add(this_point);
                            }
                        }
                    }
                    perimeter_points__per__cell.Add(perimeter_points);
                }
            }

            int num_pt_groups = perimeter_points__per__cell.Count;
            for (int i = 0; i < num_pt_groups; i++)
            {
                int num_pts = perimeter_points__per__cell[i].Count;
                for (int j = 0; j < num_pts; j++)
                {
                    Vec2d perimeter_point = perimeter_points__per__cell[i][j];

                    foreach (var existing_point in voronoi_points)
                    {
                        if (existing_point.Q__Approximately_Equal_To(perimeter_point, epsilon))
                        {
                            perimeter_points__per__cell[i][j] = existing_point;
                            break;
                        }
                    }
                }
            }

            return perimeter_points__per__cell;
        }





        public static Mesh Extract__Mesh__From__Points_List(
            List<Vec2d> points,
            Rect2d bounding_rectangle
            )
        {
            Vertex[] v_points = Convert__Vec2d_List__To__Vertex_Array(points);

            Polygon polygon = new Polygon();
            foreach (var v in v_points)
                polygon.Add(v);

            ConstraintOptions constraint_options = new ConstraintOptions
            {
                ConformingDelaunay = false,
                Convex = true,
                SegmentSplitting = 2
            };

            Mesh mesh = (Mesh)polygon.Triangulate(constraint_options);

            return mesh;
        }

        public static bool Q__Contains_Similar(
            this List<Vec2d> points,
            Vec2d point,
            double epsilon
            )
        {
            foreach (var p in points)
            {
                if (p.Q__Approximately_Equal_To(point, epsilon))
                    return true;
            }
            return false;
        }

        public static List<Vec2d> Q__Unique_Points(
            this VoronoiBase voronoi,
            List<bool> is_active__per__cell
            )
        {
            HashSet<Vec2d> unique_points = new HashSet<Vec2d>();

            int num_faces = voronoi.Faces.Count;

            for (int f = 0; f < num_faces; f++)
            {
                bool is_active = is_active__per__cell[f];
                if (is_active == false) continue;
                else
                {
                    var face = voronoi.Faces[f];
                    foreach (var he in face.EnumerateEdges())
                    {
                        var origin = he.Origin;
                        Vec2d p = new Vec2d(origin.X, origin.Y);
                        unique_points.Add(p);
                    }
                }
            }
            return unique_points.ToList();
        }

        public static List<Vec2d> Q__Approximately_Unique_Points(
            this VoronoiBase voronoi,
            List<bool> is_active__per__cell,
            double epsilon
            )
        {
            var unique_points = voronoi.Q__Unique_Points(is_active__per__cell);

            List<Vec2d> approximately_unique_points = new List<Vec2d>();

            foreach (var p in unique_points)
            {
                bool contained = approximately_unique_points.Q__Contains_Similar(p, epsilon);
                if (contained == false)
                {
                    approximately_unique_points.Add(p);
                }
            }

            return approximately_unique_points;
        }

        public static Vertex[] Convert__Vec2d_Array__To__Vertex_Array(
            Vec2d[] points
            )
        {
            int num_points = points.Length;
            Vertex[] converted_points = new Vertex[num_points];
            for (int i = 0; i < num_points; i++)
            {
                converted_points[i] = new Vertex(points[i].x, points[i].y);
            }
            return converted_points;
        }

        public static Vertex[] Convert__Vec2d_List__To__Vertex_Array(
            List<Vec2d> points
            )
        {
            int num_points = points.Count;
            Vertex[] converted_points = new Vertex[num_points];
            for (int i = 0; i < num_points; i++)
            {
                converted_points[i] = new Vertex(points[i].x, points[i].y);
            }
            return converted_points;
        }

        public static List<Vertex> Convert__Vec2d_List__To__Vertex_List(
            List<Vec2d> points
            )
        {
            List<Vertex> vertices = new List<Vertex>();
            foreach (var op in points)
            {
                Vertex p = new Vertex(op.x, op.y);
                vertices.Add(p);
            }
            return vertices;
        }

        public static List<double> Extract__Area__per__Cell(
            List<List<Vec2d>> region_points__per__cell_id,
            List<bool> is_active__per__cell
            )
        {
            int num_cells = region_points__per__cell_id.Count;

            List<double> area__per__cell = new List<double>(capacity:num_cells);

            for (int c = 0; c < num_cells; c++)
            {
                bool is_active = is_active__per__cell[c];
                if (is_active == false)
                {
                    area__per__cell.Add(0.0);
                }
                else
                {
                    double area = region_points__per__cell_id[c].Q__Area();
                    area__per__cell.Add(area);
                }

            }

            return area__per__cell;
        }



        /// <summary>
        /// Extracts the weighted connectivity graph from a mesh and a connectivity graph.
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public static DS__Undirected_Weighted_Graph Extract__Weighted_Connectivity_Graph(
            DS__Undirected_Graph connectivity_graph,
            List<Vec2d> points
            )
        {
            DS__Undirected_Weighted_Graph weighted_connectivity_graph =
                new DS__Undirected_Weighted_Graph();

            var connections = connectivity_graph.Q__Edges();

            foreach (var edge in connections)
            {
                int v1 = edge.v1;
                int v2 = edge.v2;

                var p1 = points[v1];
                var p2 = points[v2];

                double distance = Vec2d.Distance_Between(p1, p2);

                weighted_connectivity_graph.M__Add_Edge(v1, v2, distance);
            }

            return weighted_connectivity_graph;
        }

        public static List<Vec2d> Extract_Centroids(
            List<List<Vec2d>> perimeter_points__per__cell,
            List<bool> is_active__per__cell,
            List<Vec2d> generator_points
            )
        {
            int num_cells = generator_points.Count;

            List<Vec2d> centroids = new List<Vec2d>(capacity:num_cells);
            for (int c = 0; c < num_cells; c++)
            {
                if (is_active__per__cell[c] == false)
                {
                    centroids.Add(generator_points[c]);
                }
                else
                {
                    var perimeter_points = perimeter_points__per__cell[c];

                    Vec2d center = new Vec2d(0.0, 0.0);
                    foreach (var pt in perimeter_points)
                    {
                        center += pt;
                    }
                    center /= (double)perimeter_points.Count;

                    List<Vec2d> sub_centroids = new List<Vec2d>();
                    List<double> sub_areas = new List<double>();
                    int num_pts = perimeter_points.Count;
                    for (int i = 0; i < num_pts; i++)
                    {
                        int i2 = (i + 1) % num_pts;
                        Vec2d p1 = perimeter_points[i];
                        Vec2d p2 = perimeter_points[i2];
                        Vec2d p3 = center;

                        Vec2d triangle_centroid =
                            Geometric_Utilities
                            .Q__Triangle_Centroid(p1, p2, p3);
                        sub_centroids.Add(triangle_centroid);

                        double triangle_area =
                            Geometric_Utilities
                            .Q__Triangle_Area(p1, p2, p3);
                        sub_areas.Add(triangle_area);
                    }

                    double area_sum = sub_areas.Sum();

                    Vec2d centroid = new Vec2d(0, 0);
                    int num_sub_centroids = sub_centroids.Count;
                    for (int sc = 0; sc < num_sub_centroids; sc++)
                    {
                        centroid += sub_centroids[sc] * sub_areas[sc];
                    }

                    centroid /= area_sum;

                    centroids.Add(centroid);
                }
            }

            return centroids;
        }

        public static void Extract__Voronoi_Lines(
            List<List<Vec2d>> perimeter_points__per__cell,
            List<bool> is_active__per__cell,
            out List<List<Line_Segment>> perimeter_lines__per__cell,
            out List<Line_Segment> voronoi_lines
            )
        {
            int num_cells = perimeter_points__per__cell.Count;

            perimeter_lines__per__cell =
                new List<List<Line_Segment>>(capacity: num_cells);

            for (int c = 0; c < num_cells; c++)
            {
                bool is_active = is_active__per__cell[c];
                if (is_active)
                {
                    List<Vec2d> points = perimeter_points__per__cell[c];

                    List<Line_Segment> lines = new List<Line_Segment>();
                    for (int i = 0; i < points.Count; i++)
                    {
                        Vec2d p0 = points[i];
                        Vec2d p1;

                        if (i < points.Count - 1) p1 = points[i + 1];
                        else p1 = points[0];

                        Line_Segment line = new Line_Segment(p0, p1);
                        lines.Add(line);
                    }

                    perimeter_lines__per__cell.Add(lines);
                }
                else
                {
                    perimeter_lines__per__cell.Add(new List<Line_Segment>());
                }

            }

            voronoi_lines = new List<Line_Segment>();
            foreach (var perimeter_lines in perimeter_lines__per__cell)
            {
                foreach (var perimeter_line in perimeter_lines)
                {
                    if (voronoi_lines.Contains(perimeter_line) == false)
                    {
                        voronoi_lines.Add(perimeter_line);
                    }
                }
            }
        }


    }
}
