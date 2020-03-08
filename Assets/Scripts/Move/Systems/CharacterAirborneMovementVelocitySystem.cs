using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class CharacterAirborneMovementVelocitySystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;

    public CharacterAirborneMovementVelocitySystem(GameContext context) : base(context)
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
            
            if (characterEntity.characterGroundState.CharacterGroundState == CharacterGroundState.Airborne)
            {
                characterEntity.ReplaceCharacterVelocity(new Vector2(
                    characterEntity.currentMovementSpeed.CurrentMovementSpeed,
                    characterEntity.characterVelocity.Velocity.y));
            }
        }
    }
}