using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitPlayerLostStateSystem : GameReactiveSystem
{
    public ExitPlayerLostStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.PlayerLost;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        Systems playerLostSystems = GameSystemService.GetSubSystemMapping(SubState.PlayerLost);
        GameSystemService.RemoveActiveSystems(playerLostSystems);
    }
}