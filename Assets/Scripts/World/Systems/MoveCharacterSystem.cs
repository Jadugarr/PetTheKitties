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
                Vector2 moveVector = new Vector2((movementEntity.moveCharacter.MoveDirection.normalized *
                                                  entityToMove.movementSpeed.MovementSpeedValue *
                                                  Time.deltaTime).x, 0f);
                entityToMove.ReplacePosition(entityToMove.position.position + (Vector3) moveVector);
            }
        }
    }
}