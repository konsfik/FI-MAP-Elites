using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools;
using FI_MAP_Elites__PCG.Shared_Elements;

namespace FI_MAP_Elites__PCG.Algorithms.CMCE
{
    public partial class FI_MAP_Elites<T>
        where T : Data_Structure
    {
        public void M__Generate_Initial_Population(
            I_PRNG rand,
            int initial_population_size
            )
        {
            for (int i = 0; i < initial_population_size; i++)
            {
                T individual = generation_method.Generate_Individual(rand);
                bool is_feasible = Q__Calculate_Feasibility(individual);

                double individual_fitness = Q__Calculate_Fitness(individual, is_feasible);
                double[] feature_vector = Q__Calculate_Feature_Vector(individual, is_feasible);
                FIME__Location location = Q__Calculate_Location(feature_vector, is_feasible);

                var placement_result = M__Attempt_Place_Individual(
                    individual,
                    location,
                    individual_fitness,
                    feature_vector
                    );

                num_evaluations++;

                Invoke_Event__Individual_Generated(
                    individual,
                    location,
                    feature_vector,
                    individual_fitness,
                    placement_result
                    );
            }
        }

        public void M__Advance__One_Step(
            I_PRNG rand
            )
        {
            // select parent location
            FIME__Location parent_location = parent_selection_method.Select__Parent_Location(rand, this);

            double[] parent_feature_vector = new double[1];
            double parent_fitness = 0.0;
            if (parent_location.state == FIME__State_Type.Feasible)
            {
                parent_feature_vector = feasible_state.feature_vectors__archive[parent_location.cell].Q__Deep_Copy();
                parent_fitness = feasible_state.fitness__archive[parent_location.cell];
            }
            else if (parent_location.state == FIME__State_Type.Infeasible)
            {
                parent_feature_vector = infeasible_state.feature_vectors__archive[parent_location.cell].Q__Deep_Copy();
                parent_fitness = infeasible_state.fitness__archive[parent_location.cell];
            }

            // get copy of parent and mutate it
            T offspring = Q__Individual__Deep_Copy(parent_location);
            if (parent_location.state == FIME__State_Type.Feasible)
            {
                feasible_state.mutation_method.Mutate_Individual(rand, offspring);
            }
            else if (parent_location.state == FIME__State_Type.Infeasible)
            {
                infeasible_state.mutation_method.Mutate_Individual(rand, offspring);
            }

            bool offspring_is_feasible = feasibility_discrimination_method.Q__Satisfied(offspring);
            double offspring_fitness = Q__Calculate_Fitness(offspring, offspring_is_feasible);
            double[] offspring_feature_vector = Q__Calculate_Feature_Vector(offspring, offspring_is_feasible);
            FIME__Location offspring_location = Q__Calculate_Location(offspring_feature_vector, offspring_is_feasible);

            FIME__Placement_Result offspring_placement_result = M__Attempt_Place_Individual(
                offspring,
                offspring_location,
                offspring_fitness,
                offspring_feature_vector
                );

            num_evaluations++;

            Invoke_Event__Offspring_Generated(
                individual: offspring,
                parent_location: parent_location,
                parent_feature_vector: parent_feature_vector,
                parent_fitness: parent_fitness,
                offspring_location: offspring_location,
                offspring_feature_vector: offspring_feature_vector,
                offspring_fitness: offspring_fitness,
                placement_result: offspring_placement_result
                );
        }

        /// <summary>
        /// Places an individual in this algorithm's archive. 
        /// </summary>
        /// <param name="individual"></param>
        public void M__Place_Preexisting_Individual(
            T individual
            )
        {
            bool is_feasible = Q__Calculate_Feasibility(individual);

            double individual_fitness = Q__Calculate_Fitness(individual, is_feasible);
            double[] individual_feature_vector = Q__Calculate_Feature_Vector(individual, is_feasible);
            FIME__Location individual_location = Q__Calculate_Location(individual_feature_vector, is_feasible);

            var placement_result = M__Attempt_Place_Individual(
                individual,
                individual_location,
                individual_fitness,
                individual_feature_vector
                );

            num_evaluations++;

            Invoke_Event__Preexisting_Individual_Placed(
                individual_location,
                individual_feature_vector,
                placement_result
                );
        }

        private FIME__Placement_Result M__Attempt_Place_Individual(
            T individual,
            FIME__Location individual_location,
            double individual_fitness,
            double[] feature_vector
            )
        {
            if (individual_location.cell < 0)
            {
                return FIME__Placement_Result.FAILURE__OUT_OF_RANGE;
            }
            else
            {
                bool location_occupied = Q__Is_Location_Occupied(individual_location);
                double preexisting_individual_fitness = Q__Fitness_At_Location(individual_location);

                if (location_occupied)
                {
                    if (individual_location.state == FIME__State_Type.Feasible)
                    {
                        bool should_replace =
                            feasible_state.Q__Should_Replace(
                                old_fitness: preexisting_individual_fitness,
                                new_fitness: individual_fitness
                                );
                        if (should_replace)
                        {
                            feasible_state.M__Place_Individual(
                                individual_location.cell,
                                individual,
                                individual_fitness,
                                feature_vector
                                );

                            return FIME__Placement_Result.SUCCESS__REPLACEMENT;
                        }
                        else return FIME__Placement_Result.FAILURE__REPLACEMENT;
                    }
                    else if (individual_location.state == FIME__State_Type.Infeasible)
                    {
                        bool should_replace =
                            infeasible_state.Q__Should_Replace(
                                old_fitness: preexisting_individual_fitness,
                                new_fitness: individual_fitness
                                );

                        if (should_replace)
                        {
                            infeasible_state.M__Place_Individual(
                                individual_location.cell,
                                individual,
                                individual_fitness,
                                feature_vector
                                );

                            return FIME__Placement_Result.SUCCESS__REPLACEMENT;
                        }
                        else return FIME__Placement_Result.FAILURE__REPLACEMENT;
                    }
                    else throw new Exception("improper state");
                }
                else // if location is not occupied
                {
                    if (individual_location.state == FIME__State_Type.Feasible)
                    {
                        feasible_state.M__Place_Individual(
                            individual_location.cell,
                            individual,
                            individual_fitness,
                            feature_vector
                            );

                        return FIME__Placement_Result.SUCCESS__DISCOVERY;
                    }
                    else if (individual_location.state == FIME__State_Type.Infeasible)
                    {
                        infeasible_state.M__Place_Individual(
                            individual_location.cell,
                            individual,
                            individual_fitness,
                            feature_vector
                            );

                        return FIME__Placement_Result.SUCCESS__DISCOVERY;
                    }
                    else throw new Exception("improper state");
                }
            }
        }

    }
}
