using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;
using Action = AI_Utils.Action;
using Utils;

public class ValueIteration 
{
    // algorithme de Value Iteration
    public static void ValueIterationAlgorithm(ref Dictionary<IntList, State> mapState , float gamma)
    {
         float delta = float.MinValue;
         foreach (var kvp in mapState)
         {
             kvp.Value.score = 0;
         }
        
         //On boucle pour minimiser la valeur de Delta   
         do
         {
             foreach (var kvp in mapState)
             {
                 float maxA = -1;
                 int indexActionSelected = -1;
                // Debug.Log("nb Actions possible : "+kvp.Value.actions.Count + "ID Current action : " + kvp.Value.currentAction);
                 
                 if (kvp.Value.actions.Count == 0)
                     continue;
                 
                 IntList nextState = kvp.Value.actions[kvp.Value.currentAction].Act(kvp.Key);
                // Debug.Log("Next stage id " + nextState[0] +" " + nextState[1]);
                 foreach (var actions in kvp.Value.actions)
                 {
                     
                     Debug.Log("Next state : " + nextState );
                     float tmp = kvp.Value.reward + mapState[nextState].score * gamma;
                     if (maxA < tmp)
                     {
                         maxA = tmp;
                         indexActionSelected = kvp.Value.actions.IndexOf(actions);
                     }
                 }
                 kvp.Value.futureScore = maxA;
                 kvp.Value.currentAction = indexActionSelected;
                 delta = Mathf.Max(delta, kvp.Value.score - kvp.Value.futureScore);
             }
         } while (delta <0);
         
         // On créer le chemin avec les meilleurs actions 
         foreach (var kvp in mapState)
         {
             int bestAction = 0;
             float bestScore = 0;

             for(int a = 0; a < kvp.Value.actions.Count; a++)
             {
                 IntList nextState = kvp.Value.actions[a].Act(kvp.Key);

                 float tmp = kvp.Value.reward + mapState[nextState].score * gamma; 

                 if(tmp > bestScore)
                 {
                     bestScore = tmp;
                     bestAction = a;
                 }
             } 
             
             kvp.Value.currentAction = bestAction;
             Debug.Log("best action : " + kvp.Value.actions[bestAction].GetId());
         }
    }
}
