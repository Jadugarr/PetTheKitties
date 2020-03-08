using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using UnityEngine.SceneManagement;

public class EnterMainMenuStateSystem : GameReactiveSystem
{
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

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.MainMenu;
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

        if (!GameSystemService.HasSystemMapping(GameSystemType.MainMenu))
        {
            CreateMainMenuSystems();
        }

        Systems mainMenuSystems = GameSystemService.GetSystemMapping(GameSystemType.MainMenu);

        GameSystemService.AddActiveSystems(mainMenuSystems);
    }

    private void CreateMainMenuSystems()
    {
        Systems mainMenuSystems = new Feature("MainMenuSystems");
        mainMenuSystems.Add(new InitializeMainMenuSystem());
        
        GameSystemService.AddSystemMapping(GameSystemType.MainMenu, mainMenuSystems);
    }
}