using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2Int mapSize;
    private int xVal;
    private int yVal;
    public int[,] blocId;
    private Transform bloc;
    public List<GameObject> blocs;
    public void Start()
    {
        xVal = mapSize.x;
        yVal = mapSize.y;
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

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                blocId[x,y] = 0;
            }
        }

        blocId[0,0] = 1;
        blocId[3,3] = 2;
        blocId[1,2] = 3;
        blocId[2,1] = 3;
        
        for (int x = 0;x<mapSize.x;x++)
        {
            for (int y = 0;y<mapSize.y;y++)
            {
                bloc = blocs[blocId[x, y]].transform;
                Vector3 tilePos = new Vector3(x, 0, y);
                Transform newTile = Instantiate(bloc, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.parent = map;
            }
        }
    }
}
