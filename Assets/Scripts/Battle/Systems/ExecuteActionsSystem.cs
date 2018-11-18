using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ExecuteActionsSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Waiting};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public ExecuteActionsSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ExecutionTime, GameMatcher.BattleAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.battleAction.ActionAtbType == ActionATBType.Acting && entity.executionTime.RemainingTime <= 0f;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.ExecuteAction);
    }
}