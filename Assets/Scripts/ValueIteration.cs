using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_Utils;
using Action = AI_Utils.Action;

public class ValueIteration : MonoBehaviour
{
    public MapGenerator _mapGenerator;
    public List<GameObject> ListeBloc = new List<GameObject>();
    
    public void Start()
    {
        ListeBloc = _mapGenerator.blocs;
    }
    
    // algorithme de Value Iteration
    public List<Action> ValueIterationAlgorithm()
    {
        List<Action> ListeAction = new List<Action>();
        float delta = 0.001f;
        foreach (var bloc in ListeBloc)
        {
            bloc.GetComponent<Bloc>().Vs = 0;
        }

        return ListeAction;
    }
}
