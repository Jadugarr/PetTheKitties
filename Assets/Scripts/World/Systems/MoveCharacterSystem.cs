using System.Collections.Generic;
using Entitas;
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

            if (entityToMove.hasPosition && entityToMove.hasMovementSpeed)
            {
                float newVelocityX = entityToMove.characterVelocity.Velocity.x +
                                     (entityToMove.acceleration.Acceleration *
                                      movementEntity.moveCharacter.MoveDirection.normalized.x * Time.deltaTime);
                if (Mathf.Abs(newVelocityX) < entityToMove.movementSpeed.MovementSpeedValue)
                {
                    entityToMove.characterVelocity.Velocity.x = newVelocityX;
                }
                else
                {
                    entityToMove.characterVelocity.Velocity.x =
                        entityToMove.movementSpeed.MovementSpeedValue *
                        movementEntity.moveCharacter.MoveDirection.normalized.x;
                }
            }
        }
    }
}