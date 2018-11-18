using System.Collections.Generic;
using Entitas;
using UnityEngine;

public abstract class GameReactiveSystem : ReactiveSystem<GameEntity>
{
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

        if (IsInValidStates())
        {
            ExecuteSystem(entities);
        }
        else
        {
            Debug.Log("Tried executing system in wrong state: " + this);
        }
    }

    protected abstract bool IsInValidStates();

    protected abstract void ExecuteSystem(List<GameEntity> entities);
}