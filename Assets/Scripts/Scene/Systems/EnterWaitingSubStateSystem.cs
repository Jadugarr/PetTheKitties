using System.Collections.Generic;
using Entitas;

public class EnterWaitingSubStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Waiting};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public EnterWaitingSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.Waiting;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.Waiting))
        {
            CreateWaitingSystems();
        }

        Systems waitSystems = GameSystemService.GetSubSystemMapping(SubState.Waiting);
        GameSystemService.AddActiveSystems(waitSystems);
    }

    private void CreateWaitingSystems()
    {
    }
}