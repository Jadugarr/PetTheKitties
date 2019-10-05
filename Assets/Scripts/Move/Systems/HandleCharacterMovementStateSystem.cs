using System.Collections.Generic;
using Configurations;
using Entitas;
using UnityEngine;

public class HandleCharacterMovementStateSystem : GameReactiveSystem
{
    private CharacterState[] validMovementStates = new[] {CharacterState.Moving, CharacterState.MoveEnding};
    private IGroup<GameEntity> characterMoveCommandGroup;

    public HandleCharacterMovementStateSystem(IContext<GameEntity> context) : base(context)
    {
        characterMoveCommandGroup = context.GetGroup(GameMatcher.MoveCharacter);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(
            GameMatcher.AllOf(GameMatcher.CharacterVelocity, GameMatcher.CharacterState), GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCharacterState && entity.hasCharacterVelocity &&
               (entity.characterState.State == CharacterState.Idle
                || HasValidMovementState(entity.characterState.State));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            float velocityX = Mathf.Abs(gameEntity.characterVelocity.Velocity.x);

            if (velocityX == 0f && HasValidMovementState(gameEntity.characterState.State))
            {
                gameEntity.ReplaceCharacterState(CharacterState.Idle);
            }
            else if (velocityX > GameConfigurations.MovementConstantsConfiguration.MovementEndThresholdX && (gameEntity.characterState.State == CharacterState.Idle ||
                                           HasValidMovementState(gameEntity.characterState.State)))
            {
                if (HasMovementCommand(gameEntity.id.Id))
                {
                    gameEntity.ReplaceCharacterState(CharacterState.Moving);
                }
                else
                {
                    gameEntity.ReplaceCharacterState(CharacterState.MoveEnding);
                }
            }
        }
    }

    private bool HasValidMovementState(CharacterState currentState)
    {
        foreach (CharacterState movementState in validMovementStates)
        {
            if (movementState == currentState)
            {
                return true;
            }
        }

        return false;
    }

    private bool HasMovementCommand(int characterId)
    {
        foreach (GameEntity commandEntity in characterMoveCommandGroup)
        {
            if (commandEntity.moveCharacter.EntityToMoveId == characterId)
            {
                return true;
            }
        }

        return false;
    }
}