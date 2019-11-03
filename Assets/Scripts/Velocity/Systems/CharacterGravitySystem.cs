using System;
using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class CharacterGravitySystem : GameExecuteSystem
{
    private const float gravityFactor = 13f;
    private IGroup<GameEntity> characterGroup;

    public CharacterGravitySystem(GameContext context) : base(context)
    {
        characterGroup =
            context.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterGroundState, GameMatcher.CurrentMovementSpeed));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in characterGroup.GetEntities())
        {
            if (gameEntity.characterGroundState.CharacterGroundState == CharacterGroundState.Airborne)
            {
                gameEntity.ReplaceCurrentMovementSpeed(gameEntity.currentMovementSpeed.CurrentHorizontalMovementSpeed,
                    gameEntity.currentMovementSpeed.CurrentVerticalMovementSpeed - gravityFactor * Time.deltaTime);
            }
        }
    }
}