using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class EnterMainMenuStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.MainMenu};
    
    private IGroup<GameEntity> sceneLoadedGroup;

    public EnterMainMenuStateSystem(GameContext context) : base(context)
    {
        sceneLoadedGroup = context.GetGroup(GameMatcher.SceneLoaded);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.CurrentGameState == GameState.MainMenu;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameEntity changeSceneEntity = _context.CreateEntity();
        changeSceneEntity.AddChangeScene(GameSceneConstants.MainMenuScene, LoadSceneMode.Additive);
        sceneLoadedGroup.OnEntityAdded += OnMainMenuSceneLoaded;
    }

    private void OnMainMenuSceneLoaded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        sceneLoadedGroup.OnEntityAdded -= OnMainMenuSceneLoaded;

        if (!GameSystemService.HasSystemMapping(GameState.MainMenu))
        {
            CreateMainMenuSystems();
        }

        Systems mainMenuSystems = GameSystemService.GetSystemMapping(GameState.MainMenu);

        GameSystemService.AddActiveSystems(mainMenuSystems);
    }

    private void CreateMainMenuSystems()
    {
    }
}