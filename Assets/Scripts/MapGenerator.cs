using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

using AI_Utils;
using ScriptableObject;
using Utils;

public class MapGenerator : MonoBehaviour
{
    public Vector2Int mapSize;
    private int xVal;
    private int yVal;
    private Case[,] blocId;
    private Transform bloc;
    public List<GameObject> blocsPrefab;


    [Header("Map Loading")]
    public List<Map> maps;
    
    public int usedMapId;
    public bool useMap;

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

    public Case GetBlocId(Vector2Int pos)
    {
        return blocId[pos.x, pos.y];
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
        blocId = new Case[xVal, yVal];
        GameObject cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        cam.transform.position = new Vector3(xVal/2, (xVal+yVal)*0.65f , yVal/2);
    }

    public void GenerateMap(ref List<List<Bloc>> mapList)
    {
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
                mapList.Add(new List<Bloc>());
                for (int y = 0; y < yVal; y++)
                {
                    blocId[x,y] = Case.Empty;
                    mapList[x].Add(null);
                }
            }

            blocId[0,0] = Case.Start;//Début
            blocId[3,3] = Case.Goal;//Fin
            blocId[1,2] = Case.Crate;//Obstacle
            blocId[2,1] = Case.Obstacle;//Obstacle
        }
        else
        {
            for (int x = 0; x < xVal; x++)
            {
                mapList.Add(new List<Bloc>());
                for (int y = 0; y < yVal; y++)
                {
                    try
                    {
                        blocId[x,y] = maps[usedMapId].blocId[x].ligne[y];
                    }
                    catch (Exception e)
                    {
                        blocId[x,y] = Case.Empty;
                    }
                    
                    mapList[x].Add(null);
                }
            }
        }
        
        
        for (int x = 0;x<xVal;x++)
        {
            for (int y = 0;y<yVal;y++)
            {
                Vector3 tilePos = new Vector3(x, 0, y);
                //GameObject newTile = Instantiate(blocsPrefab[(int)blocId[x,y]], tilePos, Quaternion.Euler(Vector3.right * 90));
                //newTile.transform.parent = map;
                if (blocId[x, y] == Case.Crate)
                {
                    mapList[x][y] = new BlocCrate();
                    BlocCrate crate = mapList[x][y] as BlocCrate;
                    crate.blocUnderMeGO = blocsPrefab[(int)Case.Empty];
                    crate.blocUnderMe = new Bloc();
                    crate.blocUnderMe.ID = 0;
                }
                else if(blocId[x,y] == Case.CrateOnTarget)
                {
                    blocs[x][y] = new BlocCrate();
                    BlocCrate crate = blocs[x][y] as BlocCrate;
                    crate.blocUnderMeGO = blocsPrefab[(int)Case.TargetCrate];
                    crate.onTarget = true;
                    
                    crate.blocUnderMe = new Bloc();
                    crate.blocUnderMe.ID = (int)Case.TargetCrate;
                }
                else if(blocId[x, y]== Case.Obstacle)
                {
                    mapList[x][y] = new Bloc();
                    mapList[x][y].wall = true;
                }
                else
                {
                    mapList[x][y] = new Bloc();
                }
                
                if (blocId[x, y] == Case.CrateOnTarget)
                {
                    blocs[x][y].myGo = blocsPrefab[(int)Case.Crate];
                    blocs[x][y].ID = (int)blocId[x, y];
                    blocs[x][y].Spawn();
                    blocs[x][y].myGo.transform.position = tilePos;
                    blocs[x][y].myGo.transform.parent = map;
                    BlocCrate crate = blocs[x][y] as BlocCrate;
                    crate.ChangeColor();
                }

                else
                {
                    blocs[x][y].myGo = blocsPrefab[(int)blocId[x, y]];
                    blocs[x][y].ID = (int)blocId[x, y];
                    blocs[x][y].Spawn();
                    blocs[x][y].myGo.transform.position = tilePos;
                    blocs[x][y].myGo.transform.parent = map;
                }
            }
        }
    }

    public void GenerateStateMap(ref Dictionary<IntList, State> mapState)
    {
        //Generate state map
        IntList key;

        for(int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                State newState = new Gridcase();
                key = new IntList();

                switch(GetBlocId(new Vector2Int(x, y)))
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