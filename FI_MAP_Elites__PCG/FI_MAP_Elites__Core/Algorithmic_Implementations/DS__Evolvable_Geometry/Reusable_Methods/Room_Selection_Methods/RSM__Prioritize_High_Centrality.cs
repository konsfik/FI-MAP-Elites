using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Common_Tools;

namespace FI_MAP_Elites__PCG.Data_Structures.Evolvable_Geometry
{

    public class RSM__Prioritize_High_Centrality<T> : Space_Unit_Selection_Method<T>
        where T:DS__Evolvable_Geometry
    {
        public override int Select_Space_Unit(
            I_PRNG rand,
            T individual,
            List<int> visited_rooms
            )
        {
            // method for selecting the first room
            if (visited_rooms.Count == 0)
            {
                // find the most connected rooms...
                List<int> highest_centrality_rooms = individual.Q__Highest_Degree_Space_Units();

                // select one of them, at random and return it.
                int selected_room = highest_centrality_rooms.Q__Random_Item(rand);
                return selected_room;
            }
            // method for selecting any consequent rooms 
            else
            {
                // find the next room candidates
                List<int> next_room_candidates =
                    individual
                    .Q_Space_Units__Prescribed_Neighbors(visited_rooms);


                // find the candidates with the highest centrality
                List<int> highest_centrality_candidates = 
                    individual
                    .Q__Highest_Degree_Space_Units(next_room_candidates);

                // select one of them at random and return it
                int selected_room = highest_centrality_candidates.Q__Random_Item(rand);

                return selected_room;
            }
        }
    }
}
