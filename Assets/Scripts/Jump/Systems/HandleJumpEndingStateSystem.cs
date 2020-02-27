using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class HandleJumpEndingStateSystem : GameExecuteSystem
{
    private readonly IGroup<GameEntity> jumpingCharacterGroup;

    public HandleJumpEndingStateSystem(GameContext context) : base(context)
    {
        jumpingCharacterGroup =
            context.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterState, GameMatcher.CharacterVelocity));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in jumpingCharacterGroup)
        {
            if (gameEntity.hasCharacterState
                && gameEntity.characterState != null
                && gameEntity.characterState.State == CharacterState.Jumping
                && !gameEntity.hasJumpCharacter)
            {
                // foreach (GameEntity commandEntity in jumpCommandEntities)
                // {
                //     if (commandEntity.jumpCharacter.JumpEntityId == gameEntity.id.Id)
                //     {
                //         isHoldingJump = true;
                //         break;
                //     }
                // }
                //
                // if (!isHoldingJump)
                // {
                gameEntity.ReplaceCharacterState(CharacterState.JumpEnding);
                // }
            }
        }
    }
}