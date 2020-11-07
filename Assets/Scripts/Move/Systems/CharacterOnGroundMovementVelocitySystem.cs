using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class CharacterOnGroundMovementVelocitySystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;

    public CharacterOnGroundMovementVelocitySystem(GameContext context) : base(context)
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
            if (characterEntity.characterGroundState.Value == CharacterGroundState.OnGround &&
                characterEntity.characterState.State != CharacterState.Jumping)
            {
                characterEntity.ReplaceCharacterVelocity(new Vector2(
                    characterEntity.currentMovementSpeed.CurrentMovementSpeed,
                    0));
            }
        }
    }
}