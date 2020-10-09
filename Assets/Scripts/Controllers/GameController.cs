using Entitas;
using System;
using Configurations;
using Entitas.Camera.Systems;
using Entitas.Scripts.Common.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameController : AGameController
{
    [SerializeField] private SpawnConfiguration spawnConfiguration;
    [SerializeField] private CharacterConfiguration characterConfiguration;
    [SerializeField] private MovementConstantsConfiguration movementConstantsConfiguration;
    [SerializeField] private AssetReferenceConfiguration assetReferenceConfiguration;

    protected override IContext GetContext()
    {
        return Contexts.sharedInstance.game;
    }

    protected override void CreateSystems(IContext _context)
    {
        GameContext context = (GameContext) _context;

        #region BaseSystems

        updateSystems
            .Add(new InitializeCameraSystem(context))
            .Add(new InitializeGameStateSystem())
            .Add(new InitializeSceneSystem())
            //Promises
            .Add(new InitPromisesSystem())
            //Input
            .Add(new InputSystem(context))
            .Add(new LoadingComponentsAddedSystem(context))
            .Add(new EnterMainMenuStateSystem(context))
            .Add(new ExitMainMenuStateSystem(context))
            .Add(new EnterWorldStateSystem(context))
            .Add(new ExitWorldStateSystem(context))
            .Add(new ChangeSceneSystem(context))
            .Add(new UnloadSceneSystem(context))
            .Add(new CleanupSceneLoadedSystem(context))
            //Game State
            .Add(new ChangeGameStateInputMapSystem(context))
            //Sub State
            .Add(new ChangeSubStateInputMapSystem(context))
            .Add(new EnterPausedSubStateSystem(context))
            .Add(new ExitPausedSubStateSystem(context))
            .Add(new ExitChooseActionStateSystem(context))
            .Add(new RestartLevelSystem(context))
            .Add(new LoadNextLevelSystem(context))
            .Add(new RestartControllerSystem(context));

        #endregion

        #region TestSystems

        updateSystems
            // Some test systems
            .Add(new ProcessRaycastTestInputSystem(context))
            .Add(new RaycastTestSystem(context));

        #endregion
    }

    public override GameControllerType GetGameControllerType()
    {
        return GameControllerType.Game;
    }

    protected override void AfterAwake()
    {
        Contexts contexts = Contexts.sharedInstance;
        foreach (var context in contexts.allContexts)
        {
            context.OnEntityCreated += OnEntityCreated;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void BeforeStart()
    {
        InitConfigs();
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode sceneMode)
    {
        Contexts.sharedInstance.game.ReplaceCurrentScene(loadedScene.name);
    }

    // add an id to every entity as it's created
    private void OnEntityCreated(IContext context, IEntity entity)
    {
        (entity as GameEntity).AddId(entity.creationIndex);
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
        GameConfigurations.MovementConstantsConfiguration = movementConstantsConfiguration;
        GameConfigurations.AssetReferenceConfiguration = assetReferenceConfiguration;
    }
}