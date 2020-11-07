using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class HandleFallingStateSystem : GameReactiveSystem
{
    public HandleFallingStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterVelocity, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCharacterState && entity.characterState != null && entity.characterState.State != CharacterState.Falling
            && entity.hasCharacterGroundState && entity.characterGroundState != null && entity.characterGroundState.Value == CharacterGroundState.Airborne;
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
                gameEntity.characterVelocity.Velocity.y < -0.1f)
            {
                gameEntity.ReplaceCharacterState(CharacterState.Falling);
            }
        }
    }
}