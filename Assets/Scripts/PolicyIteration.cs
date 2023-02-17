using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI_Utils;
using Utils;

public class PolicyIteration
{
    static void PolicyEvaluation(ref Dictionary<IntList, State> mapState, IntList mapSize, float theta, float gamma)
    {
        // float delta;

        // do{
        //     delta = 0;

        //     for(int x = 0; x < mapSize.x; x++)
        //     {
        //         for(int x = 0; x < mapSize.x; x++)
        //         {
        //             for(int y = 0; y < mapSize.y; y++)
        //             {
        //                 IntList nextState = mapState[x][y].actions[mapState[x][y].currentAction].Act(new IntList(x, y));

        //                 mapState[x][y].futureScore = mapState[x][y].reward + mapState[nextState.x][nextState.y].score * gamma; 

        //                 delta = Mathf.Max(delta, mapState[x][y].score - mapState[x][y].futureScore);
        //             }
        //         }
        //     }

        //     for(int x = 0; x < mapState.Count; x++)
        //     {
        //         for(int y = 0; y < mapState[x].Count; y++)
        //         {
        //             mapState[x][y].score = mapState[x][y].futureScore;
        //         }
        //     }

        // }while(delta >= theta);
    }

    static bool PolicyImprovements(ref Dictionary<IntList, State> mapState, float gamma)
    {
        return false;

        // bool stable = true;

        // for(int x = 0; x < mapState.Count; x++)
        // {
        //     for(int y = 0; y < mapState[x].Count; y++)
        //     {
        //         bool first = true;
        //         int bestAction = 0;
        //         float bestScore = 0;

        //         for(int a = 0; a < mapState[x][y].actions.Count; a++)
        //         {
        //             IntList nextState = mapState[x][y].actions[a].Act(new IntList(x, y));

        //             float tmp = mapState[x][y].reward + mapState[nextState.x][nextState.y].score * gamma; 

        //             if(tmp > bestScore || first == true)
        //             {
        //                 first = false;
        //                 bestScore = tmp;
        //                 bestAction = a;
        //             }
        //         }

        //         if(bestAction != mapState[x][y].currentAction)
        //         {
        //             stable = false;
        //         }

        //         mapState[x][y].currentAction = bestAction;
        //     }
        // }

        // return stable;
    }

    static void Iteration(ref Dictionary<IntList, State> mapState, float theta, float gamma)
    {
        do{
            //PolicyEvaluation(ref mapState, theta, gamma);

        }while(PolicyImprovements(ref mapState, gamma) == false);
    }
}
