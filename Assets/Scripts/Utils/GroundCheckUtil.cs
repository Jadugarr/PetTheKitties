using Entitas.Extensions;
using UnityEngine;

public static class GroundCheckUtil
{
    private const float kDebugRayDuration = 1f;

    public static bool CheckIfCharacterOnGround(GameObject characterView, out Vector2 hitNormal)
    {
        if (characterView)
        {
            Bounds characterBounds = characterView.GetComponent<BoxCollider2D>().bounds;
            float characterRotation = characterView.transform.eulerAngles.z;

            Vector2 rayStart = new Vector2(characterBounds.min.x + characterBounds.size.x / 2,
                characterBounds.min.y - 0.02f);

            Debug.DrawRay(rayStart, Vector2.down.Rotate(characterRotation), Color.red, kDebugRayDuration);
            Vector2 raycastDirection = Vector2.down;

            //////////////////// TESTING
            
            
            BoxCollider2D collider = characterView.GetComponent<BoxCollider2D>();
            RaycastHit2D[] castResults = new RaycastHit2D[1];
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.SetLayerMask(LayerMask.GetMask("Ground"));
            if (collider.Cast(Vector2.down.Rotate(characterRotation), contactFilter2D, castResults, 0.2f) > 0)
            {
                Debug.Log("Hit ground");
            }
            
            //////////////////// TESTING END
            
            RaycastHit2D hit = Physics2D.BoxCast(rayStart, new Vector2(characterBounds.size.x - 0.1f, 0.02f), 0f,
                raycastDirection, 0.2f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                hitNormal = hit.normal;
                Debug.Log("Tag of hit target: " + hit.collider.gameObject.tag);

                if (hit.collider.gameObject.tag.Equals(Tags.Ground))
                {
                    return true;
                }
            }
        }

        hitNormal = Vector2.zero;
        return false;
    }

    public static Vector2 GetGroundNormalAtPoint(Vector2 startPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.down, 1f,
            LayerMask.GetMask("Ground"));
        Debug.DrawRay(startPoint, Vector2.down, Color.green, 1f);

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
}