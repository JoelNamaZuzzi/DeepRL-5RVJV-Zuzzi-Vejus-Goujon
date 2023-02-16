using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;
using Action = AI_Utils.Action;

public class ValueIteration 
{
    public MapGenerator _mapGenerator;
    public List<List<Bloc>> ListeBloc = new List<List<Bloc>>();
    
    public void Start()
    {
        ListeBloc = _mapGenerator.blocs;
        Debug.Log(ListeBloc.Count);
        ValueIterationAlgorithm();
    }
    
    // algorithme de Value Iteration
    public List<Action> ValueIterationAlgorithm()
    {
        List<Action> ListeAction = new List<Action>();
        
        for(int i = 0 ; i<ListeBloc.Count ; i++)
        {
            for (int j = 0; j < ListeBloc.Count; j++)
            {
                ListeBloc[i][j].Vs = 0;
            }
        }
        float delta = float.MinValue;
        for (int x = 0; x < mapState.Count; x++)
        {
            for (int y = 0; y < mapState[x].Count; y++)
            {
                mapState[x][y].score = 0;
            }
        }
        //On boucle pour minimiser la valeur de Delta   
        do
        {
            for(int i = 0 ; i<ListeBloc.Count ; i++)
            {
                for (int j = 0; j < ListeBloc.Count; j++)
                {
                    ListeBloc[i][j].Vs = 0;
                }
            }
            for(int i = 0 ; i< ListeBloc.Count -1; i++)
            {
                for (int j = 0; j < ListeBloc.Count; j++)
                {
                    if (i + 1 > ListeBloc.Count-1 || j+1 > ListeBloc.Count-1)
                    {
                        continue;
                    }
                    Bloc BlockActuel = ListeBloc[i][j];
                    float tmp = BlockActuel.Vs;
                    Bloc BlockSuivant = ListeBloc[i][j+1];
                    BlockActuel.Vs = BlockActuel.reward + (0.5f * BlockSuivant.Vs);
                    delta = Mathf.Max(delta, tmp - BlockActuel.Vs);
                }
            }
        } while (delta <0);

        print("Delta = " +delta);
        return ListeAction;
    }
}