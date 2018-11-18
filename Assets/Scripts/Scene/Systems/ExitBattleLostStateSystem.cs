using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitBattleLostStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};
    
    public ExitBattleLostStateSystem(IContext<GameEntity> context) : base(context)
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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        Systems playerLostSystems = GameSystemService.GetSubSystemMapping(SubState.PlayerLost);
        GameSystemService.RemoveActiveSystems(playerLostSystems);
    }
}