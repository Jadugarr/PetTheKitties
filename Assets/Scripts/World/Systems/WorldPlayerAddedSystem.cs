using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

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
        //TODO: Load player async (maybe with promise?)
        GameObject playerObject = Resources.Load<GameObject>(WorldAssetTypes.WorldPlayer);
        Transform playerSpawnPointTransform = GameObject.FindGameObjectWithTag(Tags.PlayerSpawnPoint).transform;

        foreach (GameEntity gameEntity in entities)
        {
            GameObject playerView = GameObject.Instantiate(playerObject, playerSpawnPointTransform.position,
                playerSpawnPointTransform.rotation);
            playerView.Link(gameEntity);
            gameEntity.AddView(playerView);
            gameEntity.AddPosition(playerView.transform.position);
            gameEntity.AddCharacterVelocity(Vector2.zero);
        }
    }
}