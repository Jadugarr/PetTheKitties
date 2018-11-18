using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitBattleWonStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Undefined};
    
    public ExitBattleWonStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.PlayerWon;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        Systems playerWonSystems = GameSystemService.GetSubSystemMapping(SubState.PlayerWon);
        if (playerWonSystems != null)
        {
            GameSystemService.RemoveActiveSystems(playerWonSystems);
        }
    }
}