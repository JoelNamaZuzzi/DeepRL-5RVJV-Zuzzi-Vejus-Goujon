using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;
using Utils;

public class QLearning  
{
   public static void Qlearning(ref Dictionary<IntList, State> mapState,KeyValuePair<IntList,State> start, float gamma, int nbEpisode, float epsilon,
      float tauxDapprentissage)
   {
       for (int episode = 0; episode < nbEpisode; episode++)
       {
          IntList Xy = start.Key;
          KeyValuePair<IntList, State> curentState = start;

          int actionInit = SARSA.EpsilonGreedy(mapState,Xy,epsilon);
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
             int nextAction = SARSA.EpsilonGreedy(mapState,Xy,epsilon);
          
             //mise a jour de Q(s,a)
      
             float currentQ = curentState.Value.actions[actionInit].q;
             float maxQ = -1;

             foreach (var actionnext in mapState[nextState].actions)
             {
                foreach (var action in mapState[Xy].actions)
                {
                   maxQ = Mathf.Max(maxQ, actionnext.q - action.q);
                }
             }
             mapState[Xy].actions[actionInit].q = currentQ + tauxDapprentissage * (reward + gamma * maxQ);

             Xy = nextState;
             actionInit = nextAction;
          
             if (mapState[Xy].final)
             {
                break;
             }
             iteration++;
          }
       }
   }
}
