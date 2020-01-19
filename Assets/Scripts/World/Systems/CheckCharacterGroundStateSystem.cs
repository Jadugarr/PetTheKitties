using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class CheckCharacterGroundStateSystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;

    private struct CharacterGroundStateData
    {
        public CharacterGroundState CharacterGroundState;
        public Vector2 GroundNormal;
        public float DistanceToGround;
    }

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
            CharacterGroundStateData characterGroundStateData;
            if (characterEntity.characterState.State != CharacterState.Jumping)
            {
                BoxCollider2D characterCollider = characterEntity.view.View.GetComponent<BoxCollider2D>();

                if (GroundCheckUtil.CheckIfCharacterOnSlope(characterCollider, out Vector2 slopeNormal))
                {
                    characterGroundStateData.GroundNormal = slopeNormal;
                    characterGroundStateData.CharacterGroundState = CharacterGroundState.OnSlope;
                    characterGroundStateData.DistanceToGround = 0;
                }
                else if (GroundCheckUtil.CheckIfCharacterOnGround(
                    characterEntity.view.View.GetComponent<BoxCollider2D>(), out Vector2 hitNormal,
                    out float distanceToGround))
                {
                    characterGroundStateData.GroundNormal = hitNormal;
                    characterGroundStateData.CharacterGroundState = CharacterGroundState.OnGround;
                    characterGroundStateData.DistanceToGround = distanceToGround;
                }
                else
                {
                    characterGroundStateData.GroundNormal = hitNormal;
                    characterGroundStateData.CharacterGroundState = CharacterGroundState.Airborne;
                    characterGroundStateData.DistanceToGround = 0;
                }
            }
            else
            {
                characterGroundStateData.GroundNormal = Vector2.zero;
                characterGroundStateData.CharacterGroundState = CharacterGroundState.Airborne;
                characterGroundStateData.DistanceToGround = 0;
            }

            if (characterGroundStateData.CharacterGroundState !=
                characterEntity.characterGroundState.CharacterGroundState)
            {
                characterEntity.ReplaceCharacterGroundState(characterGroundStateData.CharacterGroundState,
                    characterGroundStateData.GroundNormal, characterGroundStateData.DistanceToGround);
            }
        }
    }
}