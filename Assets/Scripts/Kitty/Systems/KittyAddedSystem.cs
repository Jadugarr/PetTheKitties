using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class KittyAddedSystem : GameReactiveSystem
{
    public KittyAddedSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Kitty, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isKitty;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        AssetReference kittyReference = GameConfigurations.AssetReferenceConfiguration.KittyReference;
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(Tags.KittySpawnPoint);

        for (var i = 0; i < entities.Count; i++)
        {
            GameEntity gameEntity = entities[i];
            Transform spawnPointTransform = spawnPoints[i].transform;
            Addressables.InstantiateAsync(kittyReference, spawnPointTransform.position,
                spawnPointTransform.rotation).Completed += handle =>
            {
                GameObject kittyView = handle.Result;
                kittyView.Link(gameEntity);
                gameEntity.AddView(kittyView);
                gameEntity.AddPosition(kittyView.transform.position);
                gameEntity.AddCharacterVelocity(Vector2.zero);
                Animator kittyAnimator = kittyView.GetComponentInChildren<Animator>();
                if (kittyAnimator)
                {
                    gameEntity.AddCharacterAnimator(kittyAnimator);
                }

                Rigidbody2D rigidbody2D = kittyView.GetComponent<Rigidbody2D>();
                if (rigidbody2D)
                {
                    gameEntity.AddRigidbody(rigidbody2D);
                }

                BoxCollider2D collider = kittyView.GetComponent<BoxCollider2D>();
                if (collider)
                {
                    gameEntity.AddCollider(collider);
                }
            };
        }
    }
}