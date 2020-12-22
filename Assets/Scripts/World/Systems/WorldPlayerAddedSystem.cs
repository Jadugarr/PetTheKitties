using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class WorldPlayerAddedSystem : GameReactiveSystem
{
    public WorldPlayerAddedSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Player, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        AssetReference playerReference = GameConfigurations.AssetReferenceConfiguration.WorldPlayerReference;
        Transform playerSpawnPointTransform = GameObject.FindGameObjectWithTag(Tags.PlayerSpawnPoint).transform;

        foreach (GameEntity gameEntity in entities)
        {
            gameEntity.isLoading = true;
            Addressables.InstantiateAsync(playerReference, playerSpawnPointTransform.position,
                playerSpawnPointTransform.rotation).Completed += handle =>
            {
                GameObject playerView = handle.Result;
                playerView.Link(gameEntity);
                gameEntity.AddView(playerView);
                gameEntity.AddPosition(playerView.transform.position);
                gameEntity.AddCharacterVelocity(Vector2.zero);
                gameEntity.AddGrapplingDistance(5f);
                Animator playerAnimator = playerView.GetComponentInChildren<Animator>();
                if (playerAnimator)
                {
                    gameEntity.AddCharacterAnimator(playerAnimator);
                }

                Rigidbody2D rigidbody2D = playerView.GetComponent<Rigidbody2D>();
                if (rigidbody2D)
                {
                    gameEntity.AddRigidbody(rigidbody2D);
                }

                BoxCollider2D collider = playerView.GetComponent<BoxCollider2D>();
                if (collider)
                {
                    gameEntity.AddCollider(collider);
                }
                
                gameEntity.isLoading = false;
            };
        }
    }
}