using UnityEngine;

public static class GroundCheckUtil
{
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
            Debug.DrawRay(rayStart, Vector2.down * 1, Color.red, 3f);
            Vector2 raycastDirection = Vector2.down;
            Vector2 raycastDirectionForward = new Vector2(0.5f, -0.5f).normalized;
            Vector2 rayCastDirectionBackward = new Vector2(-0.5f, -0.5f).normalized;
            Debug.DrawRay(rayStartForward, raycastDirectionForward, Color.red, 3f);
            Debug.DrawRay(rayStartBackward, rayCastDirectionBackward, Color.red, 3f);
            RaycastHit2D hit = Physics2D.BoxCast(rayStart, new Vector2(characterBounds.size.x - 0.1f, 0.02f), 0f,
                raycastDirection);
            if (hit.collider != null)
            {
                // angle test
                float hitAngle = Vector2.Angle(raycastDirection, hit.normal);
                Debug.Log("Raycast angle: " + hitAngle);
                Debug.Log("Tag of hit target: " + hit.collider.gameObject.tag);

                if (hit.collider.gameObject.tag.Equals(Tags.Ground))
                {
                    return true;
                }
            }
        }

        return false;
    }
}