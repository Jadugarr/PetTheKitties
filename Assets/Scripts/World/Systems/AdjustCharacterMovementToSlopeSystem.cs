using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class AdjustCharacterMovementToSlopeSystem : GameExecuteSystem
{
    private readonly Vector2 flatGroundNormal = new Vector2(0, 1);
    private IGroup<GameEntity> characterGroup;

    public AdjustCharacterMovementToSlopeSystem(GameContext context) : base(context)
    {
        characterGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.MovementSpeed, GameMatcher.CharacterVelocity));
    }

    protected override bool IsInValidState()
    {
        return true;
    }
    
    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in characterGroup.GetEntities())
        {
            if (gameEntity.characterGroundState.CharacterGroundState == CharacterGroundState.OnSlopeAhead)
            {
                float signedAngleAhead =
                    Mathf.Abs(Vector2.SignedAngle(gameEntity.characterGroundState.GroundNormal, flatGroundNormal));
                Vector2 newVelocity = new Vector2(gameEntity.currentMovementSpeed.CurrentMovementSpeed,
                    0).Rotate(signedAngleAhead);
                gameEntity.ReplaceCharacterVelocity(newVelocity);
            }
        }
    }
}