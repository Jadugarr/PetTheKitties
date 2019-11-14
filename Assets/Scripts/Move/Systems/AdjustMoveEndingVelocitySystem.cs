using Configurations;
using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class AdjustMoveEndingVelocitySystem : GameExecuteSystem
{
    private const float friction = 10f;
    private IGroup<GameEntity> moveEndingCharacterGroup;

    public AdjustMoveEndingVelocitySystem(GameContext context) : base(context)
    {
        moveEndingCharacterGroup = context.GetGroup(GameMatcher.CharacterState);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in moveEndingCharacterGroup)
        {
            if (gameEntity.characterState.State == CharacterState.MoveEnding)
            {
                if (Mathf.Abs(gameEntity.currentMovementSpeed.CurrentMovementSpeed) <=
                    GameConfigurations.MovementConstantsConfiguration.MovementEndThresholdX)
                {
                    gameEntity.ReplaceCurrentMovementSpeed(0f);
                }
                else
                {
                    int velocityDirectionFactor =
                        gameEntity.currentMovementSpeed.CurrentMovementSpeed >= 0 ? 1 : -1;

                    gameEntity.ReplaceCurrentMovementSpeed(
                        (Mathf.Max(
                            Mathf.Abs(gameEntity.currentMovementSpeed.CurrentMovementSpeed) -
                            friction * Time.deltaTime, 0)) *
                        velocityDirectionFactor);
                }
            }
        }
    }
}