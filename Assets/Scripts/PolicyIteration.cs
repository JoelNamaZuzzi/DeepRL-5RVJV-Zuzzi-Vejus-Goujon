using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI_Utils;

public class PolicyIteration
{
    static void PolicyEvaluation(ref List<List<State>> mapState, float theta, float gamma)
    {
        float delta;

        do{
            delta = 0;

            for(int x = 0; x < mapState.Count; x++)
            {
                for(int y = 0; y < mapState[x].Count; y++)
                {
                    Vector2Int nextState = mapState[x][y].actions[mapState[x][y].currentAction].Act(new Vector2Int(x, y));

                    mapState[x][y].futureScore = mapState[x][y].reward + mapState[nextState.x][nextState.y].score * gamma; 

                    Mathf.Max(delta, mapState[x][y].score - mapState[x][y].futureScore);
                }
            }

            for(int x = 0; x < mapState.Count; x++)
            {
                for(int y = 0; y < mapState[x].Count; y++)
                {
                    mapState[x][y].score = mapState[x][y].futureScore;
                }
            }

        }while(delta >= theta);
    }

    static void PolicyImprovements(ref List<List<State>> mapState, float theta, float gamma)
    {
        bool stable;

        do{
            stable = true;

            for(int x = 0; x < mapState.Count; x++)
            {
                for(int y = 0; y < mapState[x].Count; y++)
                {
                    bool first = true;
                    int bestAction = 0;
                    float bestScore = 0;

                    for(int a = 0; a < mapState[x][y].actions.Count; a++)
                    {
                        Vector2Int nextState = mapState[x][y].actions[a].Act(new Vector2Int(x, y));

                        float tmp = mapState[x][y].reward + mapState[nextState.x][nextState.y].score * gamma; 

                        if(tmp > bestScore)
                        {
                            bestScore = tmp;
                            bestAction = a;
                        }
                    }

                    if(bestScore != mapState[x][y].score)
                    {
                        stable = false;
                    }

                    mapState[x][y].currentAction = bestAction;
                }
            }

            if(stable == false)
            {
                PolicyEvaluation(ref mapState, theta, gamma);
            }

        }while(stable == false);
    }
}