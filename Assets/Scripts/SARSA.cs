using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;

public class SARSA 
{
   static public void SarsaAlgorithm(ref List<List<State>> mapState , float gamma ,int nbEpisode)
   {
      for (int episode = 0; episode < nbEpisode; episode++)
      {
         for (int x = 0; x < mapState.Count; x++)
         {
            for (int y = 0; y < mapState[x].Count; y++)
            {
               mapState[x][y]
            }
         }
      }
   }
}
