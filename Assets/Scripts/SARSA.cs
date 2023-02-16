using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;

public class SARSA 
{
   static void SarsaAlgorithm(ref List<List<State>> mapState , float gamma ,int nbEpisode, float epsilon)
   {
      for (int episode = 0; episode < nbEpisode; episode++)
      {
         // On recupere la zone initial
         int x = 0, y = 0;
         int actionInit = Random.Range(0,mapState[x][y].actions.Count);
         
         while (true)
         {
            // On exectue l'action initiale
            Vector2Int nextState = mapState[x][y].actions[actionInit].Act(new Vector2Int(x, y));
            float reward = mapState[x][y].reward;
            float nextScore = mapState[nextState.x][nextState.y].actions.Count;
            
            // On utilise l'algo d'exploration/exploitation 

            int nextAction;
            if (Random.Range(0.0f, 1.0f) < epsilon)
            {
               //Exploration 
               nextAction = EpsilonGreedy(mapState[x][y],nextState,epsilon);
            }
            else
            {
               //Exploitation
               nextAction = mapState[nextState.x][nextState.y].currentAction;
            }
            
            
         }
      }
   }

   public static int EpsilonGreedy(State state,Vector2Int PosState, float epsilon)
   {
      if (Random.Range(0f, 1f) < epsilon)
      {
         //Exploration
         return Random.Range(0, state.actions.Count);
      }
      else
      {
         float bestScore = float.MinValue;
         int bestAction = 0;

         for (int i = 0; i < state.actions.Count; i++)
         {
            Vector2Int nextStateCoordonee = state.actions[i].Act(new Vector2Int(PosState.x, PosState.y));
           // state nextState = 
         }
         return 0;
      }

      
   }
}
