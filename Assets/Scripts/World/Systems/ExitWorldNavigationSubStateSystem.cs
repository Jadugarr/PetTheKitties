using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using UnityEngine;

public class ExitWorldNavigationSubStateSystem : GameReactiveSystem
{
    public ExitWorldNavigationSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.subState.PreviousSubState == SubState.WorldNavigation;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameSystemService.RemoveActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.WorldNavigation));
    }
}