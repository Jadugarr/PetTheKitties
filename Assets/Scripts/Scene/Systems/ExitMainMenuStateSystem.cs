using System.Collections.Generic;
using Entitas;

public class ExitMainMenuStateSystem : GameReactiveSystem
{
    public ExitMainMenuStateSystem(GameContext context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.PreviousGameState == GameState.MainMenu;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        Systems mainMenuSystems = GameSystemService.GetSystemMapping(GameState.MainMenu);
        if (mainMenuSystems != null)
        {
            GameSystemService.RemoveActiveSystems(mainMenuSystems);
        }

        UIService.HideWidget(UiAssetTypes.MainMenu);
        GameEntity unloadScenEntity = _context.CreateEntity();
        unloadScenEntity.AddUnloadScene(GameSceneConstants.MainMenuScene);
    }
}