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
                if (Mathf.Abs(gameEntity.characterVelocity.Velocity.x) <= GameConfigurations.MovementConstantsConfiguration.MovementEndThresholdX)
                {
                    gameEntity.ReplaceCharacterVelocity(new Vector2(0f, gameEntity.characterVelocity.Velocity.y));
                }
                else
                {
                    int velocityDirectionFactor = gameEntity.characterVelocity.Velocity.x >= 0 ? 1 : -1;

                    gameEntity.ReplaceCharacterVelocity(new Vector2(
                        (Mathf.Max(Mathf.Abs(gameEntity.characterVelocity.Velocity.x) - friction * Time.deltaTime, 0)) *
                        velocityDirectionFactor, gameEntity.characterVelocity.Velocity.y));
                }
            }
        }
    }
}