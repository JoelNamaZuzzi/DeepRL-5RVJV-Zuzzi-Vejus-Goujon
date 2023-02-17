using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

using AI_Utils;
using Utils;

public class MapGenerator : MonoBehaviour
{
    public Vector2Int mapSize;
    private int xVal;
    private int yVal;
    private Case[,] blocId;
    private Transform bloc;
    public List<GameObject> blocsPrefab;
    public List<List<Bloc>> blocs = new List<List<Bloc>>();
    private Dictionary<IntList, State> mapState = new Dictionary<IntList, State>();

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
    TargetCrate
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
        GenerateMap();
        GameObject cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        cam.transform.position = new Vector3(xVal/2, (xVal+yVal)*0.65f , yVal/2);
    }

    public void GenerateMap()
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
                blocs.Add(new List<Bloc>());
                for (int y = 0; y < yVal; y++)
                {
                    blocId[x,y] = Case.Empty;
                    blocs[x].Add(null);
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
                blocs.Add(new List<Bloc>());
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
                    
                    blocs[x].Add(null);
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
                if ((int)blocId[x, y] == 4)
                {
                    blocs[x][y] = new BlocCrate();
                    BlocCrate crate = blocs[x][y] as BlocCrate;
                    crate.blocUnderMeGO = blocsPrefab[(int)Case.Empty];
                    crate.prefabTarget = blocsPrefab[(int)Case.TargetCrate];
                    crate.blocUnderMe = new Bloc();
                    crate.blocUnderMe.ID = 0;
                }else if((int)blocId[x, y]==3)
                {
                    blocs[x][y] = new Bloc();
                    blocs[x][y].wall = true;
                }
                else
                {
                    blocs[x][y] = new Bloc();
                }
                blocs[x][y].myGo = blocsPrefab[(int)blocId[x, y]];
                blocs[x][y].ID = (int)blocId[x, y];
                blocs[x][y].Spawn();
                blocs[x][y].myGo.transform.position = tilePos;
                blocs[x][y].myGo.transform.parent = map;
            }
        }

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

                AddAction(key, new Left(), mapSize, currentState);

                key[0] = x + 1;
                key[1] = y;

                AddAction(key, new Right(), mapSize, currentState);

                key[0] = x;
                key[1] = y - 1;

                AddAction(key, new Down(), mapSize, currentState);

                key[0] = x;
                key[1] = y + 1;

                AddAction(key, new Up(), mapSize, currentState);
            }
        }
    }

    public void AddAction(IntList key, AI_Utils.Action a, Vector2Int mapSize, State currentState)
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