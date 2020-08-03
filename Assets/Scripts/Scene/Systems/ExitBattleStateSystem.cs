using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using Entitas.Extensions;
using Entitas.Unity;

public class ExitBattleStateSystem : GameReactiveSystem
{
    private IGroup<GameEntity> viewGroup;

    public ExitBattleStateSystem(GameContext context) : base(context)
    {
        viewGroup = _context.GetGroup(GameMatcher.View);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState == GameState.Battle;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity entity in viewGroup.GetEntities())
        {
            entity.view.View.Unlink();
        }

        UIService.HideWidget(new[]
            {UiAssetTypes.ReturnButton, UiAssetTypes.Atb, UiAssetTypes.ActionChooser, UiAssetTypes.BattleResultText});
        _context.SetNewSubstate(SubState.Undefined);
        GameEntity unloadSceneEntity = _context.CreateEntity();
        unloadSceneEntity.AddUnloadScene(GameSceneConstants.BattleScene);
    }
}