using System.Collections.Generic;
using Entitas;
using Entitas.World;
using UnityEngine;

public class SetGravityScaleSystem : GameReactiveSystem
{
    public SetGravityScaleSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterGroundState,
            GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasCharacterGroundState && entity.hasRigidbody;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            if (gameEntity.characterGroundState.Value == CharacterGroundState.OnSlopeAhead
                || gameEntity.characterGroundState.Value == CharacterGroundState.OnSlopeBehind)
            {
                gameEntity.rigidbody.Rigidbody.gravityScale = 0f;
            }
            else
            {
                gameEntity.rigidbody.Rigidbody.gravityScale = 1f;
            }
        }
    }
}