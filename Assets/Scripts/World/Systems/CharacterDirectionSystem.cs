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
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CharacterDirection, GameMatcher.View));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.view != null;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity directionEntity in entities)
        {
            GameObject characterView = directionEntity.view?.View;

            if (characterView != null)
            {
                characterView.transform.localScale =
                    new Vector3(
                        directionEntity.characterDirection.CharacterDirection == CharacterDirection.Backward ? -1 : 1,
                        1, 1);
            }
        }
    }
}