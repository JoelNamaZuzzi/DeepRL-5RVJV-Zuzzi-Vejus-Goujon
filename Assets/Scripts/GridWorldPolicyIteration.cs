using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI_Utils;

public class GridWorldPolicyIteration : MonoBehaviour
{
    public MapGenerator _mapGenerator;

    Dictionary<List<int>, State> mapState = new Dictionary<List<int>, State>();

    public GridWorldPolicyIteration()
    {
        List<int> key;

        for(int x = 0; x < _mapGenerator.mapSize.x; x++)
        {
            for(int y = 0; y < _mapGenerator.mapSize.y; y++)
            {
                State newState = new Gridcase();
                key = new List<int>();

                switch(_mapGenerator.GetBlocId(new Vector2Int(x, y)))
                {
                    case MapGenerator.Case.Empty:
                    case MapGenerator.Case.Start:
                       //newState = new Gridcase();
                       break; 

                    case MapGenerator.Case.Goal:
                       newState = new FinalGoal();
                       break; 

                    case MapGenerator.Case.Obstacle:
                       newState = new Frobidden();
                       break; 
                }

                key.Add(x);
                key.Add(y);
                mapState.Add(key, newState);
            }
        }

        key = new List<int>();
        key.Add(0);
        key.Add(0);

        for(int x = 0; x < _mapGenerator.mapSize.x; x++)
        {
            for(int y = 0; y < _mapGenerator.mapSize.y; y++)
            {
                key[0] = x;
                key[1] = y;

                State currentState = mapState[key];

                key[0] = x-1;
                key[1] = y;

                AddAction(key, new Left(), _mapGenerator.mapSize, currentState);

                key[0] = x + 1;
                key[1] = y;

                AddAction(key, new Right(), _mapGenerator.mapSize, currentState);

                key[0] = x;
                key[1] = y - 1;

                AddAction(key, new Down(), _mapGenerator.mapSize, currentState);

                key[0] = x;
                key[1] = y + 1;

                AddAction(key, new Up(), _mapGenerator.mapSize, currentState);
            }
        }
    }

    public void AddAction(List<int> key, Action a, Vector2Int mapSize, State currentState)
    {
        //Check that everything is in bound of the map
        for(int i = 0; i < key.Count; i+=2)
        {
            if(key[i] < 0 || key[i] >= mapSize.x || key[i+1] < 0 || key[i+1] >= mapSize.y)
            {
                return;
            }
        }

        if(mapState[key].GetId() == "Gridcase" || mapState[key].GetId() == "StepGoal")
        {
            currentState.AddAction(a);
        }
    }
}
