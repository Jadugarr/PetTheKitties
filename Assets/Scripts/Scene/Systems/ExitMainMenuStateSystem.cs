using System.Collections.Generic;
using Entitas;

public class ExitMainMenuStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Undefined};
    

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

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        Systems mainMenuSystems = GameSystemService.GetSystemMapping(GameState.MainMenu);
        if (mainMenuSystems != null)
        {
            GameSystemService.RemoveActiveSystems(mainMenuSystems);
        }

        UIService.HideWidget(AssetTypes.MainMenu);
        GameEntity unloadScenEntity = _context.CreateEntity();
        unloadScenEntity.AddUnloadScene(GameSceneConstants.MainMenuScene);
    }
}