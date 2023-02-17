using System.Collections.Generic;
using AI_Utils;
using UnityEngine;
using Utils;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private AlgoApply selectedAlgorithm;
    [SerializeField] private float awaitTimer;

    private float _timer;
    
    //AI
    private Dictionary<IntList, State> _mapState;
    private IntList currentState;
    // MAP
    private List<List<Bloc>> _mapBlocs;
    private MapGenerator.Case[,] _map;
    enum AlgoApply
    {
        ValueIterator,
        PolicyIteration,
        Montecarlo,
    }
    
    void Awake()
    {
        _mapState = new Dictionary<IntList, State>();
        _mapBlocs = new List<List<Bloc>>();
    }
    
    void Start()
    {
        _map = new MapGenerator.Case[mapGenerator.xVal, mapGenerator.yVal];
        mapGenerator.GenerateMap(ref _mapBlocs, ref _map, out currentState);
        mapGenerator.GenerateStateMap(ref _mapState, ref _map);

        player.Init(new Vector3(mapGenerator.startPosition.x, 1, mapGenerator.startPosition.y));
        
        switch(selectedAlgorithm)
        {
            case AlgoApply.ValueIterator:
                ValueIteration.ValueIterationAlgorithm(ref _mapState, 0.1f, 0.5f);
                break;
            case AlgoApply.PolicyIteration:
                PolicyIteration.Iteration(ref _mapState, 0.01f, 0.5f);
                break;
            case AlgoApply.Montecarlo:
                MonteCarlo.Simulation(ref _mapState, 30, 1000, true, new IntList(currentState), 0.4f, true);
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= awaitTimer)
        {
            _timer = 0.0f;
            ApplyStateMap();
        }
    }

    private void ApplyStateMap()
    {
        State current = _mapState[currentState];

        switch(current.actions[current.currentAction].GetId())
        {
            case "right":
                player.Right();
                break;

            case "left":
                player.Left();
                break;

            case "down":
                player.Down();
                break;
                
            case "up":
                player.Up();
                break;
        }

        currentState = current.actions[current.currentAction].Act(currentState);
        
    }
}
