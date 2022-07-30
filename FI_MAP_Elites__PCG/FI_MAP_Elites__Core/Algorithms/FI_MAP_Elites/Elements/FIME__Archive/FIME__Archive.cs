using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FI_MAP_Elites__PCG.Shared_Elements;
using Common_Tools;
using FI_MAP_Elites__PCG.Algorithms.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class FIME__Archive<T> :Data_Structure
        where T : Data_Structure
    {
        // properties / methods
        public readonly Evaluation_Method<T> fitness_function;
        public readonly EA__Fitness_Goal fitness_goal;
        public readonly EA__Individual_Replacement_Rule individual_comparison_type;
        public readonly Evaluation_Method<T>[] evaluation_method__per__feature;
        public readonly Mutation_Method<T> mutation_method;

        // tessellation
        public readonly BS_Ortho_Tessellation tessellation;

        // data
        public readonly T[] individuals__archive;
        public readonly double[] fitness__archive;
        public readonly double[][] feature_vectors__archive;

        public FIME__Archive(
            Evaluation_Method<T> fitness_function,
            EA__Fitness_Goal fitness_goal,
            EA__Individual_Replacement_Rule individual_comparison_type,
            Evaluation_Method<T>[] evaluation_method__per__feature,
            BS_Ortho_Tessellation tessellation,
            Mutation_Method<T> mutation_method
            )
        {
            // properties / methods
            this.fitness_function = (Evaluation_Method<T>)fitness_function.Q__Deep_Copy();
            this.fitness_goal = fitness_goal;
            this.individual_comparison_type = individual_comparison_type;
            this.evaluation_method__per__feature = evaluation_method__per__feature.Q__Deep_Copy();

            this.tessellation = (BS_Ortho_Tessellation)tessellation.Q__Deep_Copy();

            this.mutation_method = mutation_method;

            individuals__archive = new T[tessellation.num_cells];
            fitness__archive = new double[tessellation.num_cells];
            feature_vectors__archive = new double[tessellation.num_cells][];

            for (int i = 0; i < tessellation.num_cells; i++)
            {
                individuals__archive[i] = null;
                fitness__archive[i] = Double.NaN;
                feature_vectors__archive[i] = null;
            }
        }

        private FIME__Archive(FIME__Archive<T> state_to_copy)
        {
            this.fitness_function = 
                (Evaluation_Method<T>)state_to_copy.fitness_function.Q__Deep_Copy();
            this.fitness_goal =
                state_to_copy.fitness_goal;
            this.individual_comparison_type =
                state_to_copy.individual_comparison_type;
            this.evaluation_method__per__feature =
                state_to_copy.evaluation_method__per__feature.Q__Deep_Copy();
            this.mutation_method =
                (Mutation_Method<T>)state_to_copy.mutation_method.Q__Deep_Copy();
            this.tessellation =
                (BS_Ortho_Tessellation)state_to_copy.tessellation.Q__Deep_Copy();
            this.individuals__archive =
                state_to_copy.individuals__archive.Q__Deep_Copy();
            this.fitness__archive =
                state_to_copy.fitness__archive.Q__Deep_Copy();
            this.feature_vectors__archive =
                state_to_copy.feature_vectors__archive.Q__Deep_Copy();
        }

        public override object Q__Deep_Copy()
        {
            return new FIME__Archive<T>(this);
        }
    }
}
