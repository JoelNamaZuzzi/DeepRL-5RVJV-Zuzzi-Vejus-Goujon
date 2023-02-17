using System.Collections.Generic;
using UnityEngine;
using System;

using AI_Utils;
using Utils;

public class MapGenerator : MonoBehaviour
{
    public Vector2Int mapSize;
    public int xVal;
    public int yVal;
    private Transform bloc;
    public List<GameObject> blocsPrefab;
    
    [Header("Map Loading")]
    public List<Map> maps;
    
    public int usedMapId;
    public bool useMap;

    public Vector2Int startPosition;
    
    public enum Case
    {
        Empty,
        Start,
        Goal,
        Obstacle,
        Crate,
        TargetCrate,
        CrateOnTarget
    }

    public void Awake()
    {
        if (useMap)
        {
            xVal = maps[usedMapId].mapSize.x;
            yVal = maps[usedMapId].mapSize.y;
        }
        else
        {
            xVal = mapSize.x;
            yVal = mapSize.y;
        }
        GameObject cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        cam.transform.position = new Vector3(xVal/2, (xVal+yVal)*0.65f , yVal/2);
    }

    public void GenerateMap(ref List<List<Bloc>> mapBlocs, ref Case[,] mapCase, out IntList startState)
    {
        startState = new IntList();

        string name = "GeneratedMap";
        if (transform.Find(name))
        {
            DestroyImmediate(transform.Find(name).gameObject);
        }

        Transform map = new GameObject(name).transform;
        map.parent = transform;
        
        if (!useMap)
        {
            for (int x = 0; x < xVal; x++)
            {
                mapBlocs.Add(new List<Bloc>());
                for (int y = 0; y < yVal; y++)
                {
                    mapCase[x,y] = Case.Empty;
                    mapBlocs[x].Add(null);
                }
            }
            mapCase[0,0] = Case.Start;//Début
            mapCase[3,3] = Case.Goal;//Fin
            mapCase[1,2] = Case.Obstacle;//Obstacle
            mapCase[2,1] = Case.Obstacle;//Obstacle

            startState.Add(0);
            startState.Add(0);
        }
        else
        {
            startState.Add(0);
            startState.Add(0);

            for (int x = 0; x < xVal; x++)
            {
                mapBlocs.Add(new List<Bloc>());
                for (int y = 0; y < yVal; y++)
                {
                    try
                    {
                        mapCase[x,y] = maps[usedMapId].blocId[x].ligne[y];
                    }
                    catch (Exception e)
                    {
                        mapCase[x,y] = Case.Empty;
                    }
                    
                    mapBlocs[x].Add(null);
                }
            }
        }
        
        
        for (int x = 0;x<xVal;x++)
        {
            for (int y = 0;y<yVal;y++)
            {
                Vector3 tilePos = new Vector3(x, 0, y);
                
                if (mapCase[x, y] == Case.Crate)
                {
                    mapBlocs[x][y] = new BlocCrate();
                    BlocCrate crate = mapBlocs[x][y] as BlocCrate;
                    crate.blocUnderMeGO = blocsPrefab[(int)Case.Empty];
                    crate.blocUnderMe = new Bloc();
                    crate.blocUnderMe.ID = 0;

                    startState.Add(x);
                    startState.Add(y);
                }
                else if(mapCase[x,y] == Case.CrateOnTarget)
                {
                    mapBlocs[x][y] = new BlocCrate();
                    BlocCrate crate = mapBlocs[x][y] as BlocCrate;
                    crate.blocUnderMeGO = blocsPrefab[(int)Case.TargetCrate];
                    crate.onTarget = true;
                    
                    crate.blocUnderMe = new Bloc();
                    crate.blocUnderMe.ID = (int)Case.TargetCrate;

                    startState.Add(x);
                    startState.Add(y);
                }
                else if(mapCase[x, y]== Case.Obstacle)
                {
                    mapBlocs[x][y] = new Bloc();
                    mapBlocs[x][y].wall = true;
                }
                else
                {
                    if(mapCase[x, y] == Case.Start)
                    {
                        startState[0] = x;
                        startState[1] = y;
                        startPosition.x = x;
                        startPosition.y = y;
                    }

                    mapBlocs[x][y] = new Bloc();
                }
                
                if (mapCase[x, y] == Case.CrateOnTarget)
                {
                    mapBlocs[x][y].myGo = blocsPrefab[(int)Case.Crate];
                    mapBlocs[x][y].ID = (int)mapCase[x, y];
                    mapBlocs[x][y].Spawn();
                    mapBlocs[x][y].myGo.transform.position = tilePos;
                    mapBlocs[x][y].myGo.transform.parent = map;
                    BlocCrate crate = mapBlocs[x][y] as BlocCrate;
                    crate.ChangeColor();
                }

                else
                {
                    mapBlocs[x][y].myGo = blocsPrefab[(int)mapCase[x, y]];
                    mapBlocs[x][y].ID = (int)mapCase[x, y];
                    mapBlocs[x][y].Spawn();
                    mapBlocs[x][y].myGo.transform.position = tilePos;
                    mapBlocs[x][y].myGo.transform.parent = map;
                }
            }
        }
    }

    public void GenerateStateMap(ref Dictionary<IntList, State> mapState, ref Case[,] mapCase)
    {
        //Generate state map
        IntList key;

        for(int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                State newState = new Gridcase();
                key = new IntList();

                switch(mapCase[x, y])
                {
                    case Case.Empty:
                    case Case.Start:
                        //newState = new Gridcase();
                        break; 

                    case Case.Goal:
                        newState = new FinalGoal();
                        break; 

                    case Case.Obstacle:
                        newState = new Frobidden();
                        break; 
                }

                key.Add(x);
                key.Add(y);

                mapState.Add(key, newState);
            }
        }

        key = new IntList();
        key.Add(0);
        key.Add(0);

        for(int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                key[0] = x;
                key[1] = y;

                State currentState = mapState[key];

                key[0] = x-1;
                key[1] = y;

                AddAction(key, new Left(), mapState, mapSize, currentState);

                key[0] = x + 1;
                key[1] = y;

                AddAction(key, new Right(), mapState, mapSize, currentState);

                key[0] = x;
                key[1] = y - 1;

                AddAction(key, new Down(), mapState, mapSize, currentState);

                key[0] = x;
                key[1] = y + 1;

                AddAction(key, new Up(), mapState, mapSize, currentState);
            }
        }
    }

    public void AddAction(IntList key, AI_Utils.Action a,Dictionary<IntList, State> mapState, Vector2Int mapSize, State currentState)
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