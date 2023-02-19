using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

namespace AI_Utils
{

    public abstract class State
    {
        public List<Action> actions = new List<Action>();
        public float score = 0;
        public float futureScore = 0;
        public int currentAction = 0;
        public float reward = 0;
        public bool final = false;
        public bool start = false;

        //For MonteCarlo
        public List<int> visited = new List<int>();//Counts the number of time this state was visited in a episode
        public List<float> totalScore = new List<float>();//Score accumulated by exploitation and exploartion pour chaque action
        public List<float> timePlayed = new List<float>();

        public List<float> vs = new List<float>();//V(s)

        public void AddAction(Action a)
        {
            actions.Add(a);
            totalScore.Add(0);
            timePlayed.Add(0);
            visited.Add(0);
            vs.Add(0);
        }

        public abstract string GetId();
        
        //For SARSA

        public MapGenerator.Case typeBlock; 

    }

    public class StandardState: State
    {
        public override string GetId()
        {
            return "StandardState";
        }

        public StandardState()
        {
            currentAction = Random.Range(0, actions.Count);
        }
    }

    public class StepGoal: StandardState
    {
        public override string GetId()
        {
            return "StepGoal";
        }

        public StepGoal()
        {
            reward = 1;
        }
    }
    
    public class StartCase: StandardState
    {
        public StartCase()
        {
            start = true;
        }
    }

    public class FinalGoal: StandardState
    {
        public override string GetId()
        {
            return "FinalGoal";
        }

        public FinalGoal()
        {
            reward = 10;
            final = true;
        }
    }

    public class Forbidden: StandardState
    {
        public override string GetId()
        {
            return "Forbidden";
        }

        public Forbidden()
        {
            reward = -100;
            final = true;
        }
    }


    public abstract class Action
    {
        public abstract string GetId();

        public abstract IntList Act(in IntList id);
        
        //For SARSA

        public float q = 0;
    }

    public class Right: Action
    {
        public override string GetId()
        {
            return "right";
        }

        public override IntList Act(in IntList id)
        {
            IntList newId = new IntList(id);

            newId[0]++;
            return newId;
        }
    }

    public class Left: Action
    {
        public override string GetId()
        {
            return "left";
        }

        public override IntList Act(in IntList id)
        {
            IntList newId = new IntList(id);

            newId[0]--;
            return newId;
        }
    }

    public class Up: Action
    {
        public override string GetId()
        {
            return "up";
        }

        public override IntList Act(in IntList id)
        {
            IntList newId = new IntList(id);

            newId[1]++;
            return newId;
        }
    }

    public class Down: Action
    {
        public override string GetId()
        {
            return "down";
        }

        public override IntList Act(in IntList id)
        {
            IntList newId = new IntList(id);

            newId[1]--;
            return newId;
        }
    }

}