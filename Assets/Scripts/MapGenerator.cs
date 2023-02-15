using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2 mapSize;

    public void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        string name = "GeneratedMap";
        if (transform.FindChild(name))
        {
            DestroyImmediate(transform.FindChild(name).gameObject);
        }

        Transform map = new GameObject(name).transform;
        map.parent = transform;
        
        for (int x = 0;x<mapSize.x;x++)
        {
            for (int y = 0;y<mapSize.y;y++)
            {
                Vector3 tilePos = new Vector3(x, 0, y);
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.parent = map;
            }
        }
    }
}
