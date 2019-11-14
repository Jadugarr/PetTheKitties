using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CharacterScaredSystem : GameReactiveSystem
{
    public CharacterScaredSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Scared);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isScared;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            if (entity.hasCharacterVelocity)
            {
                entity.ReplaceCurrentMovementSpeed(0f);
            }
        }
    }
}