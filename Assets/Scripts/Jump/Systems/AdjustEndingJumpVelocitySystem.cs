using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class AdjustEndingJumpVelocitySystem : GameExecuteSystem
{
    private IGroup<GameEntity> _jumpingCharacterGroup;

    public AdjustEndingJumpVelocitySystem(GameContext context) : base(context)
    {
        _jumpingCharacterGroup =
            context.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterState, GameMatcher.CharacterVelocity));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in _jumpingCharacterGroup)
        {
            if (gameEntity.hasCharacterState && gameEntity.characterState != null &&
                gameEntity.characterState.State == CharacterState.JumpEnding)
            {
                gameEntity.ReplaceCharacterVelocity(new Vector2(gameEntity.characterVelocity.Velocity.x,
                    gameEntity.characterVelocity.Velocity.y - (50f * Time.fixedDeltaTime)));
            }
        }
    }
}