using System.Collections.Generic;
using Entitas;
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
            GameObject.Instantiate(playerObject, playerSpawnPointTransform.position,
                playerSpawnPointTransform.rotation);
        }
    }
}