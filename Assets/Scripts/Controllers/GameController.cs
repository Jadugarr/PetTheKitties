using Entitas;
using System;
using Configurations;
using Entitas.Camera.Systems;
using Entitas.Controllers;
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
            //Scene
            /*.Add(new EnterBattleStateSystem(context))
            .Add(new ExitBattleStateSystem(context))*/
            .Add(new LoadingComponentsAddedSystem(context))
            .Add(new AllLoadingComponentsRemovedSystem(context))
            .Add(new EnterMainMenuStateSystem(context))
            .Add(new ExitMainMenuStateSystem(context))
            .Add(new EnterWorldStateSystem(context))
            .Add(new ExitWorldStateSystem(context))
            .Add(new ChangeSceneSystem(context))
            .Add(new UnloadSceneSystem(context))
            .Add(new CleanupSceneLoadedSystem(context))
            .Add(new CleanupUnloadSceneSystem(context))
            //Game State
            .Add(new ChangeGameStateInputMapSystem(context))
            //Sub State
            .Add(new ChangeSubStateInputMapSystem(context))
            .Add(new EnterPausedSubStateSystem(context))
            .Add(new ExitPausedSubStateSystem(context))
            .Add(new ExitChooseActionStateSystem(context))
            .Add(new RestartLevelSystem(context));

        #endregion

        #region ChooseActionStateSystems

        /*updateSystems
            .Add(new InitializeChooseActionSystem(context))
            .Add(new ActionChosenSystem(context));*/

        #endregion

        #region ChooseTargetSystems

        /*updateSystems
            .Add(new InitializeChooseTargetSystem(context))
            .Add(new ActionTargetChosenSystem(context));*/

        #endregion

        #region FinalizeActionSystems

        /*updateSystems
            .Add(new AddActionTimeSystem(context))
            .Add(new ActionTimeAddedSystem(context));*/

        #endregion

        #region ExecuteActionSystems

        /*updateSystems
            //Actions
            .Add(new InitializeExecuteActionSystem(context))
            .Add(new ExecutePlayerAttackActionSystem(context))
            .Add(new ExecuteDefenseActionSystem(context))
            .Add(new ReleaseDefenseActionSystem(context))
            .Add(new ActionFinishedSystem(context));*/

        #endregion

        #region BattleLostSystems

        /*updateSystems
            .Add(new DisplayBattleLostSystem());*/

        #endregion

        #region BattleWonSystems

        /*updateSystems
            .Add(new DisplayBattleWonSystem());*/

        #endregion

        #region WaitingStateSystems

        /*updateSystems
            .Add(new ActionTimeSystem(context))
            //Actions
            .Add(new ExecuteChooseActionSystem(context))
            .Add(new ExecuteActionsSystem(context));*/

        #endregion

        #region BattleStateSystems

        /*updateSystems
            .Add(new InitializeBattleSystem(context))
            .Add(new InitializeATBSystem(context))
            //Battle
            .Add(new CharacterDeathSystem(context))
            .Add(new TeardownCharacterSystem(context))
            .Add(new TeardownBattleSystem(context))
            //WinConditions
            .Add(new WinConditionControllerSystem(context))
            .Add(new LoseConditionControllerSystem(context));*/

        #endregion

        #region TestSystems

        updateSystems
            // Some test systems
            .Add(new ProcessRaycastTestInputSystem(context))
            .Add(new RaycastTestSystem(context));

        #endregion
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

    // Update is called once per frame
    /*private void Update()
    {
        updateSystems.Execute();
    }

    private void FixedUpdate()
    {
        fixedUpdateSystems.Execute();
    }

    private void LateUpdate()
    {
        lateUpdateSystems.Execute();
        updateSystems.Cleanup();
        fixedUpdateSystems.Cleanup();
        lateUpdateSystems.Cleanup();
    }*/
}