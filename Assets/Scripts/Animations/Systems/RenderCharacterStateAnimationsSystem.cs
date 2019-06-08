using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderCharacterStateAnimationsSystem : GameReactiveSystem
{
    public RenderCharacterStateAnimationsSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(
            GameMatcher.AllOf(GameMatcher.CharacterAnimator, GameMatcher.CharacterState), GroupEvent.Added));
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
        foreach (GameEntity gameEntity in entities)
        {
            gameEntity.characterAnimator.Animator.SetInteger(AnimationTriggerConstants.CharacterState,
                (int) gameEntity.characterState.State);
        }
    }
}