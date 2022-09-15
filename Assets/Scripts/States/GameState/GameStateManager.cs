using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region Singleton

    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    #endregion

    #region Vars

    private BaseGameState currentGameState;

    #endregion

    #region Unity Funcs.

    private void Start()
    {
        currentGameState = BuildingGameState();
    }

    private void Update()
    {
        currentGameState.UpdateState();
    }
    

    #endregion

    #region State Funcs.

    public void SetState(BaseGameState baseGameState)
    {
        currentGameState.ExitState();
        currentGameState = baseGameState;
        currentGameState.EnterState();
    }
        
    public InGameState PlayGameState()
    {
        return new InGameState();
    }

    public BuildingGameState BuildingGameState()
    {
        return new BuildingGameState();
    }
    
    public ProductGameState ProductGameState()
    {
        return new ProductGameState();
    }

    #endregion
}