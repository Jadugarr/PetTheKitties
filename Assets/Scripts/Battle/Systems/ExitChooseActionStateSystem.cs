using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using UnityEngine;

public class ExitChooseActionStateSystem : GameReactiveSystem
{
    public ExitChooseActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.ChooseAction;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        UIService.HideWidget(UiAssetTypes.ActionChooser);
    }
}