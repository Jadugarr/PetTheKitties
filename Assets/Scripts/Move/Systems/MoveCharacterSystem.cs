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
        return true;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        for (var i = 0; i < entities.Count; i++)
        {
            GameEntity movementEntity = entities[i];
            GameEntity entityToMove = _context.GetEntityWithId(movementEntity.moveCharacter.EntityToMoveId);

            if (entityToMove != null)
            {
                if (entityToMove.hasPosition && entityToMove.hasMovementSpeed)
                {
                    float newMovementSpeed = Mathf.Clamp(
                        entityToMove.currentMovementSpeed.CurrentHorizontalMovementSpeed +
                        (entityToMove.acceleration.Acceleration *
                         movementEntity.moveCharacter.MoveDirection.normalized.x *
                         Time.deltaTime),
                        -entityToMove.movementSpeed.MovementSpeedValue, entityToMove.movementSpeed.MovementSpeedValue);

                    entityToMove.ReplaceCurrentMovementSpeed(newMovementSpeed,
                        entityToMove.currentMovementSpeed.CurrentVerticalMovementSpeed);
                }
            }
        }
    }
}