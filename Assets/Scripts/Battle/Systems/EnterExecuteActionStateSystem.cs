using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EnterExecuteActionStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.ExecuteAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public EnterExecuteActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ExecuteAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.ExecuteAction))
        {
            Debug.LogError("Didn't create systems for substate: " + SubState.ExecuteAction);
            return;
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ExecuteAction));
    }
}