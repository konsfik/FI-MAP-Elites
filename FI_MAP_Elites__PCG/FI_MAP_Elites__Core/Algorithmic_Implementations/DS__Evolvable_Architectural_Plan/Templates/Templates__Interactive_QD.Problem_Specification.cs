using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common_Tools;
using FI_MAP_Elites__PCG.Data_Structures.Undirected_Graph;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public static partial class Templates__Interactive_QD
    {
        public static DS__Layout_Constraints Template__House_0()
        {
            // define the rooms
            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_0", 1.0, 0.1);

            prescription.M__Add_Space_Unit(
                id: 0,
                name: "Kitchen",
                color: RGB_Color.red,
                area: 10.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 1,
                name: "Living room",
                color: RGB_Color.yellow,
                area: 30.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 2,
                name: "Master bedroom",
                color: RGB_Color.green,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 3,
                name: "Childrens' bedroom",
                color: RGB_Color.cyan,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 4,
                name: "Private hall",
                color: RGB_Color.purple,
                area: 10.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 5,
                name: "Private bathroom",
                color: RGB_Color.magenta,
                area: 10.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Connection(1, 4);
            prescription.M__Add_Connection(4, 0);
            prescription.M__Add_Connection(4, 1);
            prescription.M__Add_Connection(4, 2);
            prescription.M__Add_Connection(4, 3);
            prescription.M__Add_Connection(4, 5);

            return prescription;
        }

        public static DS__Layout_Constraints Template__House_Simple_Test()
        {
            // define the rooms
            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_1", 1.0, 0.1);

            RGB_Color color__living_room = new RGB_Color(255, 208, 64);
            RGB_Color color__veranda_1 = new RGB_Color(255, 208, 172);
            RGB_Color color__corridor = new RGB_Color(64, 208, 64);
            RGB_Color color__WC = new RGB_Color(64, 127, 255);
            RGB_Color color__bathroom = new RGB_Color(127, 64, 255);
            RGB_Color color__kitchen = new RGB_Color(255, 64, 64);

            prescription.M__Add_Space_Unit(
                id: 0,
                name: "Living Room",
                color: color__living_room,
                type: Space_Unit__Type.INTERIOR,
                area: 32.0,
                num_entrance_doors: 1,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 1,
                name: "Veranda 1",
                color: color__veranda_1,
                type: Space_Unit__Type.EXTERIOR,
                area: 18.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 2,
                name: "Corridor",
                color: color__corridor,
                type: Space_Unit__Type.INTERIOR,
                area: 5.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 3,
                name: "WC",
                color: color__WC,
                type: Space_Unit__Type.INTERIOR,
                area: 4.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 4,
                name: "Bathroom",
                color: color__bathroom,
                type: Space_Unit__Type.INTERIOR,
                area: 6.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 5,
                name: "Kitchen",
                color: color__kitchen,
                type: Space_Unit__Type.INTERIOR,
                area: 10.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Connection(0, 1);
            prescription.M__Add_Connection(0, 2);
            prescription.M__Add_Connection(2, 3);
            prescription.M__Add_Connection(2, 4);
            prescription.M__Add_Connection(2, 5);

            return prescription;
        }

        public static DS__Layout_Constraints Template__House_1()
        {
            // define the rooms
            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_1", 1.0, 0.1);

            RGB_Color color__living_room = new RGB_Color(255, 208, 64);
            RGB_Color color__veranda_1 = new RGB_Color(255, 208, 172);
            RGB_Color color__corridor = new RGB_Color(64, 208, 64);
            RGB_Color color__bedroom_1 = new RGB_Color(255, 160, 80);
            RGB_Color color__bedroom_2 = new RGB_Color(255, 80, 160);
            RGB_Color color__veranda_2 = new RGB_Color(255, 220, 110);
            RGB_Color color__WC = new RGB_Color(64, 127, 255);
            RGB_Color color__bathroom = new RGB_Color(127, 64, 255);
            RGB_Color color__kitchen = new RGB_Color(255, 64, 64);
            RGB_Color color__balcony = new RGB_Color(255, 172, 172);

            prescription.M__Add_Space_Unit(
                id: 0,
                name: "Living Room",
                color: color__living_room,
                type: Space_Unit__Type.INTERIOR,
                area: 32.0,
                num_entrance_doors: 1,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 1,
                name: "Veranda 1",
                color: color__veranda_1,
                type: Space_Unit__Type.EXTERIOR,
                area: 18.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 2,
                name: "Corridor",
                color: color__corridor,
                type: Space_Unit__Type.INTERIOR,
                area: 5.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 3,
                name: "Bedroom 1",
                color: color__bedroom_1,
                type: Space_Unit__Type.INTERIOR,
                area: 12.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 4,
                name: "Bedroom 2",
                color: color__bedroom_2,
                type: Space_Unit__Type.INTERIOR,
                area: 12.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 5,
                name: "Veranda 2",
                color: color__veranda_2,
                type: Space_Unit__Type.EXTERIOR,
                area: 8.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 6,
                name: "WC",
                color: color__WC,
                type: Space_Unit__Type.INTERIOR,
                area: 4.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 7,
                name: "Bathroom",
                color: color__bathroom,
                type: Space_Unit__Type.INTERIOR,
                area: 6.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 8,
                name: "Kitchen",
                color: color__kitchen,
                type: Space_Unit__Type.INTERIOR,
                area: 10.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 9,
                name: "Balcony",
                color: color__balcony,
                type: Space_Unit__Type.EXTERIOR,
                area: 4.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Connection(0, 1);
            prescription.M__Add_Connection(0, 2);
            prescription.M__Add_Connection(2, 3);
            prescription.M__Add_Connection(2, 4);
            prescription.M__Add_Connection(2, 6);
            prescription.M__Add_Connection(2, 7);
            prescription.M__Add_Connection(2, 8);
            prescription.M__Add_Connection(3, 5);
            prescription.M__Add_Connection(4, 5);
            prescription.M__Add_Connection(8, 9);

            return prescription;
        }
    }
}
