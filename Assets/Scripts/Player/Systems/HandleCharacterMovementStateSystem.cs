using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class HandleCharacterMovementStateSystem : GameReactiveSystem
{
    public HandleCharacterMovementStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(
            GameMatcher.AllOf(GameMatcher.CharacterVelocity, GameMatcher.CharacterState), GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.characterState.State == CharacterState.Idle
            || entity.characterState.State == CharacterState.Moving;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            float velocityX = Mathf.Abs(gameEntity.characterVelocity.Velocity.x);
            
            if (velocityX <= 0.05f && gameEntity.characterState.State == CharacterState.Moving)
            {
                gameEntity.ReplaceCharacterState(CharacterState.Idle);
            }
            else if (velocityX > 0.05f && gameEntity.characterState.State == CharacterState.Idle)
            {
                gameEntity.ReplaceCharacterState(CharacterState.Moving);
            }
        }
    }
}