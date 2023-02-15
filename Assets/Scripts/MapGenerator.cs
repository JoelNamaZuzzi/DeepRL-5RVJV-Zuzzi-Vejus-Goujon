using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2 mapSize;
    private int xVal;
    private int yVal;
    public int[,] blocId;
    private Transform bloc;

    public List<GameObject> blocs;
    public void Start()
    {
        xVal = (int)mapSize.x;
        yVal = (int)mapSize.y;
        blocId = new int[xVal, yVal];
        
        GenerateMap();
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
        
        for (int x = 0;x<mapSize.x;x++)
        {
            for (int y = 0;y<mapSize.y;y++)
            {
                Vector3 tilePos = new Vector3(x, 0, y);
                
                if (x == 0 && y == 0)
                {
                    bloc = blocs[1].transform;
                    blocId[x,y] = 1;
                }

                else if (x == mapSize.x - 1 && y == mapSize.y - 1)
                {
                    bloc = blocs[2].transform;
                    blocId[x,y] = 2;
                }
                else if (x == y && x != 0 && y != 0)
                {
                    bloc = blocs[3].transform;
                    blocId[x,y] = 3;
                }
                else
                {
                    bloc = blocs[0].transform;
                    blocId[x,y] = 0;
                }
                Transform newTile = Instantiate(bloc, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.parent = map;
            }
        }
    }
}
