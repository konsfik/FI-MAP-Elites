using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{
    public static class Problem_Specification_Factory
    {
        public static DS__Layout_Constraints Problem_Specification__PCA_Test_1()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_2", 1.0, 0.1);

            RGB_Color living_room_color = new RGB_Color(255, 255, 0);
            RGB_Color kitchen_color = new RGB_Color(255, 127, 0);
            RGB_Color master_bedroom_color = new RGB_Color(0, 255, 0);
            RGB_Color child_bedroom_color = new RGB_Color(0, 255, 127);
            RGB_Color private_hall_color = new RGB_Color(0, 255, 255);
            RGB_Color guest_bathroom_color = new RGB_Color(255, 0, 127);
            RGB_Color main_bathroom_color = new RGB_Color(255, 0, 255);
            RGB_Color entrance_hall_color = new RGB_Color(0, 127, 255);


            prescription.M__Add_Space_Unit(
                id: 0,
                name: "Kitchen",
                color: kitchen_color,
                area: 10.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 1,
                name: "Living room",
                color: living_room_color,
                area: 30.0,
                num_entrance_doors: 0,
                num_windows: 2
                );
            prescription.M__Add_Space_Unit(
                id: 2,
                name: "Master bedroom",
                color: master_bedroom_color,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 3,
                name: "Childrens' bedroom",
                color: child_bedroom_color,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 4,
                name: "Private hall",
                color: private_hall_color,
                area: 10.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 5,
                name: "Guest bathroom",
                color: guest_bathroom_color,
                area: 4.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 6,
                name: "Main bathroom",
                color: main_bathroom_color,
                area: 6.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 7,
                name: "Entrance hall",
                color: entrance_hall_color,
                area: 10.0,
                num_entrance_doors: 1,
                num_windows: 1
                );

            prescription.M__Add_Connection(1, 0); // living room - kitchen
            prescription.M__Add_Connection(1, 4); // living room - private hall
            prescription.M__Add_Connection(1, 7); // living room - entrance hall
            prescription.M__Add_Connection(4, 1); // private hall - livingroom
            prescription.M__Add_Connection(4, 2); // private hall - master bedroom
            prescription.M__Add_Connection(4, 3); // private hall - childrens bedroom
            prescription.M__Add_Connection(4, 5); // private hall - guest bathroom
            prescription.M__Add_Connection(4, 6); // private hall - main bathroom

            return prescription;
        }

        public static DS__Layout_Constraints Hooper_House()
        {
            RGB_Color bedrooms_color = new RGB_Color(255, 127, 127);
            RGB_Color toilets_color = new RGB_Color(127, 127, 255);
            RGB_Color corridors_color = new RGB_Color(127, 64, 64);
            RGB_Color living_color = new RGB_Color(255, 255, 127);

            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("Cooper_House", 1.0, 0.1);

            prescription.M__Add_Space_Unit(
                id: 0,
                name: "ENTRANCE",
                color: corridors_color,
                area: 31.0,
                num_entrance_doors: 1,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 1,
                name: "STAIRCASE",
                color: corridors_color,
                area: 7.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 2,
                name: "PLAYROOM",
                color: living_color,
                area: 47.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 3,
                name: "STORAGE",
                color: RGB_Color.red,
                area: 8.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 4,
                name: "BR6",
                color: bedrooms_color,
                area: 16.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 5,
                name: "BR5",
                color: bedrooms_color,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 6,
                name: "WC3",
                color: toilets_color,
                area: 4.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 7,
                name: "HALL1",
                color: corridors_color,
                area: 12.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 8,
                name: "WC2",
                color: toilets_color,
                area: 5.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 9,
                name: "BR4",
                color: bedrooms_color,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 10,
                name: "BR3",
                color: bedrooms_color,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 11,
                name: "BR2",
                color: bedrooms_color,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 12,
                name: "BR1",
                color: bedrooms_color,
                area: 28.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 13,
                name: "WC1",
                color: toilets_color,
                area: 5.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 14,
                name: "YARD",
                color: RGB_Color.red,
                area: 105.0,
                num_entrance_doors: 1,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 15,
                name: "WC4",
                color: toilets_color,
                area: 3.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 16,
                name: "CORRIDOR",
                color: corridors_color,
                area: 7.5,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 17,
                name: "LIVING",
                color: living_color,
                area: 78,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 18,
                name: "KITCHEN",
                color: RGB_Color.red,
                area: 27,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Space_Unit(
                id: 19,
                name: "DINING",
                color: RGB_Color.red,
                area: 25,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Connection(0, 1);
            prescription.M__Add_Connection(0, 2);
            prescription.M__Add_Connection(0, 14);
            prescription.M__Add_Connection(0, 15);
            prescription.M__Add_Connection(0, 16);

            prescription.M__Add_Connection(2, 3);
            prescription.M__Add_Connection(2, 4);
            prescription.M__Add_Connection(2, 5);
            prescription.M__Add_Connection(2, 6);
            prescription.M__Add_Connection(2, 7);
            prescription.M__Add_Connection(2, 12);

            prescription.M__Add_Connection(12, 13);

            prescription.M__Add_Connection(7, 8);
            prescription.M__Add_Connection(7, 9);
            prescription.M__Add_Connection(7, 10);
            prescription.M__Add_Connection(7, 11);

            prescription.M__Add_Connection(16, 17);
            prescription.M__Add_Connection(16, 18);
            prescription.M__Add_Connection(17, 19);
            prescription.M__Add_Connection(18, 19);

            return prescription;
        }

        public static DS__Layout_Constraints Game_Map__1()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_1", 1.0, 0.1);

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
                num_entrance_doors: 1,
                num_windows: 2
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

        public static DS__Layout_Constraints House_1__Simple()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_1", 1.0, 0.1);

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

        public static DS__Layout_Constraints House_1()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_1", 1.0, 0.1);

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
                num_entrance_doors: 1,
                num_windows: 2
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

        public static DS__Layout_Constraints House_2()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_2", 1.0, 0.1);

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
                num_windows: 2
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
                name: "Guest bathroom",
                color: RGB_Color.magenta,
                area: 4.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 6,
                name: "Main bathroom",
                color: RGB_Color.magenta,
                area: 6.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Connection(1, 0); // living room - kitchen
            prescription.M__Add_Connection(1, 4); // living room - private hall
            prescription.M__Add_Connection(4, 1); // private hall - livingroom
            prescription.M__Add_Connection(4, 2); // private hall - master bedroom
            prescription.M__Add_Connection(4, 3); // private hall - childrens bedroom
            prescription.M__Add_Connection(4, 5); // private hall - guest bathroom
            prescription.M__Add_Connection(4, 6); // private hall - main bathroom

            return prescription;
        }

        public static DS__Layout_Constraints House_3()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_3", 1.0, 0.1);

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
                num_windows: 2
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
                name: "Guest bathroom",
                color: RGB_Color.magenta,
                area: 4.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 6,
                name: "Main bathroom",
                color: RGB_Color.pink,
                area: 6.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 7,
                name: "Entrance hall",
                color: RGB_Color.orange,
                area: 6.0,
                num_entrance_doors: 1,
                num_windows: 1
                );

            prescription.M__Add_Connection(1, 0); // living room - kitchen
            prescription.M__Add_Connection(1, 7); // living room - entrance hall
            prescription.M__Add_Connection(1, 4); // living room - private hall
            prescription.M__Add_Connection(4, 2); // private hall - master bedroom
            prescription.M__Add_Connection(4, 3); // private hall - childrens bedroom
            prescription.M__Add_Connection(4, 5); // private hall - guest bathroom
            prescription.M__Add_Connection(4, 6); // private hall - main bathroom

            return prescription;
        }

        public static DS__Layout_Constraints House_4()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("House_2", 1.0, 0.1);

            prescription.M__Add_Space_Unit(
                id: 0,
                name: "Kitchen",
                color: RGB_Color.red,
                area: 12.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 1,
                name: "Living room",
                color: RGB_Color.yellow,
                area: 36.0,
                num_entrance_doors: 1,
                num_windows: 2
                );
            prescription.M__Add_Space_Unit(
                id: 2,
                name: "Master bedroom",
                color: RGB_Color.green,
                area: 20.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 3,
                name: "Childrens' bedroom",
                color: RGB_Color.cyan,
                area: 20.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 4,
                name: "Private hall",
                color: RGB_Color.purple,
                area: 12.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 5,
                name: "Guest bathroom",
                color: RGB_Color.magenta,
                area: 6.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 6,
                name: "Main bathroom",
                color: RGB_Color.light_blue,
                area: 8.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Connection(1, 0); // living room - kitchen
            prescription.M__Add_Connection(1, 4); // living room - private hall
            prescription.M__Add_Connection(4, 1); // private hall - livingroom
            prescription.M__Add_Connection(4, 2); // private hall - master bedroom
            prescription.M__Add_Connection(4, 3); // private hall - childrens bedroom
            prescription.M__Add_Connection(4, 5); // private hall - guest bathroom
            prescription.M__Add_Connection(4, 6); // private hall - main bathroom

            return prescription;
        }

        public static DS__Layout_Constraints Circular_1()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("Circular_1", 1.0, 0.1);

            prescription.M__Add_Space_Unit(
                id: 0,
                name: "R1",
                color: RGB_Color.red,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 1,
                name: "R2",
                color: RGB_Color.yellow,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 2
                );
            prescription.M__Add_Space_Unit(
                id: 2,
                name: "R3",
                color: RGB_Color.green,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 3,
                name: "R4",
                color: RGB_Color.cyan,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 4,
                name: "R5",
                color: RGB_Color.purple,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Connection(0, 1);
            prescription.M__Add_Connection(1, 2);
            prescription.M__Add_Connection(2, 3);
            prescription.M__Add_Connection(3, 4);
            prescription.M__Add_Connection(4, 0);

            return prescription;
        }

        public static DS__Layout_Constraints Circular_2()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("Circular_2", 1.0, 0.1);

            prescription.M__Add_Space_Unit(
                id: 0,
                name: "R1",
                color: RGB_Color.red,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 1,
                name: "R2",
                color: RGB_Color.green,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 2
                );
            prescription.M__Add_Space_Unit(
                id: 2,
                name: "R3",
                color: RGB_Color.blue,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 3,
                name: "R4",
                color: RGB_Color.yellow,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 1
                );
            prescription.M__Add_Space_Unit(
                id: 4,
                name: "R5",
                color: RGB_Color.orange,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 5,
                name: "R6",
                color: RGB_Color.cyan,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 0
                );
            prescription.M__Add_Space_Unit(
                id: 6,
                name: "R7",
                color: RGB_Color.purple,
                area: 15.0,
                num_entrance_doors: 0,
                num_windows: 0
                );

            prescription.M__Add_Connection(0, 1);
            prescription.M__Add_Connection(1, 2);
            prescription.M__Add_Connection(2, 3);
            prescription.M__Add_Connection(3, 4);
            prescription.M__Add_Connection(4, 5);
            prescription.M__Add_Connection(5, 6);
            prescription.M__Add_Connection(6, 0);

            return prescription;
        }

        public static DS__Layout_Constraints Hadid__Villa_Top_Floor()
        {
            // define the rooms

            DS__Layout_Constraints prescription = new DS__Layout_Constraints("Hadid_Tower_Half_Floor", 1.0, 0.1);

            RGB_Color master_bedroom_color = new RGB_Color(170, 171, 254);
            RGB_Color her_color = new RGB_Color(164, 196, 234);
            RGB_Color his_color = new RGB_Color(216, 215, 255);
            RGB_Color circulation_color = new RGB_Color(247, 84, 85);
            RGB_Color children_bedroom_color = new RGB_Color(164, 234, 226);
            RGB_Color children_study_color = new RGB_Color(252, 192, 192);
            RGB_Color elevator_color = new RGB_Color(255, 215, 94);


            prescription.M__Add_Space_Unit(
                id: 0,
                name: "Master Bedroom",
                color: master_bedroom_color,
                area: 70.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 1,
                name: "Her Bathroom",
                color: her_color,
                area: 25.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 2,
                name: "Her Dressing Room",
                color: her_color,
                area: 160.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 3,
                name: "Private Salon",
                color: master_bedroom_color,
                area: 60.0,
                num_entrance_doors: 1,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 4,
                name: "His Bathroom",
                color: his_color,
                area: 25.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 5,
                name: "His Dressing Room",
                color: his_color,
                area: 120.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 6,
                name: "Corridor",
                color: circulation_color,
                area: 82.0,
                num_entrance_doors: 1,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 7,
                name: "Child 1 Bedroom",
                color: children_bedroom_color,
                area: 70.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 8,
                name: "Child 1 Bathroom",
                color: children_bedroom_color,
                area: 30.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 9,
                name: "Child 2 Bedroom",
                color: children_bedroom_color,
                area: 75.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 10,
                name: "Child 2 Bathroom",
                color: children_bedroom_color,
                area: 25.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 11,
                name: "Staircase & Elevator",
                color: elevator_color,
                area: 25.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 12,
                name: "Study 1",
                color: children_study_color,
                area: 25.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 13,
                name: "Study 2",
                color: children_study_color,
                area: 25.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 14,
                name: "Son Bedroom",
                color: children_bedroom_color,
                area: 50.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 15,
                name: "Son Bathroom",
                color: children_bedroom_color,
                area: 25.0,
                num_entrance_doors: 0,
                num_windows: 1
                );

            prescription.M__Add_Space_Unit(
                id: 16,
                name: "Son Dressing Room",
                color: children_bedroom_color,
                area: 50.0,
                num_entrance_doors: 0,
                num_windows: 1
                );


            prescription.M__Add_Connection(0, 1);
            prescription.M__Add_Connection(1, 2);
            prescription.M__Add_Connection(2, 3);
            prescription.M__Add_Connection(0, 3);
            prescription.M__Add_Connection(3, 4);
            prescription.M__Add_Connection(3, 5);
            prescription.M__Add_Connection(4, 5);
            prescription.M__Add_Connection(3, 6);
            prescription.M__Add_Connection(6, 7);
            prescription.M__Add_Connection(7, 8);
            prescription.M__Add_Connection(6, 9);
            prescription.M__Add_Connection(9, 10);
            prescription.M__Add_Connection(6, 11);
            prescription.M__Add_Connection(6, 13);
            prescription.M__Add_Connection(6, 12);
            prescription.M__Add_Connection(6, 14);
            prescription.M__Add_Connection(14, 15);
            prescription.M__Add_Connection(14, 16);

            return prescription;
        }

    }
}
