using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.World;
using UnityEngine;

public class HandleGroundedJumpStateSystem : GameReactiveSystem
{
    public HandleGroundedJumpStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterVelocity, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCharacterState && entity.characterState != null &&
               entity.characterState.State == CharacterState.Falling;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
//            if (gameEntity.hasCharacterVelocity && gameEntity.characterVelocity != null &&
//                (gameEntity.characterVelocity.Velocity.y > 0f ||
//                 Mathf.Abs(gameEntity.characterVelocity.Velocity.y) <
//                 GameConfigurations.MovementConstantsConfiguration.MovementEndThresholdX))
//            {
                if (gameEntity.hasView && gameEntity.view != null && gameEntity.hasCharacterGroundState &&
                    gameEntity.characterGroundState.Value != CharacterGroundState.Airborne)
                {
                    gameEntity.ReplaceCharacterState(CharacterState.Idle);
                }
//            }
        }
    }
}