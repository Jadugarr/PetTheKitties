using System.Collections.Generic;
using Entitas;
using Entitas.World;
using UnityEngine;

public class CharacterOnGroundSystem : GameReactiveSystem
{
    public CharacterOnGroundSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterGroundState,
            GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCharacterGroundState && entity.hasPosition &&
               entity.characterGroundState.CharacterGroundState == CharacterGroundState.OnGround;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            Vector3 currentPos = gameEntity.position.position;
            gameEntity.ReplacePosition(new Vector3(currentPos.x,
                currentPos.y - Mathf.Abs(gameEntity.characterGroundState.DistanceToGround), currentPos.z));
        }
    }
}