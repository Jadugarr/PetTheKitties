using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderMovementAnimationsSystem : GameReactiveSystem
{
    public RenderMovementAnimationsSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(
            GameMatcher.AllOf(GameMatcher.CharacterVelocity, GameMatcher.CharacterAnimator), GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCharacterAnimator && entity.hasCharacterVelocity;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            gameEntity.characterAnimator.Animator.SetFloat(AnimationTriggerConstants.VelocityX,
                Mathf.Abs(gameEntity.characterVelocity.Velocity.x));
        }
    }
}