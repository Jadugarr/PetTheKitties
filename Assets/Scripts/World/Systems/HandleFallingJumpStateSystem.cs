using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class HandleFallingJumpStateSystem : GameReactiveSystem
{
    public HandleFallingJumpStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterVelocity, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasJumpState && entity.jumpState != null && entity.jumpState.JumpState != JumpState.Falling;
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
                gameEntity.characterVelocity.Velocity.y < 0f)
            {
                gameEntity.ReplaceJumpState(JumpState.Falling);
            }
        }
    }
}