using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Data_Structures.Voronoi
{
    public class DS_Voronoi__Feature_Vector : I_DS_Stats<DS__Voronoi>
    {
        // basic
        public double rect_width;
        public double rect_height;
        public double rect_area;
        public int num_generator_points;
        public int num_active_cells;

        // evaluations
        public double eval__cells_area__sum;
        public double eval__cells_area__average;
        public double eval__cells_area__simplistic_variance;
        public double eval__cells_area__diversity;
        public double eval__cells_area__minimum_difference;

        public double eval__cells_compactness__average;
        public double eval__cells_compactness__simplistic_variance;
        public double eval__cells_compactness__diversity;

        public double eval__lines_orthogonality__average;
        public double eval__lines_orthogonality__simplistic_variance;
        public double eval__lines_orthogonality__diversity;

        public double eval__angles_orthogonality__average;
        public double eval__angles_orthogonality__simplistic_variance;
        public double eval__angles_orthogonality__diversity;

        // evaluations normalized
        public double eval__cells_area__sum__normalized;
        public double eval__cells_area__average__normalized;
        public double eval__cells_area__simplistic_variance__normalized;
        public double eval__cells_area__diversity__normalized;
        public double eval__cells_area__minimum_difference__normalized;

        public double eval__cells_compactness__average__normalized;
        public double eval__cells_compactness__simplistic_variance__normalized;
        public double eval__cells_compactness__diversity__normalized;

        public double eval__lines_orthogonality__average__normalized;
        public double eval__lines_orthogonality__simplistic_variance__normalized;
        public double eval__lines_orthogonality__diversity__normalized;


        public DS_Voronoi__Feature_Vector(DS__Voronoi voronoi)
        {
            M__Update(voronoi);
        }

        public string Q__CSV_Header(string delimiter)
        {
            var field_names = 
                this
                .GetType()
                .GetFields(BindingFlags.Public)
                .Select(field => field.Name)
                .ToList();

            string output = "";

            int num_names = field_names.Count;

            for (int i = 0; i < num_names; i++) {
                output += field_names[i];
                if (i < num_names - 1)
                    output += delimiter;
            }

            return output;
        }

        public string Q__CSV_Row(string delimiter)
        {
            var field_values =
                this
                .GetType()
                .GetFields(BindingFlags.Public)
                .Select(field => field.GetValue(this).ToString())
                .ToList();

            string output = "";

            int num_names = field_values.Count;

            for (int i = 0; i < num_names; i++)
            {
                output += field_values[i];
                if (i < num_names - 1)
                    output += delimiter;
            }

            return output;
        }

        public void M__Update(DS__Voronoi individual)
        {
            this.rect_width = individual.bounding_rectangle.width;
            this.rect_height = individual.bounding_rectangle.height;
            this.rect_area = this.rect_width * this.rect_height;

            this.num_generator_points = individual.Q__Num_Generator_Points();
            this.num_active_cells = individual.Q__Num_Active_Cells();

            this.eval__cells_area__sum = individual.Q__Eval__Cells_Area__Sum();
            this.eval__cells_area__average = individual.Q__Eval__Cells_Area__Average();
            this.eval__cells_area__simplistic_variance = individual.Q__Eval__Cells_Area__Simplistic_Variance();
            this.eval__cells_area__diversity = individual.Q__Eval__Cells_Area__Diversity();
            this.eval__cells_area__minimum_difference = individual.Q__Eval__Cells_Area__Minimum_Difference();

            this.eval__cells_compactness__average = individual.Q__Eval__Cells_Compactness__Average();
            this.eval__cells_compactness__simplistic_variance = individual.Q__Eval__Cells_Compactness__Simplistic_Variance();
            this.eval__cells_compactness__diversity = individual.Q__Eval__Cells_Compactness__Diversity();

            this.eval__lines_orthogonality__average = individual.Q__Eval__Lines_Orthogonality__Average();
            this.eval__lines_orthogonality__simplistic_variance = individual.Q__Eval__Lines_Orthogonality__Simplistic_Variance();
            this.eval__lines_orthogonality__diversity = individual.Q__Eval__Lines_Orthogonality__Diversity();

            this.eval__angles_orthogonality__average = individual.Q__Eval__Angles_Orthogonality__Average();
            this.eval__angles_orthogonality__diversity = individual.Q__Eval__Angles_Orthogonality__Diversity();

            //var aaa = voronoi.Q__Eval__Lines_Orthogonality__Average
            //var aaa = voronoi.Q__Eval__Cells_Size__Diversity

            this.eval__cells_area__sum__normalized = individual.Q__Eval__Cells_Area__Sum__Normalized();
            this.eval__cells_area__average__normalized = individual.Q__Eval__Cells_Area__Average__Normalized();
            this.eval__cells_area__simplistic_variance__normalized = individual.Q__Eval__Cells_Area__Simplistic_Variance__Normalized();
            this.eval__cells_area__diversity__normalized = individual.Q__Eval__Cells_Area__Diversity__Normalized();
            this.eval__cells_area__minimum_difference__normalized = individual.Q__Eval__Cells_Area__Minimum_Difference__Normalized();

            this.eval__cells_compactness__average__normalized = individual.Q__Eval__Cells_Compactness__Average();
            this.eval__cells_compactness__simplistic_variance__normalized = individual.Q__Eval__Cells_Compactness__Simplistic_Variance__Normalized();
            this.eval__cells_compactness__diversity__normalized = individual.Q__Eval__Cells_Compactness__Diversity__Normalized();

            eval__lines_orthogonality__average__normalized = individual.Q__Eval__Lines_Orthogonality__Average();
            eval__lines_orthogonality__simplistic_variance__normalized = individual.Q__Eval__Lines_Orthogonality__Simplistic_Variance();
        }
    }
}
