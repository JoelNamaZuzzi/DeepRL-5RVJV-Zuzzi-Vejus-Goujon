using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI_Utils
{

    public class State
    {
        public List<Action> actions;
        public float score = 0;
        public int currentAction;
        public int futurAction;
        Vector2Int id;

        public State(List<Action> newActions)
        {
            for(int i = 0; i < newActions.Count; i++)
            {
                actions.Add(newActions[i]);
            }

            currentAction = Random.Range(0, actions.Count);
        }
    }

    public abstract class Action
    {
        public abstract string GetId();

        public abstract Vector2Int Act(Vector2Int id);
    }
}