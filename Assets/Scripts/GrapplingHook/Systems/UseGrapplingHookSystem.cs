using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UseGrapplingHookSystem : GameReactiveSystem
{
    private IGroup<GameEntity> _reticleGroup;
    
    public UseGrapplingHookSystem(IContext<GameEntity> context) : base(context)
    {
        _reticleGroup = context.GetGroup(GameMatcher.GrapplingHookReticle);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.UseGrapplingHook, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return _reticleGroup != null && _reticleGroup.count > 0 && entity != null && entity.hasPosition;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameEntity reticleEntity = _reticleGroup.GetSingleEntity();
        
        foreach (GameEntity entity in entities)
        {
            Vector2 entityPosition = entity.position.position;
            Vector2 reticlePosition = reticleEntity.position.position;
            
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.SetLayerMask(LayerMask.GetMask(Tags.Ground));
            List<RaycastHit2D> results = new List<RaycastHit2D>();
            Vector2 direction = (reticlePosition - entityPosition).normalized;
            
            Physics2D.Raycast(entityPosition, direction, contactFilter2D, results, Vector2.Distance(entityPosition, reticlePosition));

            if (results.Count > 0)
            {
                RaycastHit2D hit = results[0];

                Vector2 characterBounds = entity.collider.Collider.size;
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
                entity.ReplacePosition(newPosition);
            }

            entity.isUseGrapplingHook = false;
        }
    }
}