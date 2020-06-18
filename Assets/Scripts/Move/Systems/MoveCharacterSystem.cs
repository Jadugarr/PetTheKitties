using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class MoveCharacterSystem : GameReactiveSystem
{
    public MoveCharacterSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.MoveCharacter);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition && entity.hasMovementSpeed;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        for (var i = 0; i < entities.Count; i++)
        {
            GameEntity movementEntity = entities[i];
            float newMovementSpeed = Mathf.Clamp(
                movementEntity.currentMovementSpeed.CurrentMovementSpeed +
                (movementEntity.acceleration.Acceleration *
                 movementEntity.moveCharacter.MoveDirection.normalized.x *
                 Time.deltaTime),
                -movementEntity.movementSpeed.MovementSpeedValue, movementEntity.movementSpeed.MovementSpeedValue);

            movementEntity.ReplaceCurrentMovementSpeed(newMovementSpeed);
            movementEntity.RemoveMoveCharacter();
        }
    }
}