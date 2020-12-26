using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class GrapplingHookReachedTargetSystem : GameReactiveSystem
{
    public GrapplingHookReachedTargetSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.GrapplingHookLine,
            GameMatcher.GrapplingHookCurrentPoint, GameMatcher.GrapplingHookEndPoint));
    }

    protected override bool Filter(GameEntity entity)
    {
        if (entity != null && entity.hasGrapplingHookCurrentPoint && entity.hasGrapplingHookEndPoint)
        {
            float distance = Vector3.Distance(entity.grapplingHookCurrentPoint.Value, entity.grapplingHookEndPoint.Value);

            return distance <= 0.01f;
        }

        return false;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            entity.DestroyEntity();
        }
    }
}