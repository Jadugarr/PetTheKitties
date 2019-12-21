using Entitas.Extensions;
using UnityEngine;

public static class GroundCheckUtil
{
    private const float kDebugRayDuration = 1f;
    private static readonly Vector2 flatGroundNormal = new Vector2(0, 1);

    public static bool CheckIfCharacterOnGround(BoxCollider2D characterCollider2D, out Vector2 hitNormal)
    {
        if (characterCollider2D)
        {
            Bounds characterBounds = characterCollider2D.bounds;

            Vector2 rayStart = new Vector2(characterBounds.min.x + characterBounds.size.x / 2, characterBounds.min.y);

            Debug.DrawRay(rayStart, Vector2.down * 0.01f, Color.red, kDebugRayDuration);

            RaycastHit2D[] castResults = new RaycastHit2D[1];
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.SetLayerMask(LayerMask.GetMask(Tags.Ground));
            if (characterCollider2D.Cast(Vector2.down, contactFilter2D, castResults, 0.01f) > 0)
            {
                Debug.Log("Hit ground");
                hitNormal = castResults[0].normal;
                return true;
            }
        }

        hitNormal = Vector2.zero;
        return false;
    }

    public static bool CheckIfCharacterOnSlope(BoxCollider2D characterCollider2D, out Vector2 hitNormal)
    {
        if (characterCollider2D != null)
        {
            Bounds characterBounds = characterCollider2D.bounds;
            Vector2 forwardCastPointOfOrigin = new Vector2(characterBounds.max.x, characterBounds.min.y);

            Debug.DrawRay(forwardCastPointOfOrigin, Vector2.down * 0.03f, Color.green, kDebugRayDuration);
            Debug.DrawRay(forwardCastPointOfOrigin, Vector2.right * 0.03f, Color.green, kDebugRayDuration);

            Debug.DrawRay(characterBounds.min, Vector2.down * 0.03f, Color.green, kDebugRayDuration);
            Debug.DrawRay(characterBounds.min, Vector2.left * 0.03f, Color.green, kDebugRayDuration);

            // Check ahead
            RaycastHit2D hit = Physics2D.Raycast(forwardCastPointOfOrigin, Vector2.right, 0.03f,
                LayerMask.GetMask(Tags.Ground));

            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return true;
            }

            // Check ahead and down
            hit = Physics2D.Raycast(forwardCastPointOfOrigin, Vector2.down, 0.03f,
                LayerMask.GetMask(Tags.Ground));
            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return true;
            }

            // Check back
            hit = Physics2D.Raycast(characterBounds.min, Vector2.left, 0.03f,
                LayerMask.GetMask(Tags.Ground));
            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return true;
            }

            // Check back and down
            hit = Physics2D.Raycast(characterBounds.min, Vector2.down, 0.03f,
                LayerMask.GetMask(Tags.Ground));
            if (IsSlope(hit))
            {
                hitNormal = hit.normal;
                return true;
            }
        }

        hitNormal = Vector2.zero;
        return false;
    }

    public static Vector2 GetGroundNormalAtPoint(Vector2 startPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.down, 1f,
            LayerMask.GetMask("Ground"));
//        Debug.DrawRay(startPoint, Vector2.down, Color.green, 1f);

        if (hit.collider != null)
        {
            return hit.normal;
        }

        return Vector2.zero;
    }

    public static float GetMovementAngle(Vector2 movementDirection, Vector2 hitNormal)
    {
        if (hitNormal != Vector2.zero)
        {
            float hitAngle = Vector2.Angle(movementDirection, hitNormal) - 90f;
            return hitAngle;
        }

        return 0;
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
            Debug.Log("Hit normal: " + testHit.normal);
            // test movement vector adjustment
            Vector2 testMovementVector = new Vector2(5f, 0f);
            Vector2 rotatedVector = testMovementVector.Rotate(hitAngle);
            Debug.DrawRay(rayStartForward, rotatedVector, Color.green, kDebugRayDuration);
            Debug.Log("Raycast angle: " + hitAngle);
        }
    }

    public static void TestHitAngleFromMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("Mouse position: " + mousePosition);

        Vector2 rayStartForward = new Vector2(mousePosition.x, mousePosition.y);

        Vector2 raycastDirectionForward = new Vector2(0.5f, -0.5f).normalized;

        // angle test
        RaycastHit2D testHit =
            Physics2D.Raycast(rayStartForward, raycastDirectionForward, 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rayStartForward, raycastDirectionForward, Color.red, kDebugRayDuration);
        if (testHit.collider != null)
        {
            float hitAngle = Vector2.Angle(raycastDirectionForward, testHit.normal) - 135f;
            Debug.Log("Hit normal: " + testHit.normal);
            // test movement vector adjustment
            Vector2 testMovementVector = new Vector2(5f, 0f);
            Vector2 rotatedVector = testMovementVector.Rotate(hitAngle);
            Debug.DrawRay(rayStartForward, rotatedVector, Color.green, kDebugRayDuration);
            Debug.Log("Raycast angle: " + hitAngle);
        }
    }

    private static bool IsSlope(RaycastHit2D hit)
    {
        return hit.collider != null && Mathf.Abs(Vector2.SignedAngle(hit.normal, flatGroundNormal)) > 0.01f;
    }
}