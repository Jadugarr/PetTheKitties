using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class CheckCharacterGroundStateSystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;
    private readonly Vector2 flatGroundNormal = new Vector2(0, 1);

    public CheckCharacterGroundStateSystem(GameContext context) : base(context)
    {
        characterGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterGroundState, GameMatcher.View));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity characterEntity in characterGroup.GetEntities())
        {
            Bounds characterBounds = characterEntity.view.View.GetComponent<BoxCollider2D>().bounds;
            var position = characterEntity.view.View.transform.position;
            Vector2 groundNormalAheadOfCharacter =
                GroundCheckUtil.GetGroundNormalAtPoint(new Vector2(position.x + (characterBounds.size.x / 2f) + 0.1f,
                    position.y));
            Vector2 groundNormalBehindCharacter = GroundCheckUtil.GetGroundNormalAtPoint(new Vector2(
                position.x - (characterBounds.size.x / 2f) - 0.1f,
                position.y));

            float signedAngleAhead = Mathf.Abs(Vector2.SignedAngle(groundNormalAheadOfCharacter, flatGroundNormal));
            float signedAngleBehind = Mathf.Abs(Vector2.SignedAngle(groundNormalBehindCharacter, flatGroundNormal));

            Debug.Log("Signed angle ahead: " + signedAngleAhead);
            Debug.Log("Signed angle behind: " + signedAngleBehind);

            if (groundNormalBehindCharacter != Vector2.zero && signedAngleBehind >= 0.01f || groundNormalAheadOfCharacter != Vector2.zero && signedAngleAhead >= 0.01f)
            {
                characterEntity.ReplaceCharacterGroundState(CharacterGroundState.OnSlope, groundNormalAheadOfCharacter);
            }
            else if (GroundCheckUtil.CheckIfCharacterOnGround(characterEntity.view.View, out Vector2 hitNormal))
            {
                characterEntity.ReplaceCharacterGroundState(CharacterGroundState.OnGround, hitNormal);
            }
            else
            {
                characterEntity.ReplaceCharacterGroundState(CharacterGroundState.Airborne, hitNormal);
            }
        }
    }
}