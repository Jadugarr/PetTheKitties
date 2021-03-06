using Entitas.Extensions;
using Entitas.World;
using UnityEngine;

public enum CharacterSlopeState
{
    None = 0,
    SlopeAhead = 1,
    SlopeBehind = -1
}

public static class GroundCheckUtil
{
    private const float kDebugRayDuration = 1f;
    private const float kGroundCheckDistance = 0.07f;
    private static readonly Vector2 flatGroundNormal = new Vector2(0, 1);

    public static bool CheckIfCharacterOnGround(GameEntity characterEntity, out Vector2 hitNormal, out float distanceToGround)
    {
        if (characterEntity.hasCollider && characterEntity.collider.Collider != null)
        {
            BoxCollider2D collider = characterEntity.collider.Collider;
            Bounds characterBounds = collider.bounds;

            Vector2 rayStart = new Vector2(characterBounds.min.x + characterBounds.size.x / 2, characterBounds.min.y);

            Debug.DrawRay(rayStart, Vector2.down * kGroundCheckDistance, Color.red, kDebugRayDuration);

            RaycastHit2D[] castResults = new RaycastHit2D[1];
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.SetLayerMask(LayerMask.GetMask(Tags.Ground));

            CharacterGroundState characterGroundState = characterEntity.characterGroundState.Value;
            if (characterGroundState == CharacterGroundState.OnGround
            || characterGroundState == CharacterGroundState.OnSlopeAhead
            || characterGroundState == CharacterGroundState.OnSlopeBehind)
            {
                if (collider.Cast(Vector2.down, contactFilter2D, castResults, characterEntity.stepSize.StepSize) > 0)
                {
                    hitNormal = castResults[0].normal;
                    distanceToGround = castResults[0].distance;
                    return true;
                }
            }
            else
            {
                if (collider.Cast(Vector2.down, contactFilter2D, castResults, kGroundCheckDistance) > 0)
                {
                    hitNormal = castResults[0].normal;
                    distanceToGround = castResults[0].distance;
                    return true;
                }
            }
        }

        hitNormal = Vector2.zero;
        distanceToGround = 0;
        return false;
    }

    public static CharacterSlopeState CheckIfCharacterOnSlope(GameEntity characterEntity, out Vector2 hitNormal)
    {
        if (characterEntity.hasCollider && characterEntity.collider.Collider != null)
        {
            Bounds characterBounds = characterEntity.collider.Collider.bounds;
            Vector2 forwardCastPointOfOrigin = new Vector2(characterBounds.max.x, characterBounds.min.y + 0.01f);
            Vector2 backwardCastPointOfOrigin = new Vector2(characterBounds.min.x, characterBounds.min.y + 0.01f);

            Debug.DrawRay(forwardCastPointOfOrigin, Vector2.down * 0.04f, Color.green, kDebugRayDuration);
            Debug.DrawRay(forwardCastPointOfOrigin, Vector2.right * 0.04f, Color.green, kDebugRayDuration);

            Debug.DrawRay(backwardCastPointOfOrigin, Vector2.down * 0.04f, Color.green, kDebugRayDuration);
            Debug.DrawRay(backwardCastPointOfOrigin, Vector2.left * 0.04f, Color.green, kDebugRayDuration);

            // Check ahead
            RaycastHit2D hit = Physics2D.Raycast(forwardCastPointOfOrigin, Vector2.right, 0.04f,
                LayerMask.GetMask(Tags.Ground));

            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return CharacterSlopeState.SlopeAhead;
            }

            // Check ahead and down
            hit = Physics2D.Raycast(forwardCastPointOfOrigin, Vector2.down, 0.04f,
                LayerMask.GetMask(Tags.Ground));
            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return CharacterSlopeState.SlopeAhead;
            }

            // Check back
            hit = Physics2D.Raycast(backwardCastPointOfOrigin, Vector2.left, 0.04f,
                LayerMask.GetMask(Tags.Ground));
            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return CharacterSlopeState.SlopeBehind;
            }

            // Check back and down
            hit = Physics2D.Raycast(backwardCastPointOfOrigin, Vector2.down, 0.04f,
                LayerMask.GetMask(Tags.Ground));
            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return CharacterSlopeState.SlopeBehind;
            }
        }

        hitNormal = Vector2.zero;
        return CharacterSlopeState.None;
    }

    public static void TestHitAngle(GameObject characterView)
    {
        Bounds characterBounds = characterView.GetComponent<BoxCollider2D>().bounds;
        float distanceToGround = characterBounds.size.y / 2f;

        Vector2 rayStartForward = new Vector2(characterView.transform.position.x + characterBounds.size.x / 2f,
            characterView.transform.position.y - distanceToGround + 0.2f); // + distanceToGround);

        Vector2 raycastDirectionForward = new Vector2(0.5f, -0.5f).normalized;

        // angle test
        RaycastHit2D testHit =
            Physics2D.Raycast(rayStartForward, raycastDirectionForward, 0.3f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rayStartForward, raycastDirectionForward, Color.red, kDebugRayDuration);
        if (testHit.collider != null)
        {
            float hitAngle = Vector2.Angle(raycastDirectionForward, testHit.normal) - 135f;
            // test movement vector adjustment
            Vector2 testMovementVector = new Vector2(5f, 0f);
            Vector2 rotatedVector = testMovementVector.Rotate(hitAngle);
            Debug.DrawRay(rayStartForward, rotatedVector, Color.green, kDebugRayDuration);
        }
    }

    private static bool IsSlope(RaycastHit2D hit)
    {
        float angle = Mathf.Abs(Vector2.SignedAngle(hit.normal, flatGroundNormal));
        return hit.collider != null && angle > 0.01f && angle <= 45f;
    }
}