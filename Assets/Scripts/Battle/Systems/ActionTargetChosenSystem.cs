using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ActionTargetChosenSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.ChooseTarget};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public ActionTargetChosenSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Target, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.FinalizeAction);
    }
}