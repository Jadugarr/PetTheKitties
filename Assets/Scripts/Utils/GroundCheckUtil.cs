using Entitas.Extensions;
using UnityEngine;

public static class GroundCheckUtil
{
    private const float kDebugRayDuration = 1f;

    public static bool CheckIfCharacterOnGround(GameObject characterView)
    {
        if (characterView)
        {
            Bounds characterBounds = characterView.GetComponent<CapsuleCollider2D>().bounds;
            float distanceToGround = characterBounds.size.y / 2f;


            Vector2 rayStart = new Vector2(characterView.transform.position.x,
                characterView.transform.position.y - distanceToGround - 0.02f);
            Vector2 rayStartForward = new Vector2(characterView.transform.position.x + characterBounds.size.x / 2f,
                characterView.transform.position.y - distanceToGround - 0.02f);
            Vector2 rayStartBackward = new Vector2(characterView.transform.position.x - characterBounds.size.x / 2f,
                characterView.transform.position.y - distanceToGround - 0.02f);


            Debug.DrawRay(rayStart, Vector2.down * 1, Color.red, kDebugRayDuration);


            Vector2 raycastDirection = Vector2.down;
            Vector2 raycastDirectionForward = new Vector2(0.5f, -0.5f).normalized;
            Vector2 rayCastDirectionBackward = new Vector2(-0.5f, -0.5f).normalized;
            Debug.DrawRay(rayStartForward, raycastDirectionForward, Color.red, kDebugRayDuration);
            Debug.DrawRay(rayStartBackward, rayCastDirectionBackward, Color.red, kDebugRayDuration);


            RaycastHit2D hit = Physics2D.BoxCast(rayStart, new Vector2(characterBounds.size.x - 0.1f, 0.02f), 0f,
                raycastDirection);
            if (hit.collider != null)
            {
                Debug.Log("Tag of hit target: " + hit.collider.gameObject.tag);

                if (hit.collider.gameObject.tag.Equals(Tags.Ground))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static void TestHitAngle(GameObject characterView)
    {
        Bounds characterBounds = characterView.GetComponent<CapsuleCollider2D>().bounds;
        float distanceToGround = characterBounds.size.y / 2f;

        Vector2 rayStartForward = new Vector2(characterView.transform.position.x + characterBounds.size.x / 3f,
            characterView.transform.position.y - distanceToGround + 0.175f);// + distanceToGround);

        Vector2 raycastDirectionForward = new Vector2(0.5f, -0.5f).normalized;

        // angle test
        RaycastHit2D testHit = Physics2D.Raycast(rayStartForward, raycastDirectionForward);
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
        RaycastHit2D testHit = Physics2D.Raycast(rayStartForward, raycastDirectionForward);
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