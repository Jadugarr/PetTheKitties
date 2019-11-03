using System;
using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class TransformMovementSpeedToVelocitySystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;

    public TransformMovementSpeedToVelocitySystem(GameContext context) : base(context)
    {
        characterGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.CurrentMovementSpeed));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in characterGroup.GetEntities())
        {
            gameEntity.ReplaceCharacterVelocity(new Vector2(
                gameEntity.currentMovementSpeed.CurrentHorizontalMovementSpeed,
                gameEntity.currentMovementSpeed.CurrentVerticalMovementSpeed));
        }
    }
}