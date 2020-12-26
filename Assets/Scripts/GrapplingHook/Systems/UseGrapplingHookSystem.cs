using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
            AssetReference grapplingHookLineReference =
                GameConfigurations.AssetReferenceConfiguration.GrapplingHookLine;

            grapplingHookLineReference.InstantiateAsync(entity.position.position, Quaternion.identity)
                .Completed += handle =>
            {
                GameEntity grapplingHookEntity = _context.CreateEntity();
                grapplingHookEntity.isGrapplingHookLine = true;
                grapplingHookEntity.AddGrapplingHookStartingPoint(entity.position.position);
                grapplingHookEntity.AddGrapplingHookEndPoint(reticleEntity.position.position);
                grapplingHookEntity.AddGrapplingHookCurrentPoint(entity.position.position);
                grapplingHookEntity.AddGrapplingHookSpeed(30f);
                grapplingHookEntity.AddView(handle.Result);
                grapplingHookEntity.AddPosition(handle.Result.transform.position);
                grapplingHookEntity.AddGrapplingHookLineRenderer(handle.Result.GetComponent<LineRenderer>());
                entity.AddUsedGrapplingHookId(grapplingHookEntity.id.Id);
                grapplingHookEntity.AddGrapplingHookUserId(entity.id.Id);
                handle.Result.SetActive(true);
                handle.Result.Link(grapplingHookEntity);
            };
        }
    }
}