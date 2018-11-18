using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExitExecuteActionStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};
    
    public ExitExecuteActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.ExecuteAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (GameSystemService.HasSubSystemMapping(SubState.ExecuteAction))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ExecuteAction));
        }
    }
}