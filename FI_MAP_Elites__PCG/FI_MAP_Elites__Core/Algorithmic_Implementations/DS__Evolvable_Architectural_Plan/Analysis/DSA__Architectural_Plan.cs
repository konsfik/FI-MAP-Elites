using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public class DSA__Architectural_Plan : Data_Structure_Analyzer<DS__Architectural_Plan>
    {
        // basic properties:
        public double rect_width;
        public double rect_height;
        public int num_generator_points;
        public int num_active_cells;

        // constraints:
        // L1 - constraints:
        CEM__L1__Voronoi_Connected<DS__Architectural_Plan> cem__l1__voronoi_connected;
        CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan> cem__l1__voronoi_min_percent_active_cells;
        // L2 - constraints:
        CEM__L2__Space_Units_Exist<DS__Architectural_Plan> cem__l2__space_units_exist;
        CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan> cem__l2__space_units_are_coherent;
        CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan> cem__l2__prescribed_connections_exist;
        CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan> cem__l2__space_units_areas_within_margin;
        // L3 - constraints:
        CEM__L3__Prescribed_Openings cem__l3__prescribed_openings;
        CEM__L3__Not_Narrow_Passages cem__l3__not_narrow_passages;

        // constraints - values:
        // L1 - constraints - values:
        public bool v__cem__l1__voronoi_connected;
        public bool v__cem__l1__voronoi_min_percent_active_cells;
        // L2 - constraints - values:
        public bool v__cem__l2__space_units_exist;
        public bool v__cem__l2__space_units_are_coherent;
        public bool v__cem__l2__prescribed_connections_exist;
        public bool v__cem__l2__space_units_areas_within_margin;
        // L3 - constraints - values:
        public bool v__cem__l3__prescribed_openings;
        public bool v__cem__l3__not_narrow_passages;

        // gradient constraints: 
        // L1 - gradient constraints:
        EM__CEM__L1__Voronoi_Connected<DS__Architectural_Plan> em__cem__l1__voronoi_connected;
        EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan> em__cem__l1__voronoi_min_percent_active_cells;
        // L2 - gradient constraints:
        EM__CEM__L2__Space_Units_Exist<DS__Architectural_Plan> em__cem__l2__space_units_exist;
        EM__CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan> em__cem__l2__space_units_are_coherent;
        EM__CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan> em__cem__l2__prescribed_connections_exist;
        EM__CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan> em__cem__l2__space_units_areas_within_margin;
        // L3 - gradient constraints:
        EM__CEM__L3__Prescribed_Openings em__cem__l3__prescribed_openings;
        EM__CEM__L3__Not_Narrow_Passages em__cem__l3__not_narrow_passages;

        // gradient constraints - values:
        // L1 - gradient constraints - values:
        public double v__em__cem__l1__voronoi_connected;
        public double v__em__cem__l1__voronoi_min_percent_active_cells;
        // L2 - gradient constraints - values:
        public double v__em__cem__l2__space_units_exist;
        public double v__em__cem__l2__space_units_are_coherent;
        public double v__em__cem__l2__prescribed_connections_exist;
        public double v__em__cem__l2__space_units_areas_within_margin;
        // L3 - gradient constraints - values:
        public double v__em__cem__l3__prescribed_openings;
        public double v__em__cem__l3__not_narrow_passages;

        // BCs: 
        // L2 BCs:
        EM__L2__Compactness<DS__Architectural_Plan> em__l2__compactness;
        EM__L2__Compactness_Per_Space_Unit<DS__Architectural_Plan> em__l2__compactness_per_space_unit;
        EM__L2__Used_Cells_Compactness<DS__Architectural_Plan> em__l2__used_cells_compactness_average;
        EM__L2__Lines_Orthogonality<DS__Architectural_Plan> em__l2__lines_orthogonality;
        EM__L2__Lines_Orthogonality_Weighted<DS__Architectural_Plan> em__l2__lines_orthogonality_weighted;
        EM__L2__Angles_Orthogonality<DS__Architectural_Plan> em__l2__angles_orthogonality;
        EM__L2__Angles_Non_Acute<DS__Architectural_Plan> em__l2__angles_non_acute;
        EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan> em__l2__space_units_area_precision;
        // L3 BCs:
        EM__L3__Avg_Dist_Entrances_Connection_Doors_Normalized em__l3__avg_dist_entrances_connection_doors_normalized;
        EM__L3__Avg_Dist_Connection_Doors_Normalized em__l3__avg_dist_connection_doors_normalized;
        EM__L3__Avg_Dist_Windows_Normalized em__l3__avg_dist_windows_normalized;
        EM__L3__Percent_Circulation_Area em__l3__percent_circulation_area;

        // BCs - values:
        // L2 BCs - values:
        public double v__em__l2__compactness;
        public double v__em__l2__compactnesss_per_space_unit;
        public double v__em__l2__used_cells_compactness_average;
        public double v__em__l2__lines_orthogonality;
        public double v__em__l2__lines_orthogonality_weighted;
        public double v__em__l2__angles_orthogonality;
        public double v__em__l2__angles_non_acute;
        public double v__em__l2__space_units_area_precision;
        // L3 BCs - values:
        public double v__em__l3__avg_dist_entrances_connection_doors_normalized;
        public double v__em__l3__avg_dist_connection_doors_normalized;
        public double v__em__l3__avg_dist_windows_normalized;
        public double v__em__l3__percent_circulation_area;

        public DSA__Architectural_Plan(
            // constraints
            // constraints - L1
            CEM__L1__Voronoi_Connected<DS__Architectural_Plan> cem__l1__voronoi_connected,
            CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan> cem__l1__voronoi_min_percent_active_cells,
            // constraints - L2
            CEM__L2__Space_Units_Exist<DS__Architectural_Plan> cem__l2__space_units_exist,
            CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan> cem__l2__space_units_are_coherent,
            CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan> cem__l2__prescribed_connections_exist,
            CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan> cem__l2__space_units_areas_within_margin,
            // constraints - L3
            CEM__L3__Prescribed_Openings cem__l3__prescribed_openings,
            CEM__L3__Not_Narrow_Passages cem__l3__not_narrow_passages,

            // gradient constraints
            // gradient constraints - L1
            EM__CEM__L1__Voronoi_Connected<DS__Architectural_Plan> em__cem__l1__voronoi_connected,
            EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan> em__cem__l1__voronoi_min_percent_active_cells,
            // gradient constraints - L2
            EM__CEM__L2__Space_Units_Exist<DS__Architectural_Plan> em__cem__l2__space_units_exist,
            EM__CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan> em__cem__l2__space_units_are_coherent,
            EM__CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan> em__cem__l2__prescribed_connections_exist,
            EM__CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan> em__cem__l2__space_units_areas_within_margin,
            // gradient constraints - L3
            EM__CEM__L3__Prescribed_Openings em__cem__l3__prescribed_openings,
            EM__CEM__L3__Not_Narrow_Passages em__cem__l3__not_narrow_passages,

            // BCs
            // BCs - L2:
            EM__L2__Compactness<DS__Architectural_Plan> em__l2__compactness,
            EM__L2__Compactness_Per_Space_Unit<DS__Architectural_Plan> em__l2__compactness_per_space_unit,
            EM__L2__Used_Cells_Compactness<DS__Architectural_Plan> em__l2__used_cells_compactness_average,
            EM__L2__Lines_Orthogonality<DS__Architectural_Plan> em__l2__lines_orthogonality,
            EM__L2__Lines_Orthogonality_Weighted<DS__Architectural_Plan> em__l2__lines_orthogonality_weighted,
            EM__L2__Angles_Orthogonality<DS__Architectural_Plan> em__l2__angles_orthogonality,
            EM__L2__Angles_Non_Acute<DS__Architectural_Plan> em__l2__angles_non_acute,
            EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan> em__l2__space_units_area_precision,
            // BCs - L3:
            EM__L3__Avg_Dist_Entrances_Connection_Doors_Normalized em__l3__avg_dist_entrances_connection_doors_normalized,
            EM__L3__Avg_Dist_Connection_Doors_Normalized em__l3__avg_dist_connection_doors_normalized,
            EM__L3__Avg_Dist_Windows_Normalized em__l3__avg_dist_windows_normalized,
            EM__L3__Percent_Circulation_Area em__l3__percent_circulation_area
            )
        {
            // constraints
            // constraints - L1
            this.cem__l1__voronoi_connected =
                (CEM__L1__Voronoi_Connected<DS__Architectural_Plan>)
                cem__l1__voronoi_connected.Q__Deep_Copy();
            this.cem__l1__voronoi_min_percent_active_cells =
                (CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>)
                cem__l1__voronoi_min_percent_active_cells.Q__Deep_Copy();
            // constraints - L2
            this.cem__l2__space_units_exist =
                (CEM__L2__Space_Units_Exist<DS__Architectural_Plan>)
                cem__l2__space_units_exist.Q__Deep_Copy();
            this.cem__l2__space_units_are_coherent
                = (CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>)
                cem__l2__space_units_are_coherent.Q__Deep_Copy();
            this.cem__l2__prescribed_connections_exist
                = (CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>)
                cem__l2__prescribed_connections_exist.Q__Deep_Copy();
            this.cem__l2__space_units_areas_within_margin
                = (CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>)
                cem__l2__space_units_areas_within_margin.Q__Deep_Copy();
            // constraints - L3
            this.cem__l3__prescribed_openings =
                (CEM__L3__Prescribed_Openings)
                cem__l3__prescribed_openings.Q__Deep_Copy();
            this.cem__l3__not_narrow_passages =
                (CEM__L3__Not_Narrow_Passages)
                cem__l3__not_narrow_passages.Q__Deep_Copy();

            // gradient_constraints
            // gradient_constraints - L1
            this.em__cem__l1__voronoi_connected =
                (EM__CEM__L1__Voronoi_Connected<DS__Architectural_Plan>)
                em__cem__l1__voronoi_connected.Q__Deep_Copy();
            this.em__cem__l1__voronoi_min_percent_active_cells =
                (EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>)
                em__cem__l1__voronoi_min_percent_active_cells.Q__Deep_Copy();
            // gradient_constraints - L2
            this.em__cem__l2__space_units_exist =
                (EM__CEM__L2__Space_Units_Exist<DS__Architectural_Plan>)
                em__cem__l2__space_units_exist.Q__Deep_Copy();
            this.em__cem__l2__space_units_are_coherent
                = (EM__CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>)
                em__cem__l2__space_units_are_coherent.Q__Deep_Copy();
            this.em__cem__l2__prescribed_connections_exist
                = (EM__CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>)
                em__cem__l2__prescribed_connections_exist.Q__Deep_Copy();
            this.em__cem__l2__space_units_areas_within_margin
                = (EM__CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>)
                em__cem__l2__space_units_areas_within_margin.Q__Deep_Copy();
            // gradient_constraints - L3
            this.em__cem__l3__prescribed_openings =
                (EM__CEM__L3__Prescribed_Openings)
                em__cem__l3__prescribed_openings.Q__Deep_Copy();
            this.em__cem__l3__not_narrow_passages =
                (EM__CEM__L3__Not_Narrow_Passages)
                em__cem__l3__not_narrow_passages.Q__Deep_Copy();

            // BCs:
            // BCs - L2:
            this.em__l2__compactness = 
                (EM__L2__Compactness<DS__Architectural_Plan>)
                em__l2__compactness.Q__Deep_Copy();
            this.em__l2__compactness_per_space_unit =
                (EM__L2__Compactness_Per_Space_Unit<DS__Architectural_Plan>)
                em__l2__compactness_per_space_unit.Q__Deep_Copy();
            this.em__l2__used_cells_compactness_average =
                (EM__L2__Used_Cells_Compactness<DS__Architectural_Plan>)
                em__l2__used_cells_compactness_average.Q__Deep_Copy();
            this.em__l2__lines_orthogonality =
                (EM__L2__Lines_Orthogonality<DS__Architectural_Plan>)
                em__l2__lines_orthogonality.Q__Deep_Copy();
            this.em__l2__lines_orthogonality_weighted =
                (EM__L2__Lines_Orthogonality_Weighted<DS__Architectural_Plan>)
                em__l2__lines_orthogonality_weighted.Q__Deep_Copy();
            this.em__l2__angles_orthogonality =
                (EM__L2__Angles_Orthogonality<DS__Architectural_Plan>)
                em__l2__angles_orthogonality.Q__Deep_Copy();
            this.em__l2__angles_non_acute =
                (EM__L2__Angles_Non_Acute<DS__Architectural_Plan>)
                em__l2__angles_non_acute.Q__Deep_Copy();
            this.em__l2__space_units_area_precision =
                (EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan>)
                em__l2__space_units_area_precision.Q__Deep_Copy();
            // BCs - L3:
            this.em__l3__avg_dist_entrances_connection_doors_normalized =
                (EM__L3__Avg_Dist_Entrances_Connection_Doors_Normalized)
                em__l3__avg_dist_entrances_connection_doors_normalized.Q__Deep_Copy();
            this.em__l3__avg_dist_connection_doors_normalized =
                (EM__L3__Avg_Dist_Connection_Doors_Normalized)
                em__l3__avg_dist_connection_doors_normalized.Q__Deep_Copy();
            this.em__l3__avg_dist_windows_normalized =
                (EM__L3__Avg_Dist_Windows_Normalized)
                em__l3__avg_dist_windows_normalized.Q__Deep_Copy();
            this.em__l3__percent_circulation_area =
                (EM__L3__Percent_Circulation_Area)
                em__l3__percent_circulation_area.Q__Deep_Copy();
        }

        public DSA__Architectural_Plan(DS__Architectural_Plan individual)
        {
            Update(individual);
        }

        private DSA__Architectural_Plan(DSA__Architectural_Plan analyzer_to_copy)
        {
            // constraints
            // constraints - L1
            this.cem__l1__voronoi_connected =
                (CEM__L1__Voronoi_Connected<DS__Architectural_Plan>)
                analyzer_to_copy.cem__l1__voronoi_connected.Q__Deep_Copy();
            this.cem__l1__voronoi_min_percent_active_cells =
                (CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>)
                analyzer_to_copy.cem__l1__voronoi_min_percent_active_cells.Q__Deep_Copy();
            // constraints - L2
            this.cem__l2__space_units_exist =
                (CEM__L2__Space_Units_Exist<DS__Architectural_Plan>)
                analyzer_to_copy.cem__l2__space_units_exist.Q__Deep_Copy();
            this.cem__l2__space_units_are_coherent
                = (CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>)
                analyzer_to_copy.cem__l2__space_units_are_coherent.Q__Deep_Copy();
            this.cem__l2__prescribed_connections_exist
                = (CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>)
                analyzer_to_copy.cem__l2__prescribed_connections_exist.Q__Deep_Copy();
            this.cem__l2__space_units_areas_within_margin
                = (CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>)
                analyzer_to_copy.cem__l2__space_units_areas_within_margin.Q__Deep_Copy();
            // constraints - L3
            this.cem__l3__prescribed_openings =
                (CEM__L3__Prescribed_Openings)
                analyzer_to_copy.cem__l3__prescribed_openings.Q__Deep_Copy();
            this.cem__l3__not_narrow_passages =
                (CEM__L3__Not_Narrow_Passages)
                analyzer_to_copy.cem__l3__not_narrow_passages.Q__Deep_Copy();

            // gradient_constraints
            // gradient_constraints - L1
            this.em__cem__l1__voronoi_connected =
                (EM__CEM__L1__Voronoi_Connected<DS__Architectural_Plan>)
                analyzer_to_copy.em__cem__l1__voronoi_connected.Q__Deep_Copy();
            this.em__cem__l1__voronoi_min_percent_active_cells =
                (EM__CEM__L1__Voronoi_Min_Percent_Active_Cells<DS__Architectural_Plan>)
                analyzer_to_copy.em__cem__l1__voronoi_min_percent_active_cells.Q__Deep_Copy();
            // gradient_constraints - L2
            this.em__cem__l2__space_units_exist =
                (EM__CEM__L2__Space_Units_Exist<DS__Architectural_Plan>)
                analyzer_to_copy.em__cem__l2__space_units_exist.Q__Deep_Copy();
            this.em__cem__l2__space_units_are_coherent
                = (EM__CEM__L2__Space_Units_Are_Coherent<DS__Architectural_Plan>)
                analyzer_to_copy.em__cem__l2__space_units_are_coherent.Q__Deep_Copy();
            this.em__cem__l2__prescribed_connections_exist
                = (EM__CEM__L2__Prescribed_Connections_Exist<DS__Architectural_Plan>)
                analyzer_to_copy.em__cem__l2__prescribed_connections_exist.Q__Deep_Copy();
            this.em__cem__l2__space_units_areas_within_margin
                = (EM__CEM__L2__Space_Units_Areas_Within_Margin<DS__Architectural_Plan>)
                analyzer_to_copy.em__cem__l2__space_units_areas_within_margin.Q__Deep_Copy();
            // gradient_constraints - L3
            this.em__cem__l3__prescribed_openings =
                (EM__CEM__L3__Prescribed_Openings)
                analyzer_to_copy.em__cem__l3__prescribed_openings.Q__Deep_Copy();
            this.em__cem__l3__not_narrow_passages =
                (EM__CEM__L3__Not_Narrow_Passages)
                analyzer_to_copy.em__cem__l3__not_narrow_passages.Q__Deep_Copy();

            // BCs:
            // BCs - L2:
            this.em__l2__compactness =
                (EM__L2__Compactness<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__compactness.Q__Deep_Copy();
            this.em__l2__compactness_per_space_unit =
                (EM__L2__Compactness_Per_Space_Unit<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__compactness_per_space_unit.Q__Deep_Copy();
            this.em__l2__used_cells_compactness_average =
                (EM__L2__Used_Cells_Compactness<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__used_cells_compactness_average.Q__Deep_Copy();
            this.em__l2__lines_orthogonality =
                (EM__L2__Lines_Orthogonality<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__lines_orthogonality.Q__Deep_Copy();
            this.em__l2__lines_orthogonality_weighted =
                (EM__L2__Lines_Orthogonality_Weighted<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__lines_orthogonality_weighted.Q__Deep_Copy();
            this.em__l2__angles_orthogonality =
                (EM__L2__Angles_Orthogonality<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__angles_orthogonality.Q__Deep_Copy();
            this.em__l2__angles_non_acute =
                (EM__L2__Angles_Non_Acute<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__angles_non_acute.Q__Deep_Copy();
            this.em__l2__space_units_area_precision =
                (EM__L2__Space_Units_Area_Precision<DS__Architectural_Plan>)
                analyzer_to_copy.em__l2__space_units_area_precision.Q__Deep_Copy();
            // BCs - L3:
            this.em__l3__avg_dist_entrances_connection_doors_normalized =
                (EM__L3__Avg_Dist_Entrances_Connection_Doors_Normalized)
                analyzer_to_copy.em__l3__avg_dist_entrances_connection_doors_normalized.Q__Deep_Copy();
            this.em__l3__avg_dist_connection_doors_normalized =
                (EM__L3__Avg_Dist_Connection_Doors_Normalized)
                analyzer_to_copy.em__l3__avg_dist_connection_doors_normalized.Q__Deep_Copy();
            this.em__l3__avg_dist_windows_normalized =
                (EM__L3__Avg_Dist_Windows_Normalized)
                analyzer_to_copy.em__l3__avg_dist_windows_normalized.Q__Deep_Copy();
            this.em__l3__percent_circulation_area =
                (EM__L3__Percent_Circulation_Area)
                analyzer_to_copy.em__l3__percent_circulation_area.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new DSA__Architectural_Plan(this);
        }

        public override void Update(DS__Architectural_Plan individual)
        {
            // basic properties:
            rect_width = individual.voronoi_tessellation.bounding_rectangle.width;
            rect_height = individual.voronoi_tessellation.bounding_rectangle.height;
            num_generator_points = individual.voronoi_tessellation.Q__Num_Generator_Points();
            num_active_cells = individual.voronoi_tessellation.Q__Num_Active_Cells();

            // constraints - values:
            // L1 - constraints - values:
            v__cem__l1__voronoi_connected = 
                cem__l1__voronoi_connected.Q__Satisfied(individual);
            v__cem__l1__voronoi_min_percent_active_cells = 
                cem__l1__voronoi_min_percent_active_cells.Q__Satisfied(individual);
            // L2 - constraints - values:
            v__cem__l2__space_units_exist = 
                cem__l2__space_units_exist.Q__Satisfied(individual);
            v__cem__l2__space_units_are_coherent = 
                cem__l2__space_units_are_coherent.Q__Satisfied(individual);
            v__cem__l2__prescribed_connections_exist = 
                cem__l2__prescribed_connections_exist.Q__Satisfied(individual);
            v__cem__l2__space_units_areas_within_margin = 
                cem__l2__space_units_areas_within_margin.Q__Satisfied(individual);
            // L3 - constraints - values:
            v__cem__l3__prescribed_openings = 
                cem__l3__prescribed_openings.Q__Satisfied(individual);
            v__cem__l3__not_narrow_passages = 
                cem__l3__not_narrow_passages.Q__Satisfied(individual);

            // gradient constraints - values:
            // L1 - gradient constraints - values:
            v__em__cem__l1__voronoi_connected = 
                em__cem__l1__voronoi_connected.Evaluate_Individual(individual);
            v__em__cem__l1__voronoi_min_percent_active_cells = 
                em__cem__l1__voronoi_min_percent_active_cells.Evaluate_Individual(individual);
            // L2 - gradient constraints - values:
            v__em__cem__l2__space_units_exist = 
                em__cem__l2__space_units_exist.Evaluate_Individual(individual);
            v__em__cem__l2__space_units_are_coherent = 
                em__cem__l2__space_units_are_coherent.Evaluate_Individual(individual);
            v__em__cem__l2__prescribed_connections_exist = 
                em__cem__l2__prescribed_connections_exist.Evaluate_Individual(individual);
            v__em__cem__l2__space_units_areas_within_margin = 
                em__cem__l2__space_units_areas_within_margin.Evaluate_Individual(individual);
            // L3 - gradient constraints - values:
            v__em__cem__l3__prescribed_openings = 
                em__cem__l3__prescribed_openings.Evaluate_Individual(individual);
            v__em__cem__l3__not_narrow_passages = 
                em__cem__l3__not_narrow_passages.Evaluate_Individual(individual);

            // BCs - values
            // L2 - BCs:
            v__em__l2__compactness =
                em__l2__compactness.Evaluate_Individual(individual);
            v__em__l2__compactnesss_per_space_unit =
                em__l2__compactness_per_space_unit.Evaluate_Individual(individual);
            v__em__l2__used_cells_compactness_average =
                em__l2__used_cells_compactness_average.Evaluate_Individual(individual);
            v__em__l2__lines_orthogonality =
                em__l2__lines_orthogonality.Evaluate_Individual(individual);
            v__em__l2__lines_orthogonality_weighted =
                em__l2__lines_orthogonality_weighted.Evaluate_Individual(individual);
            v__em__l2__angles_orthogonality =
                em__l2__angles_orthogonality.Evaluate_Individual(individual);
            v__em__l2__angles_non_acute =
                em__l2__angles_non_acute.Evaluate_Individual(individual);
            v__em__l2__space_units_area_precision =
                em__l2__space_units_area_precision.Evaluate_Individual(individual);
            // L3 - BCs:
            v__em__l3__avg_dist_entrances_connection_doors_normalized =
                em__l3__avg_dist_entrances_connection_doors_normalized.Evaluate_Individual(individual);
            v__em__l3__avg_dist_connection_doors_normalized =
                em__l3__avg_dist_connection_doors_normalized.Evaluate_Individual(individual);
            v__em__l3__avg_dist_windows_normalized =
                em__l3__avg_dist_windows_normalized.Evaluate_Individual(individual);
            v__em__l3__percent_circulation_area =
                em__l3__percent_circulation_area.Evaluate_Individual(individual);
    }

        public override string Q__CSV__Data_Header(string delimiter, string end_character)
        {
            var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            string output_string = "";
            int num_fields = fields.Length;

            for (int i = 0; i < num_fields; i++) {
                output_string += fields[i].Name;
                if (i < num_fields - 1) {
                    output_string += delimiter;
                }
            }

            output_string += end_character;

            return output_string;
        }

        public override string Q__CSV__Data_Row(string delimiter, string end_character)
        {
            var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            string output_string = "";
            int num_fields = fields.Length;

            for (int i = 0; i < num_fields; i++)
            {
                output_string += fields[i].GetValue(this).ToString();
                if (i < num_fields - 1)
                {
                    output_string += delimiter;
                }
            }

            output_string += end_character;

            return output_string;
        }

        


    }
}
