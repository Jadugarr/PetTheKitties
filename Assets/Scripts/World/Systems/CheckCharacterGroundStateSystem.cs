using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class CheckCharacterGroundStateSystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;

    public CheckCharacterGroundStateSystem(GameContext context) : base(context)
    {
        characterGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterGroundState,
            GameMatcher.CharacterState, GameMatcher.View));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity characterEntity in characterGroup.GetEntities())
        {
            if (characterEntity.characterState.State != CharacterState.Jumping)
            {
                BoxCollider2D characterCollider = characterEntity.view.View.GetComponent<BoxCollider2D>();

                if (GroundCheckUtil.CheckIfCharacterOnSlope(characterCollider, out Vector2 slopeNormal))
                {
                    characterEntity.ReplaceCharacterGroundState(CharacterGroundState.OnSlope,
                        slopeNormal);
                }
                else if (GroundCheckUtil.CheckIfCharacterOnGround(characterEntity.view.View.GetComponent<BoxCollider2D>(), out Vector2 hitNormal))
                {
                    characterEntity.ReplaceCharacterGroundState(CharacterGroundState.OnGround, hitNormal);
                }
                else
                {
                    characterEntity.ReplaceCharacterGroundState(CharacterGroundState.Airborne, hitNormal);
                }
            }
            else
            {
                characterEntity.ReplaceCharacterGroundState(CharacterGroundState.Airborne, Vector2.zero);
            }
        }
    }
}