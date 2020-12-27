using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class GrapplingHookHitTargetSystem : GameReactiveSystem
{
    public GrapplingHookHitTargetSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.GrapplingHookStartingPoint,
            GameMatcher.GrapplingHookCurrentPoint, GameMatcher.GrapplingHookUserId));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity != null && entity.hasGrapplingHookStartingPoint && entity.hasGrapplingHookCurrentPoint && entity.hasGrapplingHookUserId;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity grapplingHookEntity in entities)
        {
            Vector2 startPos = grapplingHookEntity.grapplingHookStartingPoint.Value;
            Vector2 currentPos = grapplingHookEntity.grapplingHookCurrentPoint.Value;

            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.SetLayerMask(LayerMask.GetMask(Tags.Ground));
            List<RaycastHit2D> results = new List<RaycastHit2D>();
            Vector2 direction = (currentPos - startPos).normalized;

            Physics2D.Raycast(startPos, direction, contactFilter2D, results,
                Vector2.Distance(startPos, currentPos));

            if (results.Count > 0)
            {
                GameEntity userEntity = _context.GetEntityWithId(grapplingHookEntity.grapplingHookUserId.Value);
                userEntity.ReplaceCharacterState(CharacterState.Grappled);
                grapplingHookEntity.isGrapplingHookHitTarget = true;

                /*RaycastHit2D hit = results[0];
                Vector2 characterBounds = userEntity.collider.Collider.size;
                float newX;
                float newY;

                if (direction.x >= 0)
                {
                    newX = hit.point.x - characterBounds.y / 2f;
                }
                else
                {
                    newX = hit.point.x + characterBounds.y / 2f;
                }

                if (direction.y >= 0)
                {
                    newY = hit.point.y - characterBounds.x / 2f;
                }
                else
                {
                    newY = hit.point.y + characterBounds.x / 2f;
                }

                Vector2 newPosition = new Vector2(newX, newY);
                userEntity.ReplacePosition(newPosition);
                
                userEntity.RemoveUsedGrapplingHookId();
                userEntity.isUseGrapplingHook = false;
                
                grapplingHookEntity.DestroyEntity();*/
            }
        }
    }
}