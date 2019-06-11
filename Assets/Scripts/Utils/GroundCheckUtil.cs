
    using UnityEngine;

    public static class GroundCheckUtil
    {
        public static RaycastHit2D CheckIfCharacterOnGround(GameObject characterView)
        {
            float distanceToGround = characterView.GetComponent<Collider2D>().bounds.extents.y;
            Vector2 rayStart = new Vector2(characterView.transform.position.x,
                characterView.transform.position.y - distanceToGround - 0.01f);
            Debug.DrawRay(rayStart, Vector2.down * 0.01f, Color.red, 3f);
            return Physics2D.Raycast(rayStart, Vector2.down, 0.01f);
        }
    }