using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Vector2Int mapSize;
    private int xVal;
    private int yVal;
    private Case[,] blocId;
    private Transform bloc;
    public List<GameObject> blocsPrefab;
    public List<List<Bloc>> blocs = new List<List<Bloc>>();

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
        xVal = mapSize.x;
        yVal = mapSize.y;
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

        for (int x = 0; x < mapSize.x; x++)
        {
            blocs.Add(new List<Bloc>());
            for (int y = 0; y < mapSize.y; y++)
            {
                blocId[x,y] = Case.Empty;
                blocs[x].Add(null);
            }
        }

        blocId[0,0] = Case.Start;//Début
        blocId[3,3] = Case.Goal;//Fin
        blocId[1,2] = Case.Crate;//Obstacle
        blocId[2,1] = Case.Obstacle;//Obstacle
        
        for (int x = 0;x<mapSize.x;x++)
        {
            for (int y = 0;y<mapSize.y;y++)
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
    }
}