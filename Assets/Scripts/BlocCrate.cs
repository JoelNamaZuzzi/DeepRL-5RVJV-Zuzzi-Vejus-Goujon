using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocCrate : Bloc
{
    //Check if it's possible to move to new position
    //Take the gameObject list of blocs, the position of the crate on the grid
    //And the direction you want to move it (example:(0,1) to move up)
    //Returns a boolean true is possible, false if not
    public bool isPossible(List<List<GameObject>>gridData, Vector2Int pos, Vector2Int dir)
    {
        Vector2Int newPos = pos + dir;
        //Checks if out of bounds
        if (newPos.x < gridData.Count - 1 || newPos.y < gridData[0].Count - 1 || newPos.x>0 || newPos.y>0)
        {
            //Debug.Log("is Not OOB");
            //Checks if new possition is wall or Crate
            if (!gridData[newPos.x][newPos.y].GetComponent<Bloc>().wall && gridData[newPos.x][newPos.y].GetComponent<BlocIdHolder>().ID!=4)
            {
                Debug.Log("is Possible");
                return true;
            }
        }
        Debug.Log("is Not Possible");
        return false;
    }
}
