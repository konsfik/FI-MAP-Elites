using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public partial class DS__Architectural_Plan : DS__Evolvable_Geometry
    {
        public int Q__Num__Openings__On_Edge(Undirected_Edge edge)
        {
            int cnt = 0;

            foreach (var connection_door in connection_doors)
            {
                if (connection_door.cells_connection == edge)
                {
                    cnt++;
                }
            }

            foreach (var entrance_door in entrance_doors)
            {
                if (entrance_door.cells_connection == edge)
                {
                    cnt++;
                }
            }

            foreach (var window in windows)
            {
                if (window.cells_connection == edge)
                {
                    cnt++;
                }
            }

            return cnt;
        }

        /// <summary>
        /// Returns the number of prescribed openings.
        /// This includes connection doors, entrance doors and windows.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Prescribed_Openings()
        {
            int num_prescribed_openings =
                Q__Num__Prescribed_Connection_Doors()
                + Q__Num__Prescribed_Entrance_Doors()
                + Q__Num__Prescribed_Windows();
            return num_prescribed_openings;
        }

        public int Q__Num__Satisfied_Prescribed_Openings()
        {
            int connection_doors = Q__Num__Satisfied_Prescribed_Connection_Doors();
            int entrance_doors = Q__Num__Satisfied_Prescribed_Entrance_Doors();
            int windows = Q__Num__Satisfied_Prescribed_Windows();
            return
                connection_doors
                + entrance_doors
                + windows;
        }

        /// <summary>
        /// Returns the number of prescribed interior doors.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Prescribed_Connection_Doors()
        {
            return
                base
                .prescription
                .graph
                .Q__Num_Edges();
        }

        /// <summary>
        /// Returns the number of prescribed interior doors that are satisfied.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Satisfied_Prescribed_Connection_Doors()
        {
            var prescribed_space_unit_connections = base.prescription.graph.Q__Edges();
            int num = 0;
            foreach (var connection in prescribed_space_unit_connections)
            {
                bool connection_satisfied =
                    connection_doors.Any(
                        x =>
                        Q__Is_Connection_Door__Properly_Placed(x)
                        &&
                        connection == x.space_units_connection
                        );
                if (connection_satisfied)
                    num++;
            }
            return num;
        }

        /// <summary>
        /// Returns the number of prescribed entrance doors.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Prescribed_Entrance_Doors()
        {
            int sum = 0;

            foreach (var kvp in base.prescription.num_entrance_doors__per__space_unit)
                sum += kvp.Value;

            return sum;
        }

        /// <summary>
        /// Returns the number of prescribed entrance doors that are satisfied.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Satisfied_Prescribed_Entrance_Doors()
        {
            int num = 0;
            foreach (var kvp in base.prescription.num_entrance_doors__per__space_unit)
            {
                int space_unit = kvp.Key;
                int num_entrance_doors = kvp.Value;

                if (base.Q__Space_Unit_Exists(space_unit) == false)
                    continue;

                int cnt = 0;
                foreach (var entrance_door in entrance_doors)
                {
                    if (
                        entrance_door.Q__Space_Unit() == space_unit
                        &&
                        Q__Is_Entrance_Door__Properly_Placed(entrance_door)
                        )
                    {
                        cnt++;
                    }
                }

                if (cnt > num_entrance_doors)
                    cnt = num_entrance_doors;

                num += cnt;
            }

            return num;
        }

        /// <summary>
        /// Returns the number of prescribed windows.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Prescribed_Windows()
        {
            int sum = 0;

            foreach (var kvp in base.prescription.num_windows__per__space_unit)
                sum += kvp.Value;

            return sum;
        }

        /// <summary>
        /// Returns the number of prescribed windows that are satisfied.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Satisfied_Prescribed_Windows()
        {
            int num = 0;
            foreach (var kvp in base.prescription.num_windows__per__space_unit)
            {
                int space_unit = kvp.Key;
                int num_windows = kvp.Value;

                if (base.Q__Space_Unit_Exists(space_unit) == false)
                    continue;

                int cnt = 0;
                foreach (var window in windows)
                {
                    if (
                        window.Q__Space_Unit() == space_unit
                        &&
                        Q__Is_Window__Properly_Placed(window)
                        )
                    {
                        cnt++;
                    }
                }

                if (cnt > num_windows)
                    cnt = num_windows;

                num += cnt;
            }

            return num;
        }

        /// <summary>
        /// Returns the number of existing openings.
        /// This includes connection doors, entrance doors and windows.
        /// 
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Existing_Openings()
        {
            int num_existing_openings =
                Q__Num__Existing_Connection_Doors()
                + Q__Num__Existing_Entrance_Doors()
                + Q__Num__Existing_Windows();
            return num_existing_openings;
        }

        /// <summary>
        /// Returns the number of existing interior doors.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Existing_Connection_Doors()
        {
            return connection_doors.Count;
        }

        /// <summary>
        /// Returns the number of existing entrance doors.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Existing_Entrance_Doors()
        {
            return entrance_doors.Count;
        }

        /// <summary>
        /// Returns the number of existing windows.
        /// </summary>
        /// <returns></returns>
        public int Q__Num__Existing_Windows()
        {
            return windows.Count;
        }


        public List<EG__Connection_Door> Q__Improper_Connection_Doors()
        {
            List<EG__Connection_Door> improper_interior_doors = new List<EG__Connection_Door>();
            foreach (var interior_door in connection_doors)
            {
                bool is_proper = Q__Is_Connection_Door__Proper(interior_door);
                if (is_proper == false)
                {
                    improper_interior_doors.Add(interior_door);
                }
            }
            return improper_interior_doors;
        }

        public List<EG__Entrance_Door> Q__Improper_Entrance_Doors()
        {
            List<EG__Entrance_Door> improper_entrance_doors = new List<EG__Entrance_Door>();
            foreach (var entrance_door in entrance_doors)
            {
                if (Q__Is_Entrance_Door__Proper(entrance_door) == false)
                {
                    improper_entrance_doors.Add(entrance_door);
                }
            }
            return improper_entrance_doors;
        }

        public List<EG__Window> Q__Improper_Windows()
        {
            List<EG__Window> improper_windows = new List<EG__Window>();
            foreach (var window in windows)
            {
                if (Q__Is_Window__Proper(window) == false)
                {
                    improper_windows.Add(window);
                }
            }
            return improper_windows;
        }

        /// <summary>
        /// Checks whether an interior door is proper. I.e: 
        /// - 1. Whether its rooms connection is prescribed
        /// - 2. Whether its cells connection exists in the connectivity graph
        /// - 3. Whether its wall is long enough
        /// - 4. Whether the cells correspond to the rooms
        /// </summary>
        /// <param name="interior_door"></param>
        /// <returns></returns>
        public bool Q__Is_Connection_Door__Proper(
            EG__Connection_Door interior_door
            )
        {
            bool door_prescribed = Q__Is_Connection_Door_Prescribed(
                interior_door
                );

            if (door_prescribed == false)
            {
                return false;
            }

            bool door_properly_placed = Q__Is_Connection_Door__Properly_Placed(interior_door);

            if (door_properly_placed == false)
            {
                return false;
            }

            return true;
        }

        public bool Q__Is_Connection_Door_Prescribed(
            EG__Connection_Door interior_door
            )
        {
            var space_units_connection =
                interior_door
                .Q__Space_Units_Connection();

            bool space_units_connection_prescribed =
                prescription
                .graph
                .Q__Contains_Edge(space_units_connection);

            return space_units_connection_prescribed;
        }

        /// <summary>
        /// Checks whether an interior door is proper. I.e: 
        /// - 1. Whether its cells connection exists in the connectivity graph
        /// - 2. Whether its wall is long enough
        /// - 3. Whether the cells correspond to the rooms
        /// - 4. Whether it is in a unique location (i.e. no other openings coexist)
        /// This method differs from the previous one (Q__Is_Interior_Door__Proper) 
        /// in that it does not check to see whether the door is actually prescribed. 
        /// It only checks whether it is coherently placed.
        /// </summary>
        /// <param name="interior_door"></param>
        /// <param name="minimum_wall_length"></param>
        /// <returns></returns>
        public bool Q__Is_Connection_Door__Properly_Placed(
            EG__Connection_Door interior_door
            )
        {
            bool cells_connection_exists =
                Q__Is_Connection_Door__Properly_Placed__Check_1__Cells_Connection(
                    interior_door
                    );

            if (cells_connection_exists == false)
            {
                return false;
            }

            bool shared_line_long_enough =
                Q__Is_Interior_Door__Properly_Placed__Check_2__Wall_Length(
                    interior_door
                    );

            if (shared_line_long_enough == false)
            {
                return false;
            }

            bool cells_correspond_to_rooms =
                Q__Is_Interior_Door__Properly_Placed__Check_3__Cells_Space_Units(
                    interior_door
                    );

            if (cells_correspond_to_rooms == false)
            {
                return false;
            }

            bool unique_location =
                Q__Is_Interior_Door__Properly_Placed__Check_4__Unique_Location(interior_door);

            if (unique_location == false)
            {
                return false;
            }

            return true;
        }

        private bool Q__Is_Connection_Door__Properly_Placed__Check_1__Cells_Connection(
            EG__Connection_Door interior_door
            )
        {
            var cells_connection =
                interior_door
                .Q__Cells_Connection();

            bool cells_connection_exists =
                voronoi_tessellation
                .connectivity_graph
                .Q__Contains_Edge(cells_connection);

            return cells_connection_exists;
        }

        private bool Q__Is_Interior_Door__Properly_Placed__Check_2__Wall_Length(
            EG__Connection_Door interior_door
            )
        {
            var cells_connection =
                interior_door
                .Q__Cells_Connection();

            bool found_shared_line;
            Line_Segment shared_line;
            voronoi_tessellation.Q__Shared_Line__Between_Neighbor_Cells(
                cells_connection.v1,
                cells_connection.v2,
                out found_shared_line,
                out shared_line
                );

            if (found_shared_line == false)
            {
                return false;
            }

            double shared_line_length = shared_line.Q__Magnitude();
            if (shared_line_length >= prescription.openings__minimum_wall_length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Q__Is_Interior_Door__Properly_Placed__Check_3__Cells_Space_Units(
            EG__Connection_Door interior_door
            )
        {
            // test whether the cells correspond to the space units
            int cell_1 = interior_door.Q__Cell_1();
            int cell_2 = interior_door.Q__Cell_2();

            int space_unit_1 = interior_door.Q__Space_Unit_1();
            int space_unit_2 = interior_door.Q__Space_Unit_2();

            bool cells_correspond_to_rooms =
                ((space_unit__per__cell[cell_1] == space_unit_1)
                &&
                (space_unit__per__cell[cell_2] == space_unit_2))
                ||
                ((space_unit__per__cell[cell_2] == space_unit_1)
                &&
                (space_unit__per__cell[cell_1] == space_unit_2))
                ;

            return cells_correspond_to_rooms;
        }

        private bool Q__Is_Interior_Door__Properly_Placed__Check_4__Unique_Location(
            EG__Connection_Door interior_door
            )
        {
            int num = Q__Num__Openings__On_Edge(interior_door.cells_connection);
            return num == 1;
        }

        /// <summary>
        /// Returns whether an entrance door is proper. I.e. if:
        /// 1. The entrance door is prescribed
        /// 2. The entrance door is properly placed.
        /// </summary>
        /// <param name="entrance_door"></param>
        /// <returns></returns>
        public bool Q__Is_Entrance_Door__Proper(EG__Entrance_Door entrance_door)
        {
            bool prescribed = Q__Is_Entrance_Door__Prescribed(
                entrance_door
                );

            if (prescribed == false)
            {
                return false;
            }

            bool properly_placed = Q__Is_Entrance_Door__Properly_Placed(entrance_door);

            if (properly_placed == false)
            {
                return false;
            }

            return true;
        }

        private bool Q__Is_Entrance_Door__Prescribed(
            EG__Entrance_Door entrance_door
            )
        {
            var space_unit = entrance_door.Q__Space_Unit();

            bool prescribed =
                prescription
                .num_entrance_doors__per__space_unit[space_unit] > 0;

            return prescribed;
        }

        /// <summary>
        /// Checks whether an interior door is proper. I.e: 
        /// - 1. Whether its cells connection exists in the connectivity graph
        /// - 2. Whether its wall is long enough
        /// - 3. Whether the cells correspond to the proper rooms
        /// </summary>
        /// <param name="entrance_door"></param>
        /// <returns></returns>
        public bool Q__Is_Entrance_Door__Properly_Placed(
            EG__Entrance_Door entrance_door
            )
        {
            bool cells_connection_exists = Q__Is_Entrance_Door__Properly_Placed__Check_1__Cells_Connection(
                entrance_door
                );

            if (cells_connection_exists == false)
            {
                return false;
            }

            bool wall_long_enough = Q__Is_Entrance_Door__Properly_Placed__Check_2__Wall_Length(
                entrance_door
                );

            if (wall_long_enough == false)
            {
                return false;
            }

            bool cells_correspond_to_rooms = Q__Is_Entrance_Door__Properly_Placed__Check_3__Cells_Space_Units(
                entrance_door
                );

            if (cells_correspond_to_rooms == false)
            {
                return false;
            }

            bool unique_location = Q__Is_Entrance_Door__Properly_Placed__Check_4__Unique_Location(
                entrance_door
                );

            if (unique_location == false)
            {
                return false;
            }

            return true;
        }


        private bool Q__Is_Entrance_Door__Properly_Placed__Check_1__Cells_Connection(
            EG__Entrance_Door entrance_door
            )
        {
            var cells_connection =
                entrance_door
                .Q__Cells_Connection();

            bool cells_connection_exists =
                voronoi_tessellation
                .connectivity_graph
                .Q__Contains_Edge(cells_connection);

            return cells_connection_exists;
        }

        private bool Q__Is_Entrance_Door__Properly_Placed__Check_2__Wall_Length(
            EG__Entrance_Door entrance_door
            )
        {
            var cells_connection =
                entrance_door
                .Q__Cells_Connection();

            bool found_shared_line;
            Line_Segment shared_line;
            voronoi_tessellation.Q__Shared_Line__Between_Neighbor_Cells(
                cells_connection.v1,
                cells_connection.v2,
                out found_shared_line,
                out shared_line
                );

            if (found_shared_line == false)
            {
                return false;
            }

            double shared_line_length = shared_line.Q__Magnitude();
            if (shared_line_length >= prescription.openings__minimum_wall_length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Q__Is_Entrance_Door__Properly_Placed__Check_3__Cells_Space_Units(
            EG__Entrance_Door entrance_door
            )
        {
            var space_unit = entrance_door.Q__Space_Unit();

            // test whether one of the cells correspond to the space unit and the other is unoccupied
            int cell_1 = entrance_door.Q__Cell_1();
            int cell_2 = entrance_door.Q__Cell_2();

            bool cell_1__free_and_legal = Q__Is_Cell__Free_And_Active(cell_1);
            bool cell_2__free_and_legal = Q__Is_Cell__Free_And_Active(cell_2);
            bool cell_1__belongs_to_space_unit = space_unit__per__cell[cell_1] == space_unit;
            bool cell_2__belongs_to_space_unit = space_unit__per__cell[cell_2] == space_unit;

            bool proper_cell_use =
                (cell_1__free_and_legal && cell_2__belongs_to_space_unit)
                ||
                (cell_2__free_and_legal && cell_1__belongs_to_space_unit);

            return proper_cell_use;
        }

        private bool Q__Is_Entrance_Door__Properly_Placed__Check_4__Unique_Location(
            EG__Entrance_Door entrance_door
            )
        {
            int num = Q__Num__Openings__On_Edge(entrance_door.cells_connection);
            return num == 1;
        }

        public bool Q__Is_Window__Proper(EG__Window window)
        {
            bool prescribed = Q__Is_Window__Prescribed(window);

            if (prescribed == false)
            {
                return false;
            }

            bool properly_placed = Q__Is_Window__Properly_Placed(window);

            if (properly_placed == false)
            {
                return false;
            }

            return true;
        }

        private bool Q__Is_Window__Prescribed(
            EG__Window window
            )
        {
            var space_unit = window.Q__Space_Unit();

            bool prescribed =
                prescription
                .num_windows__per__space_unit[space_unit] > 0;

            return prescribed;
        }

        public bool Q__Is_Window__Properly_Placed(EG__Window window)
        {
            bool cells_connection_exists = Q__Is_Window__Properly_Placed__Check_1__Cells_Connection(
                window
                );

            if (cells_connection_exists == false)
            {
                return false;
            }

            bool proper_wall_length = Q__Is_Window__Properly_Placed__Check_2__Wall_Length(
                window
                );

            if (proper_wall_length == false)
            {
                return false;
            }

            bool proper_cells_space_units = Q__Is_Window__Properly_Placed__Check_3__Cells_Space_Units(
                window
                );

            if (proper_cells_space_units == false)
            {
                return false;
            }

            bool unique_location = Q__Is_Window__Properly_Placed__Check_4__Unique_Location(
                window
                );

            if (unique_location == false)
            {
                return false;
            }

            return true;
        }

        private bool Q__Is_Window__Properly_Placed__Check_1__Cells_Connection(
            EG__Window window
            )
        {
            var cells_connection =
                window
                .Q__Cells_Connection();

            bool cells_connection_exists =
                voronoi_tessellation
                .connectivity_graph
                .Q__Contains_Edge(cells_connection);

            return cells_connection_exists;
        }

        private bool Q__Is_Window__Properly_Placed__Check_2__Wall_Length(
            EG__Window window
            )
        {
            var cells_connection =
                window
                .Q__Cells_Connection();

            bool found_shared_line;
            Line_Segment shared_line;
            voronoi_tessellation.Q__Shared_Line__Between_Neighbor_Cells(
                cells_connection.v1,
                cells_connection.v2,
                out found_shared_line,
                out shared_line
                );

            if (found_shared_line == false)
            {
                return false;
            }

            double shared_line_length = shared_line.Q__Magnitude();
            if (shared_line_length >= prescription.openings__minimum_wall_length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Q__Is_Window__Properly_Placed__Check_3__Cells_Space_Units(
            EG__Window window
            )
        {
            var space_unit = window.Q__Space_Unit();

            // test whether one of the cells correspond to the space unit and the other is unoccupied
            int cell_1 = window.Q__Cell_1();
            int cell_2 = window.Q__Cell_2();

            bool cell_1__outside = 
                Q__Is_Cell__Free_And_Active(cell_1) 
                ||
                Q__Is_Cell__Exterior_Space_Unit(cell_1);


            bool cell_2__outside =
                Q__Is_Cell__Free_And_Active(cell_2)
                ||
                Q__Is_Cell__Exterior_Space_Unit(cell_2);


            bool cell_1__belongs_to_space_unit = space_unit__per__cell[cell_1] == space_unit;
            bool cell_2__belongs_to_space_unit = space_unit__per__cell[cell_2] == space_unit;

            bool proper_cell_use =
                (cell_1__outside && cell_2__belongs_to_space_unit)
                ||
                (cell_2__outside && cell_1__belongs_to_space_unit);

            return proper_cell_use;
        }

        private bool Q__Is_Window__Properly_Placed__Check_4__Unique_Location(
            EG__Window window
            )
        {
            int num = Q__Num__Openings__On_Edge(window.cells_connection);
            return num == 1;
        }

        public List<EG__Connection_Door> Q__Connection_Doors()
        {
            return new List<EG__Connection_Door>(connection_doors);
        }

        public List<EG__Entrance_Door> Q__Entrance_Doors()
        {
            return new List<EG__Entrance_Door>(entrance_doors);
        }

        public List<EG__Window> Q__Windows()
        {
            return new List<EG__Window>(windows);
        }
    }
}
