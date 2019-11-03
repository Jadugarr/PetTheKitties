using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using Entitas.World;
using UnityEngine;

public class AdjustCharacterMovementToSlopeSystem : GameReactiveSystem
{
    private readonly Vector2 flatGroundNormal = new Vector2(0, 1);

    public AdjustCharacterMovementToSlopeSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterGroundState,
            GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            if (gameEntity.characterGroundState.CharacterGroundState == CharacterGroundState.OnSlope)
            {
                float signedAngleAhead =
                    Mathf.Abs(Vector2.SignedAngle(gameEntity.characterGroundState.GroundNormal, flatGroundNormal));
                Vector2 newVelocity = new Vector2(gameEntity.currentMovementSpeed.CurrentHorizontalMovementSpeed,
                    gameEntity.currentMovementSpeed.CurrentVerticalMovementSpeed).Rotate(signedAngleAhead);
                gameEntity.ReplaceCurrentMovementSpeed(newVelocity.x, newVelocity.y);
            }
        }
    }
}