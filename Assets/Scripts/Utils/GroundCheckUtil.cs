using UnityEngine;

public static class GroundCheckUtil
{
    public static bool CheckIfCharacterOnGround(GameObject characterView)
    {
        if (characterView)
        {
            float distanceToGround = characterView.GetComponent<Collider2D>().bounds.extents.y;
            Vector2 rayStart = new Vector2(characterView.transform.position.x,
                characterView.transform.position.y - distanceToGround - 0.01f);
            Debug.DrawRay(rayStart, Vector2.down * 0.01f, Color.red, 3f);
            RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.01f);
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
}