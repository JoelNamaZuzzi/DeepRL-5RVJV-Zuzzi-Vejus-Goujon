using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;

using Utils;

public class SARSA 
{
  public static void SarsaAlgorithm(ref Dictionary<IntList, State> mapState ,KeyValuePair<IntList,State> start,  float gamma ,int nbEpisode, float epsilon, float tauxDapprentissage)
   {
   //    for (int episode = 0; episode < nbEpisode; episode++)
   //    {
   //       int x = 0, y = 0;
//
   //       
   //       int actionInit = EpsilonGreedy(mapState,x,y,epsilon);
   //       //Initialisation des valeurs de Q(s,a) à 0 
   //       foreach (KeyValuePair<IntList,State> state in mapState)
   //       { 
   //          foreach (var action in state.Value.actions)
   //          {
   //             action.q = 0f;
   //          }
   //       }
   //       
   //       while (true)
   //       {
   //          
   //          // On exectue l'action initiale
   //          State nextState = mapState.Keys.[x][y].actions[actionInit].Act(new IntList(x, y));
   //          float reward = mapState[x][y].reward;
//
   //          // On utilise l'algo d'exploration/exploitation 
   //          int nextAction = EpsilonGreedy(mapState,x,y,epsilon);
   //       
   //          //mise a jour de Q(s,a)
//
   //          float currentQ = mapState[x][y].actions[actionInit].q;
   //          float nextQ = mapState[nextState.x][nextState.y].actions[nextAction].q;
   //          mapState[x][y].actions[actionInit].q = currentQ + tauxDapprentissage * (reward + gamma * nextQ - currentQ);
//
   //          x = nextState.x;
   //          y = nextState.y;
   //          actionInit = nextAction;
   //       
   //          if (mapState[curentState].final)
   //          {
   //             break;
   //          }
   //       }
   //    }
   }

   // Algorithme d'exploration / exploitation
   public static int EpsilonGreedy(Dictionary<IntList, State> mapState,int x , int y , float epsilon)
   {
      return 0;

      // State actuState = mapState[x][y];
      // if (Random.Range(0f, 1f) < epsilon)
      // {
      //    //Exploration
      //    return Random.Range(0, actuState.actions.Count);
      // }
      // else
      // {
      //    // Exploitation
      //    float bestScore = float.MinValue;
      //    int bestAction = 0;

      //    for (int i = 0; i < actuState.actions.Count; i++)
      //    {
      //       IntList nextStateCoordonee = actuState.actions[i].Act(new IntList(x,y));
      //       State nextState = mapState[nextStateCoordonee.x][nextStateCoordonee.y];

      //       if (nextState.score > bestScore)
      //       {
      //          bestScore = nextState.score;
      //          bestAction = i;
      //       }
      //    }
      //    return bestAction;
      // }
   }
}
