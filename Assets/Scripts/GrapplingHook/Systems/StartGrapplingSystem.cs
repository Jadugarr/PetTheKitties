using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StartGrapplingSystem : GameReactiveSystem
{
    private IGroup<GameEntity> _playerGroup;
    
    public StartGrapplingSystem(IContext<GameEntity> context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.Player);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CharacterState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.characterState.State == CharacterState.Grappling && entity.isPlayer;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        // show grappling hook marker
        if (_playerGroup.count > 0)
        {
            GameEntity playerEntity = _playerGroup.GetSingleEntity();
            AssetReference grapplingHookReticle = GameConfigurations.AssetReferenceConfiguration.GrapplingHookReticle;
            
            Vector3 playerPosition = playerEntity.view.View.transform.position;
            Vector3 reticlePosition = new Vector3(playerPosition.x, playerPosition.y + 2f, playerPosition.z);
            grapplingHookReticle.InstantiateAsync(reticlePosition, Quaternion.identity).Completed += handle =>
            {
                GameObject spawnedReticle = handle.Result;

                if (playerEntity != null && playerEntity.isPlayer && playerEntity.hasView)
                {
                    GameEntity reticleEntity = _context.CreateEntity();
                    reticleEntity.isGrapplingHookReticle = true;
                    reticleEntity.AddView(spawnedReticle);
                    spawnedReticle.Link(reticleEntity);
                }
            };
        }
    }
}