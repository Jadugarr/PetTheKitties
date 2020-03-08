using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using Entitas.Common;
using UnityEngine;

public class EnterPlayerLostStateSystem : GameReactiveSystem
{
    public EnterPlayerLostStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.PlayerLost;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSystemMapping(GameSystemType.PlayerLost))
        {
            CreatePlayerLostSystems();
        }
        
        Systems playerLostSystems = GameSystemService.GetSystemMapping(GameSystemType.PlayerLost);
        GameSystemService.AddActiveSystems(playerLostSystems);
    }

    private void CreatePlayerLostSystems()
    {
        Systems playerLostSystems = new Feature("PlayerLostSystems")
            .Add(new DisplayBattleLostSystem());
        
        GameSystemService.AddSystemMapping(GameSystemType.PlayerLost, playerLostSystems);
    }
}