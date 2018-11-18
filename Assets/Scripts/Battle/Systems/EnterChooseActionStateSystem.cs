using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using UnityEngine;

public class EnterChooseActionStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.ChooseAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};
    

    public EnterChooseActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ChooseAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.ChooseAction))
        {
            CreateChooseActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ChooseAction));
    }

    private void CreateChooseActionSystems()
    {
    }
}