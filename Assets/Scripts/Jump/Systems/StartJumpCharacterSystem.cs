using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class StartJumpCharacterSystem : GameReactiveSystem
{
    private CharacterState[] validJumpStates = new[]
        {CharacterState.Idle, CharacterState.Moving, CharacterState.MoveEnding};

    public StartJumpCharacterSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.JumpCharacter, GroupEvent.Added));
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
        foreach (GameEntity jumpCharacterEntity in entities)
        {
            GameEntity jumpingEntity = _context.GetEntityWithId(jumpCharacterEntity.jumpCharacter.JumpEntityId);
            if (jumpingEntity != null && jumpingEntity.hasCharacterState &&
                HasValidStateToJump(jumpingEntity.characterState.State))
            {
                GameObject characterView = jumpingEntity.view.View;

                if (characterView)
                {
                    if (GroundCheckUtil.CheckIfCharacterOnGround(characterView))
                    {
                        jumpingEntity.ReplaceCharacterVelocity(new Vector2(
                            jumpingEntity.hasCharacterVelocity ? jumpingEntity.characterVelocity.Velocity.x : 0f,
                            jumpingEntity.jumpForce.JumpForce));
                        jumpingEntity.ReplaceCharacterState(CharacterState.Jumping);
                    }
                }
            }
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