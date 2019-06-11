using System.Collections.Generic;
using Entitas;
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
        return entity.hasJumpState && entity.jumpState != null && entity.jumpState.JumpState != JumpState.Grounded;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            if (gameEntity.hasCharacterVelocity && gameEntity.characterVelocity != null &&
                Mathf.Abs(gameEntity.characterVelocity.Velocity.y) < 0.05f)
            {
                if (gameEntity.hasView && gameEntity.view != null)
                {
                    if (GroundCheckUtil.CheckIfCharacterOnGround(gameEntity.view.View))
                    {
                        gameEntity.ReplaceJumpState(JumpState.Grounded);
                    }
                }
            }
        }
    }
}