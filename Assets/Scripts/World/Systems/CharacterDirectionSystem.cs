using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CharacterDirectionSystem : GameReactiveSystem
{
    public CharacterDirectionSystem(IContext<GameEntity> context) : base(context)
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
        foreach (GameEntity movementEntity in entities)
        {
            GameEntity characterToFlip = _context.GetEntityWithId(movementEntity.moveCharacter.EntityToMoveId);
            GameObject characterView = characterToFlip.view?.View;

            if (characterView != null)
            {
                characterView.transform.localScale =
                    new Vector3(movementEntity.moveCharacter.MoveDirection.x < 0f ? -1 : 1, 1, 1);
            }
        }
    }
}