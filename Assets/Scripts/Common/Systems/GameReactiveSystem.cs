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
        if (IsInValidState())
        {
            ExecuteSystem(entities);
        }
        else
        {
            Debug.LogWarning("Tried executing system in wrong state: " + this);
        }
    }

    protected abstract bool IsInValidState();

    protected abstract void ExecuteSystem(List<GameEntity> entities);
}