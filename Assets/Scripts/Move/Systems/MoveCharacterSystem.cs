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
                    float angleToRotate = 0f;
                    if (entityToMove.characterGroundState.GroundNormal != Vector2.zero)
                    {
                        angleToRotate = GroundCheckUtil.GetMovementAngle(movementEntity.moveCharacter.MoveDirection,
                            entityToMove.characterGroundState.GroundNormal);
                    }

                    float newVelocityX = entityToMove.characterVelocity.Velocity.x +
                                         (entityToMove.acceleration.Acceleration *
                                          movementEntity.moveCharacter.MoveDirection.normalized.x * Time.deltaTime);
                    Vector2 velocityVector = new Vector2();

                    if (Mathf.Abs(newVelocityX) < entityToMove.movementSpeed.MovementSpeedValue)
                    {
                        velocityVector.x = newVelocityX;
                    }
                    else
                    {
                        velocityVector.x =
                            entityToMove.movementSpeed.MovementSpeedValue *
                            movementEntity.moveCharacter.MoveDirection.normalized.x;
                    }

                    if (angleToRotate != 0)
                    {
                        velocityVector = velocityVector.Rotate(angleToRotate);
                    }
                    entityToMove.characterVelocity.Velocity.x = velocityVector.x;
                    entityToMove.characterVelocity.Velocity.y = velocityVector.y;
                }
            }
        }
    }
}