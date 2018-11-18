using System.Collections.Generic;
using Entitas;

public class ExitWaitingSubStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Undefined};
    
    public ExitWaitingSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.Waiting;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        Systems waitSystems = GameSystemService.GetSubSystemMapping(SubState.Waiting);
        if (waitSystems != null)
        {
            GameSystemService.RemoveActiveSystems(waitSystems);
        }
    }
}