using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

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
        GameObject kittyObject = Resources.Load<GameObject>(WorldAssetTypes.WorldKitty);
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(Tags.KittySpawnPoint);

        for (var i = 0; i < entities.Count; i++)
        {
            GameEntity gameEntity = entities[i];
            Transform spawnPointTransform = spawnPoints[i].transform;
            GameObject kittyView = GameObject.Instantiate(kittyObject, spawnPointTransform.position,
                spawnPointTransform.rotation);
            kittyView.Link(gameEntity);
            gameEntity.AddView(kittyView);
            gameEntity.AddPosition(kittyView.transform.position);
            gameEntity.AddCharacterVelocity(Vector2.zero);
        }
    }
}