using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

namespace UTEST__FI_MAP_Elites.Algorithms.Shared_Elements
{
    [TestClass]
    public class Test__Ortho_Tessellation
    {
        [TestMethod]
        public void Test__Q__Cell__To__Coords()
        {
            BS_Ortho_Tessellation tessellation = new BS_Ortho_Tessellation(
                num_bcs: 2,
                num_cells__per__bc: new int[] { 4, 3 },
                min_value__per__bc: new double[] { 0.0, 0.0 },
                max_value__per__bc: new double[] { 1.0, 1.0 },
                out_of_range_treatment: BS_OT__Out_Of_Range_Treatment.DISCARD
                );

            int id = 0;
            var coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 0);

            id = 1;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 1);
            Assert.IsTrue(coords[1] == 0);

            id = 2;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 2);
            Assert.IsTrue(coords[1] == 0);

            id = 3;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 0);

            id = 4;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 1);

            id = 5;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 1);
            Assert.IsTrue(coords[1] == 1);

            id = 6;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 2);
            Assert.IsTrue(coords[1] == 1);

            id = 7;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 1);

            id = 8;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 2);

            id = 9;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 1);
            Assert.IsTrue(coords[1] == 2);

            id = 10;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 2);
            Assert.IsTrue(coords[1] == 2);

            id = 11;
            coords = tessellation.Q__Cell__To__Coords(id);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 2);

            id = -1;
            Assert.ThrowsException<IndexOutOfRangeException>(() => tessellation.Q__Cell__To__Coords(id));

            id = 12;
            Assert.ThrowsException<IndexOutOfRangeException>(() => tessellation.Q__Cell__To__Coords(id));

        }

        [TestMethod]
        public void Test__Q__Coords__To__Cell()
        {
            BS_Ortho_Tessellation tessellation = new BS_Ortho_Tessellation(
                num_bcs: 2,
                num_cells__per__bc: new int[] { 4, 3 },
                min_value__per__bc: new double[] { 0.0, 0.0 },
                max_value__per__bc: new double[] { 1.0, 1.0 },
                out_of_range_treatment: BS_OT__Out_Of_Range_Treatment.DISCARD
                );

            int[] coords = new int[] { 0, 0 };
            int id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 0);

            coords = new int[] { 1, 0 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 1);

            coords = new int[] { 2, 0 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 2);

            coords = new int[] { 3, 0 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 3);

            coords = new int[] { 0, 1 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 4);

            coords = new int[] { 1, 1 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 5);

            coords = new int[] { 2, 1 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 6);

            coords = new int[] { 3, 1 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 7);

            coords = new int[] { 0, 2 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 8);

            coords = new int[] { 1, 2 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 9);

            coords = new int[] { 2, 2 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 10);

            coords = new int[] { 3, 2 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == 11);

            coords = new int[] { 0, 3 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == -1);

            coords = new int[] { 4, 0 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == -1);

            coords = new int[] { 0, -1 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == -1);

            coords = new int[] { -1, 0 };
            id = tessellation.Q__Coords__To__Cell(coords);
            Assert.IsTrue(id == -1);

        }

        [TestMethod]
        public void Test__Q__Feature_Vector__To__Cell()
        {
            BS_Ortho_Tessellation tessellation = new BS_Ortho_Tessellation(
                num_bcs: 2,
                num_cells__per__bc: new int[] { 4, 4 },
                min_value__per__bc: new double[] { 0.0, 0.0 },
                max_value__per__bc: new double[] { 1.0, 1.0 },
                out_of_range_treatment: BS_OT__Out_Of_Range_Treatment.DISCARD
                );

            double[] feature_vector = new double[] { -0.01, -0.01 };
            int[] coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            int cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == -1);
            Assert.IsTrue(coords[1] == -1);
            Assert.IsTrue(cell_id == -1);

            feature_vector = new double[] { -100.0, -100.0 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == -1);
            Assert.IsTrue(coords[1] == -1);
            Assert.IsTrue(cell_id == -1);

            feature_vector = new double[] { 0.01, 0.01 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 0);
            Assert.IsTrue(cell_id == 0);

            feature_vector = new double[] { 0.24, 0.24 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 0);
            Assert.IsTrue(cell_id == 0);

            feature_vector = new double[] { 0.26, 0.26 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 1);
            Assert.IsTrue(coords[1] == 1);
            Assert.IsTrue(cell_id == 5);

            feature_vector = new double[] { 0.49, 0.49 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 1);
            Assert.IsTrue(coords[1] == 1);
            Assert.IsTrue(cell_id == 5);

            feature_vector = new double[] { 0.51, 0.51 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 2);
            Assert.IsTrue(coords[1] == 2);
            Assert.IsTrue(cell_id == 10);

            feature_vector = new double[] { 0.74, 0.74 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 2);
            Assert.IsTrue(coords[1] == 2);
            Assert.IsTrue(cell_id == 10);

            feature_vector = new double[] { 0.76, 0.76 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 3);
            Assert.IsTrue(cell_id == 15);

            feature_vector = new double[] { 0.99, 0.99 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 3);
            Assert.IsTrue(cell_id == 15);

            feature_vector = new double[] { 1.01, 1.01 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == -1);
            Assert.IsTrue(coords[1] == -1);
            Assert.IsTrue(cell_id == -1);

            feature_vector = new double[] { 100.0, 100.0 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == -1);
            Assert.IsTrue(coords[1] == -1);
            Assert.IsTrue(cell_id == -1);

            tessellation = new BS_Ortho_Tessellation(
                num_bcs: 2,
                num_cells__per__bc: new int[] { 4, 2 },
                min_value__per__bc: new double[] { 0.0, 0.0 },
                max_value__per__bc: new double[] { 1.0, 1.0 },
                out_of_range_treatment: BS_OT__Out_Of_Range_Treatment.DISCARD
                );

            feature_vector = new double[] { -0.01, -0.01 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == -1);
            Assert.IsTrue(coords[1] == -1);
            Assert.IsTrue(cell_id == -1);

            feature_vector = new double[] { 0.0, 0.0 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 0);
            Assert.IsTrue(cell_id == 0);

            feature_vector = new double[] { 0.24, 0.49 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 0);
            Assert.IsTrue(cell_id == 0);

            feature_vector = new double[] { 0.26, 0.51 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 1);
            Assert.IsTrue(coords[1] == 1);
            Assert.IsTrue(cell_id == 5);

            feature_vector = new double[] { 0.51, 0.51 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 2);
            Assert.IsTrue(coords[1] == 1);
            Assert.IsTrue(cell_id == 6);

            feature_vector = new double[] { 0.76, 0.51 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 1);
            Assert.IsTrue(cell_id == 7);


            tessellation = new BS_Ortho_Tessellation(
                num_bcs: 2,
                num_cells__per__bc: new int[] { 4, 4 },
                min_value__per__bc: new double[] { 0.0, 0.0 },
                max_value__per__bc: new double[] { 1.0, 1.0 },
                out_of_range_treatment: BS_OT__Out_Of_Range_Treatment.KEEP
                );

            feature_vector = new double[] { -0.01, -0.01 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 0);
            Assert.IsTrue(cell_id == 0);

            feature_vector = new double[] { -100.0, -100.0 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 0);
            Assert.IsTrue(coords[1] == 0);
            Assert.IsTrue(cell_id == 0);

            feature_vector = new double[] { 1.01, 1.01 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 3);
            Assert.IsTrue(cell_id == 15);

            feature_vector = new double[] { 100.0, 100.0 };
            coords = tessellation.Q__Feature_Vector__To__Coords(feature_vector);
            cell_id = tessellation.Q__Feature_Vector__To__Cell(feature_vector);
            Assert.IsTrue(coords[0] == 3);
            Assert.IsTrue(coords[1] == 3);
            Assert.IsTrue(cell_id == 15);
        }
    }
}
