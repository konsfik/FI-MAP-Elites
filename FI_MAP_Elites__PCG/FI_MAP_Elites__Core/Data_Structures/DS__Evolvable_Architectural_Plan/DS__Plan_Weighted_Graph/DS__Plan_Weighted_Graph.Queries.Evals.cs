using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Weighted_Graph;
using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Plan_Weighted_Graph
    {
        public double Q__Avg_Dist_Entrances_Connection_Doors()
        {
            var entrance_doors_verts = Q__Entrance_Doors__Verts();
            var connection_doors_verts = Q__Connection_Doors__Verts();

            double sum = 0.0;
            int cnt = 0;
            foreach (var entrance_door_vertex in entrance_doors_verts)
            {
                foreach (var connection_door_vertex in connection_doors_verts)
                {
                    graph.Q__Shortest_Path__Distance(
                        root: entrance_door_vertex,
                        destination: connection_door_vertex,
                        success: out bool success,
                        distance: out double distance
                        );
                    if (success == false)
                    {
                        continue;
                    }
                    else
                    {
                        sum += distance;
                        cnt++;
                    }
                }
            }

            if (cnt == 0)
            {
                return 0.0;
            }
            else
            {
                return sum / (double)cnt;
            }
        }

        public double Q__Avg_Dist_Connection_Doors()
        {
            var connection_doors_verts = Q__Connection_Doors__Verts();

            int num_verts = connection_doors_verts.Count;

            double sum = 0.0;
            int cnt = 0;
            for (int i1 = 0; i1 < num_verts - 1; i1++)
            {
                for (int i2 = i1 + 1; i2 < num_verts; i2++)
                {
                    int connection_door_vertex_1 = connection_doors_verts[i1];
                    int connection_door_vertex_2 = connection_doors_verts[i2];

                    graph.Q__Shortest_Path__Distance(
                        root: connection_door_vertex_1,
                        destination: connection_door_vertex_2,
                        success: out bool success,
                        distance: out double distance
                        );
                    if (success == false)
                    {
                        continue;
                    }
                    else
                    {
                        sum += distance;
                        cnt++;
                    }
                }
            }

            if (cnt == 0)
            {
                return 0.0;
            }
            else
            {
                return sum / (double)cnt;
            }
        }

        public double Q__Avg_Dist_Windows()
        {
            var windows_doors_verts = Q__Windows__Verts();

            int num_verts = windows_doors_verts.Count;

            double sum = 0.0;
            int cnt = 0;
            for (int i1 = 0; i1 < num_verts - 1; i1++)
            {
                for (int i2 = i1 + 1; i2 < num_verts; i2++)
                {
                    int connection_door_vertex_1 = windows_doors_verts[i1];
                    int connection_door_vertex_2 = windows_doors_verts[i2];

                    graph.Q__Shortest_Path__Distance(
                        root: connection_door_vertex_1,
                        destination: connection_door_vertex_2,
                        success: out bool success,
                        distance: out double distance
                        );
                    if (success == false)
                    {
                        continue;
                    }
                    else
                    {
                        sum += distance;
                        cnt++;
                    }
                }
            }

            if (cnt == 0)
            {
                return 0.0;
            }
            else
            {
                return sum / (double)cnt;
            }
        }

        public double Q__Circulation_Area(double circulation_width)
        {
            var unique_circulation_edges = Q__Circulation_Paths__Unique_Edges();
            double circulation_length = 0;
            foreach (var edge in unique_circulation_edges)
            {
                circulation_length += edge.weight;
            }

            double circulation_area = circulation_length * circulation_width;

            return circulation_area;
        }

        public double Q__Percent_Circulation_Verts()
        {
            int num_verts = graph.Q__Num_Vertices();
            var circulation_verts = Q__Circulation_verts();
            int num_circulation_verts = circulation_verts.Count;

            return (double)num_circulation_verts / (double)num_verts;
        }
    }
}
