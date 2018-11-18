using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class WorldPlayerAddedSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.World};

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