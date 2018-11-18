using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EnterWorldStateSystem : ReactiveSystem<GameEntity>
{
    public EnterWorldStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        
    }
}