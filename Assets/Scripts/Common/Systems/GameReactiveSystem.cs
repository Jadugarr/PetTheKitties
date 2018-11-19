using System.Collections.Generic;
using Entitas;
using UnityEngine;

public abstract class GameReactiveSystem : ReactiveSystem<GameEntity>
{
    protected abstract IList<SubState> ValidSubStates { get; }
    protected abstract IList<GameState> ValidGameStates { get; }

    protected GameContext _context;

    public GameReactiveSystem(IContext<GameEntity> context) : base(context)
    {
        _context = (GameContext) context;
    }

    protected abstract override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context);

    protected abstract override bool Filter(GameEntity entity);

    protected sealed override void Execute(List<GameEntity> entities)
    {
        GameState currentGameState = _context.gameState.CurrentGameState;
        SubState currentSubState = _context.subState.CurrentSubState;

        if ((ValidGameStates.Contains(currentGameState) || ValidGameStates.Contains(GameState.Undefined))
            && (ValidSubStates.Contains(currentSubState) || ValidSubStates.Contains(SubState.Undefined)))
        {
            ExecuteSystem(entities);
        }
        else
        {
            Debug.Log("Tried executing system in wrong state: " + this);
        }
    }

    protected abstract void ExecuteSystem(List<GameEntity> entities);
}