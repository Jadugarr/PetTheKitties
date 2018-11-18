using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;

public class EnterFinalizeActionStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.FinalizeAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public EnterFinalizeActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.FinalizeAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.FinalizeAction))
        {
            CreateFinalizeActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.FinalizeAction));
    }

    private void CreateFinalizeActionSystems()
    {
    }
}

public class ExitFinalizeActionStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public ExitFinalizeActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.FinalizeAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (GameSystemService.HasSubSystemMapping(SubState.FinalizeAction))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSubSystemMapping(SubState.FinalizeAction));
        }
    }
}