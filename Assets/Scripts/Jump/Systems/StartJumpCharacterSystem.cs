using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class StartJumpCharacterSystem : GameExecuteSystem
{
    private CharacterState[] validJumpStates = new[]
        {CharacterState.Idle, CharacterState.Moving, CharacterState.MoveEnding};

    private readonly IGroup<GameEntity> jumpGroup;

    public StartJumpCharacterSystem(GameContext context) : base(context)
    {
        jumpGroup = context.GetGroup(GameMatcher.JumpCharacter);
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity jumpCharacterEntity in jumpGroup.GetEntities())
        {
            if (jumpCharacterEntity.hasCharacterState
                && HasValidStateToJump(jumpCharacterEntity.characterState.State)
                && jumpCharacterEntity.hasView
                && jumpCharacterEntity.hasCharacterGroundState
                && jumpCharacterEntity.characterGroundState.CharacterGroundState != CharacterGroundState.Airborne)
            {
                jumpCharacterEntity.ReplaceCharacterVelocity(new Vector2(
                    jumpCharacterEntity.characterVelocity.Velocity.x,
                    jumpCharacterEntity.jumpForce.JumpForce));
                jumpCharacterEntity.ReplaceCharacterState(CharacterState.Jumping);
                jumpCharacterEntity.ReplaceCharacterGroundState(CharacterGroundState.Airborne, Vector2.zero, 0);
            }
            
            jumpCharacterEntity.RemoveJumpCharacter();
        }
    }

    private bool HasValidStateToJump(CharacterState currentState)
    {
        foreach (CharacterState jumpState in validJumpStates)
        {
            if (jumpState == currentState)
            {
                return true;
            }
        }

        return false;
    }
}