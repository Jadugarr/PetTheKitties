using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class ConvertMovementSpeedToVelocitySystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;

    public ConvertMovementSpeedToVelocitySystem(GameContext context) : base(context)
    {
        characterGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.MovementSpeed, GameMatcher.CharacterVelocity));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity characterEntity in characterGroup.GetEntities())
        {
            characterEntity.ReplaceCharacterVelocity(new Vector2(
                characterEntity.currentMovementSpeed.CurrentMovementSpeed,
                characterEntity.characterVelocity.Velocity.y));
        }
    }
}