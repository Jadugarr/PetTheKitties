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
            GameMatcher.CharacterState, GameMatcher.View, GameMatcher.CharacterDirection));
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

            if (characterEntity.characterState.State == CharacterState.Grappling
                && characterEntity.characterGroundState.Value == CharacterGroundState.Planted)
            {
                characterGroundStateData.GroundNormal = Vector2.zero;
                characterGroundStateData.CharacterGroundState = CharacterGroundState.Planted;
                characterGroundStateData.DistanceToGround = 0;
            }
            else if (characterEntity.characterState.State == CharacterState.Grappled)
            {
                characterGroundStateData.GroundNormal = Vector2.zero;
                characterGroundStateData.CharacterGroundState = CharacterGroundState.Grappled;
                characterGroundStateData.DistanceToGround = 0;
            }
            else if (characterEntity.characterState.State == CharacterState.Planted)
            {
                characterGroundStateData.GroundNormal = Vector2.zero;
                characterGroundStateData.CharacterGroundState = CharacterGroundState.Planted;
                characterGroundStateData.DistanceToGround = 0;
            }
            else if (characterEntity.characterState.State != CharacterState.Jumping
                     && characterEntity.characterState.State != CharacterState.JumpEnding
                     && characterEntity.characterState.State != CharacterState.JumpStart)
            {
                CharacterSlopeState characterSlopeState =
                    GroundCheckUtil.CheckIfCharacterOnSlope(characterEntity, out Vector2 slopeNormal);
                if (characterSlopeState != CharacterSlopeState.None
                    && ((slopeNormal.x < 0f && characterSlopeState == CharacterSlopeState.SlopeAhead)
                        || (slopeNormal.x >= 0f && characterSlopeState == CharacterSlopeState.SlopeBehind)))
                {
                    characterGroundStateData.GroundNormal = slopeNormal;
                    if (characterSlopeState == CharacterSlopeState.SlopeAhead)
                    {
                        characterGroundStateData.CharacterGroundState =
                            characterEntity.characterDirection.CharacterDirection == CharacterDirection.Forward
                                ? CharacterGroundState.OnSlopeAhead
                                : CharacterGroundState.OnSlopeBehind;
                    }
                    else if (characterSlopeState == CharacterSlopeState.SlopeBehind)
                    {
                        characterGroundStateData.CharacterGroundState =
                            characterEntity.characterDirection.CharacterDirection == CharacterDirection.Forward
                                ? CharacterGroundState.OnSlopeBehind
                                : CharacterGroundState.OnSlopeAhead;
                    }
                    else
                    {
                        characterGroundStateData.CharacterGroundState = CharacterGroundState.OnSlopeAhead;
                    }

                    characterGroundStateData.DistanceToGround = 0;
                }
                else if (GroundCheckUtil.CheckIfCharacterOnGround(characterEntity, out Vector2 hitNormal,
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
                characterEntity.characterGroundState.Value)
            {
                characterEntity.ReplacePreviousCharacterGroundState(characterEntity.characterGroundState.Value);
                characterEntity.ReplaceCharacterGroundState(characterGroundStateData.CharacterGroundState);
            }

            if (characterGroundStateData.GroundNormal != characterEntity.groundHitNormal.Value)
            {
                characterEntity.ReplaceGroundHitNormal(characterGroundStateData.GroundNormal);
            }

            characterEntity.ReplaceDistanceToGround(characterGroundStateData.DistanceToGround);
            
        }
    }
}