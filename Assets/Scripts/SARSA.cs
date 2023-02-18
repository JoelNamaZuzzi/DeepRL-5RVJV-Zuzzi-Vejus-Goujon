using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;

using Utils;

public class SARSA 
{
  public static void SarsaAlgorithm(ref Dictionary<IntList, State> mapState ,  float gamma ,int nbEpisode, float epsilon, float tauxDapprentissage)
   {
       for (int episode = 0; episode < nbEpisode; episode++)
       {
          IntList xy = new IntList();
          KeyValuePair<IntList, State> curentState = new KeyValuePair<IntList, State>();
          foreach (var kvp in mapState)
          {
             if (kvp.Value.start)
             {
                curentState = kvp;
                xy = kvp.Key;
             }
          }
          
          int actionInit = EpsilonGreedy(mapState,xy,epsilon);
          
          //Initialisation des valeurs de Q(s,a) à 0 
          foreach (KeyValuePair<IntList,State> state in mapState)
          { 
             foreach (var action in state.Value.actions)
             {
                action.q = 0f;
             }
          }
         
          int iteration = 0;
          while (true ||iteration == 1000000)
          {
             // On exectue l'action initiale
             
             IntList nextState = curentState.Value.actions[actionInit].Act(curentState.Key);
             float reward = curentState.Value.reward;

             // On utilise l'algo d'exploration/exploitation 
             int nextAction = EpsilonGreedy(mapState,xy,epsilon);
          
             //mise a jour de Q(s,a)

             float currentQ = curentState.Value.actions[actionInit].q;
             float nextQ = mapState[nextState].actions[nextAction].q;
             curentState.Value.actions[actionInit].q = currentQ + tauxDapprentissage * (reward + gamma * nextQ - currentQ);
             
             xy = nextState;
             actionInit = nextAction;

             if (curentState.Value.final)
             {
                break;
             }
             iteration++;
          }
       }
   }

   // Algorithme d'exploration / exploitation
   public static int EpsilonGreedy(Dictionary<IntList, State> mapState,IntList Xy, float epsilon)
   {
      State actuState = mapState[Xy];
       if (Random.Range(0f, 1f) < epsilon)
       {
          //Exploration
          return Random.Range(0, actuState.actions.Count);
       }
       else
       {
          // Exploitation
          float bestScore = float.MinValue;
          int bestAction = 0;

          for (int i = 0; i < actuState.actions.Count; i++)
          {
             IntList nextStateCoordonee = actuState.actions[i].Act(Xy);
             State nextState = mapState[nextStateCoordonee];

             if (nextState.score > bestScore)
             {
                bestScore = nextState.score;
                bestAction = i;
             }
          }
          return bestAction;
       }
   }
}
