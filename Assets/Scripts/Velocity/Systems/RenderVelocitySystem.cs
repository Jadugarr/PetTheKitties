using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderVelocitySystem : GameReactiveSystem
{
    public RenderVelocitySystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(Matcher<GameEntity>.AllOf(GameMatcher.CharacterVelocity, GameMatcher.View));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            Rigidbody2D rigidbody = e.view?.View.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {
                rigidbody.velocity = e.characterVelocity.Velocity;
            }
        }
    }
}