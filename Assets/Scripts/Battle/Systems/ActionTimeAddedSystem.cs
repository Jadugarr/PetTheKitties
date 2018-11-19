using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ActionTimeAddedSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.FinalizeAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};
    
    public ActionTimeAddedSystem(IContext<GameEntity> context) : base(context)
    {
        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.ExecutionTime, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.Waiting);
    }
}