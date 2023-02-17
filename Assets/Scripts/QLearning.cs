using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;
using Utils;

public class QLearning  
{
   public static void Qlearning(ref Dictionary<IntList, State> mapState, float gamma, int nbEpisode, float epsilon,
      float tauxDapprentissage)
   {
      // for (int episode = 0; episode < nbEpisode; episode++)
      // {
      //    // On recupere la zone initial
      //    int x = 0, y = 0;
      //    for (int i = 0; i < mapState.Count; i++)
      //    {
      //       for (int j = 0; j < mapState[i].Count; j++)
      //       {
      //          if (mapState[i][j].start)
      //          {
      //             x = i;
      //             y = j;
      //          }
      //       }
      //    }
      //    int actionInit = SARSA.EpsilonGreedy(mapState,x,y,epsilon);
      //    //Initialisation des valeurs de Q(s,a) à 0 
      //    foreach (var liststate in mapState)
      //    {
      //       foreach (var state in liststate)
      //       {
      //          foreach (var action in state.actions)
      //          {
      //             action.q = 0f;
      //          }
      //       }
      //    }
         
      //    while (true)
      //    {
      //       // On exectue l'action initiale
      //       IntList nextState = mapState[x][y].actions[actionInit].Act(new IntList(x, y));
      //       float reward = mapState[x][y].reward;

      //       // On utilise l'algo d'exploration/exploitation 
      //       int nextAction = SARSA.EpsilonGreedy(mapState,x,y,epsilon);
            
      //       //mise a jour de Q(s,a)
      
      //       float currentQ = mapState[x][y].actions[actionInit].q;
      //       float nextQ = mapState[nextState.x][nextState.y].actions[nextAction].q;
            
      //       float maxQ = -1;

      //       foreach (var actionnext in mapState[nextState.x][nextState.y].actions)
      //       {
      //          foreach (var action in mapState[x][y].actions)
      //          {
      //             maxQ = Mathf.Max(maxQ, actionnext.q - action.q);
      //          }
      //       }
      //       mapState[x][y].actions[actionInit].q = currentQ + tauxDapprentissage * (reward + gamma * maxQ);

      //       x = nextState.x;
      //       y = nextState.y;
      //       actionInit = nextAction;
            
      //       if (mapState[x][y].final)
      //       {
      //          break;
      //       }
      //    }
      // }
   }
}
