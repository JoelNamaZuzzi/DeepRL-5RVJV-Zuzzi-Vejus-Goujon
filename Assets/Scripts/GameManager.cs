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
    // MAP
    private List<List<Bloc>> _map;
    
    enum AlgoApply
    {
        ValueIterator,
        PolicyIteration,
    }
    
    void Awake()
    {
        _mapState = new Dictionary<IntList, State>();
        _map = new List<List<Bloc>>();
    }
    
    void Start()
    {
        mapGenerator.GenerateMap(ref _map);
        mapGenerator.GenerateStateMap(ref _mapState);
        player.Init(new Vector3(0, 1, 0));
        
        switch(selectedAlgorithm)
        {
            case AlgoApply.ValueIterator:
                ValueIteration.ValueIterationAlgorithm(ref _mapState,0.5f);
                break;
            case AlgoApply.PolicyIteration:
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
        Vector3 currentPlayerPosition = player.GetPlayerPosition();
        Bloc currentBloc = _map[(int) currentPlayerPosition[0]][(int) currentPlayerPosition[2]];
        float bestScore;
        int xPosition = 0;
        int yPosition = 0;
        
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                int nextX = (int) currentPlayerPosition[0];
                int nextY = (int) currentPlayerPosition[2];

                if (nextX + x >= 0 && nextX + x < mapGenerator.xVal
                    && nextY + y >= 0 && nextY + y < mapGenerator.yVal
                    && x != 0 && y != 0)
                {
                    // if (_map[nextX + x][nextY + y] )
                    // {
                    //     
                    // }
                }
            }
        }
    }
}
