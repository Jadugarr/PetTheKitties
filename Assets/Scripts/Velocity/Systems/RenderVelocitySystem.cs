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
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(
            Matcher<GameEntity>.AllOf(GameMatcher.CharacterVelocity, GameMatcher.View), GroupEvent.Added));
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
            if (e.hasView)
            {
                Rigidbody2D rigidbody = e.view.View.GetComponent<Rigidbody2D>();

                if (rigidbody != null)
                {
                    rigidbody.velocity = e.characterVelocity.Velocity;
                }
            }
        }
    }
}