using Entitas;
using System;
using Configurations;
using Entitas.Actions.Systems;
using Entitas.Animations.Systems;
using Entitas.Battle.Systems;
using Entitas.Camera.Systems;
using Entitas.Input.Systems;
using Entitas.Kitty.Systems;
using Entitas.Position;
using Entitas.Scripts.Common.Systems;
using Entitas.World.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnConfiguration spawnConfiguration;
    [SerializeField] private CharacterConfiguration characterConfiguration;
    [SerializeField] private MovementConstantsConfiguration movementConstantsConfiguration;
    [SerializeField] private AssetReferenceConfiguration assetReferenceConfiguration;

    private Systems updateSystems;
    private Systems fixedUpdateSystems;
    private Systems lateUpdateSystems;

    private void Awake()
    {
        updateSystems = new Feature("UpdateSystems");
        fixedUpdateSystems = new Feature("FixedUpdateSystems");
        lateUpdateSystems = new Feature("LateUpdateSystems");

        Contexts contexts = Contexts.sharedInstance;
        foreach (var context in contexts.allContexts)
        {
            context.OnEntityCreated += OnEntityCreated;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode sceneMode)
    {
        Contexts.sharedInstance.game.ReplaceCurrentScene(loadedScene.name);
    }

    // Use this for initialization
    void Start()
    {
        InitConfigs();
        Contexts pools = Contexts.sharedInstance;

        CreateSystems(pools.game);

        ExecuteOneOffSystems(pools.game);
        updateSystems.Initialize();
        fixedUpdateSystems.Initialize();
        lateUpdateSystems.Initialize();
    }

    // add an id to every entity as it's created
    private void OnEntityCreated(IContext context, IEntity entity)
    {
        (entity as GameEntity).AddId(entity.creationIndex);
    }

    private void ExecuteOneOffSystems(GameContext context)
    {
        Systems oneOffSystems = new Feature("OneOffSystems")
            .Add(new InitializeCameraSystem(context))
            .Add(new InitializeGameStateSystem())
            .Add(new InitializeSceneSystem());

        oneOffSystems.Initialize();
    }

    private void CreateSystems(GameContext context)
    {
        #region BaseSystems

        updateSystems
            //Promises
            .Add(new InitPromisesSystem())
            //Input
            .Add(new InputSystem(context))
            //Scene
            /*.Add(new EnterBattleStateSystem(context))
            .Add(new ExitBattleStateSystem(context))*/
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

        #region WinConditionSystems

        updateSystems
            .Add(new CheckKillEnemiesConditionSystem(context))
            .Add(new CheckKittensReachedGoalConditionSystem(context));

        #endregion

        #region LoseConditionSystems

        updateSystems
            .Add(new CheckPlayerDeadConditionSystem(context));

        #endregion

        #region FallingStateSystems

        updateSystems
            .Add(new HandleGroundedJumpStateSystem(context));

        #endregion

        #region BattleLostSystems

        /*updateSystems
            .Add(new DisplayBattleLostSystem());*/

        #endregion

        #region BattleWonSystems

        /*updateSystems
            .Add(new DisplayBattleWonSystem());*/

        #endregion

        #region MainMenuSystems

        updateSystems
            .Add(new InitializeMainMenuSystem());

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

        #region WorldUpdateSystems

        updateSystems
            //Input
            .Add(new ProcessPauseInputSystem(context))
            .Add(new ProcessUnpauseInputSystem(context))
            .Add(new ProcessWorldMoveInputSystem(context))
            .Add(new ProcessJumpInputSystem(context))
            .Add(new CountTimeSinceLastJumpInputSystem(context))
            .Add(new ResetTimeSinceLastJumpInputSystem(context))
            .Add(new CheckJumpInputAvailableSystem(context))
            .Add(new CountTimeSinceLastPauseInputSystem(context))
            .Add(new ResetTimeSinceLastPauseInputSystem(context))
            .Add(new CheckPauseInputAvailabilitySystem(context))
            .Add(new ProcessInteractionInputSystem(context))
            .Add(new SetCameraFollowTargetSystem(context))
            .Add(new WorldSceneLoadedSystem(context))
            .Add(new SetCameraConfinerSystem(context))
            .Add(new WorldPlayerAddedSystem(context))
            .Add(new KittyAddedSystem(context))
            .Add(new CharacterDeathSystem(context))
            .Add(new CheckInteractInputAvailableSystem(context))
            .Add(new KittyInteractionSystem(context))
            .Add(new CharacterStartFollowSystem(context))
            .Add(new CharacterDirectionSystem(context))
            .Add(new CharacterFollowSystem(context))
            .Add(new CharacterScaredSystem(context))
            .Add(new CharacterReachedGoalSystem(context))
            .Add(new HandleJumpEndingStateSystem(context))
            .Add(new HandleCharacterMovementStateSystem(context))
            .Add(new HandleFallingStateSystem(context))
            .Add(new UpdateKittyAmountDisplaySystem(context))
            .Add(new KittySavedSystem(context))
            //WinConditions
            .Add(new WinConditionControllerSystem(context))
            .Add(new LoseConditionControllerSystem(context))
            .Add(new PlayerWonSystem(context))
            .Add(new ExitPlayerWonStateSystem(context));

        #endregion

        #region WorldFixedUpdateSystems

        fixedUpdateSystems
            .Add(new SyncPositionAndViewSystem(context))
            .Add(new SyncVelocitySystem(context))
            .Add(new SyncMovementAnimationSystem(context))
            .Add(new CheckCharacterGroundStateSystem(context))
            .Add(new CharacterOnGroundSystem(context))
            .Add(new SetGravityScaleSystem(context))
            .Add(new MoveCharacterSystem(context))
            .Add(new AdjustMoveEndingVelocitySystem(context))
            .Add(new StartJumpCharacterSystem(context))
            .Add(new AdjustCharacterMovementToSlopeSystem(context))
            .Add(new AdjustEndingJumpVelocitySystem(context))
            .Add(new CharacterOnGroundMovementVelocitySystem(context))
            .Add(new CharacterAirborneMovementVelocitySystem(context))
            // Gravity
            .Add(new CharacterGravitySystem(context))
            //Position
            .Add(new RenderPositionSystem(context))
            //Velocity
            .Add(new RenderVelocitySystem(context))
            //Animations
            .Add(new RenderVelocityAnimationsSystem(context))
            .Add(new RenderCharacterStateAnimationsSystem(context));

        #endregion
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
        GameConfigurations.MovementConstantsConfiguration = movementConstantsConfiguration;
        GameConfigurations.AssetReferenceConfiguration = assetReferenceConfiguration;
    }

    // Update is called once per frame
    private void Update()
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
    }
}