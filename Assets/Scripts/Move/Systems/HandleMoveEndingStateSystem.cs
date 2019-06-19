using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class HandleMoveEndingStateSystem : GameReactiveSystem
{
    private IGroup<GameEntity> moveCharacterCommandGroup;
    private IGroup<GameEntity> movingCharacterEntityGroup;
    
    public HandleMoveEndingStateSystem(IContext<GameEntity> context) : base(context)
    {
        moveCharacterCommandGroup = context.GetGroup(GameMatcher.MoveCharacter);
        movingCharacterEntityGroup = context.GetGroup(GameMatcher.CharacterState);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.MoveCharacter, GroupEvent.Removed));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity movingCharacter in movingCharacterEntityGroup)
        {
            if (movingCharacter.characterState.State == CharacterState.Moving)
            {
                bool isStillMoving = false;
                foreach (GameEntity moveCharacterEntity in moveCharacterCommandGroup)
                {
                    if (moveCharacterEntity.moveCharacter.EntityToMoveId == movingCharacter.id.Id)
                    {
                        isStillMoving = true;
                        break;
                    }
                }

                if (!isStillMoving)
                {
                    movingCharacter.ReplaceCharacterState(CharacterState.MoveEnding);
                }
            }
        }
    }
}