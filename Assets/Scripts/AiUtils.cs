using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI_Utils
{

    public abstract class State
    {
        public List<Action> actions = new List<Action>();
        public float score = 0;
        public float futureScore = 0;
        public int currentAction;
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

    public class Gridcase: State
    {
        public override string GetId()
        {
            return "Gridcase";
        }

        public Gridcase()
        {
            actions.Add(new Right());
            actions.Add(new Left());
            actions.Add(new Down());
            actions.Add(new Up());

            for(int i = 0; i < 4; i++)
            {
                totalScore.Add(0);
                timePlayed.Add(0);
                visited.Add(0);
            }

            currentAction = Random.Range(0, actions.Count);
        }
    }

    public class StepGoal: Gridcase
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
    
    public class StartCase: Gridcase
    {
        public StartCase()
        {
            start = true;
            reward = 1;
        }
    }

    public class FinalGoal: Gridcase
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

    public class Frobidden: Gridcase
    {
        public override string GetId()
        {
            return "Frobidden";
        }

        public Frobidden()
        {
            reward = -100;
            final = true;
        }
    }


    public abstract class Action
    {
        public abstract string GetId();

        public abstract Vector2Int Act(Vector2Int id);
        
        //For SARSA

        public float q;
    }

    public class Right: Action
    {
        public override string GetId()
        {
            return "right";
        }

        public override Vector2Int Act(Vector2Int id)
        {
            id.x++;
            return id;
        }
    }

    public class Left: Action
    {
        public override string GetId()
        {
            return "left";
        }

        public override Vector2Int Act(Vector2Int id)
        {
            id.x--;
            return id;
        }
    }

    public class Up: Action
    {
        public override string GetId()
        {
            return "up";
        }

        public override Vector2Int Act(Vector2Int id)
        {
            id.y++;
            return id;
        }
    }

    public class Down: Action
    {
        public override string GetId()
        {
            return "down";
        }

        public override Vector2Int Act(Vector2Int id)
        {
            id.y--;
            return id;
        }
    }

}