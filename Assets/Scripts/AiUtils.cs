using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI_Utils
{

    public class State
    {
        public List<Action> actions;
        List<float> score = new List<float>();
    }

    public abstract class Action
    {
        public abstract string GetId();

        public abstract (int, int) Act((int, int) id);
    }
}