using Entitas;
using System;
using System.Collections.Generic;
using Configurations;
using Entitas.Actions.Systems;
using Entitas.Animations.Systems;
using Entitas.Battle.Systems;
using Entitas.Camera.Systems;
using Entitas.Input.Systems;
using Entitas.Kitty.Systems;
using Entitas.Player;
using Entitas.Position;
using Entitas.Scripts.Common.Systems;
using Entitas.World.Systems;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnConfiguration spawnConfiguration;
    [SerializeField] private CharacterConfiguration characterConfiguration;
    [SerializeField] private MovementConstantsConfiguration movementConstantsConfiguration;

    private Systems endFrameSystems;

    private void Awake()
    {
        Contexts contexts = Contexts.sharedInstance;
        foreach (var context in contexts.allContexts)
        {
            context.OnEntityCreated += OnEntityCreated;
        }
    }

    // Use this for initialization
    void Start()
    {
        InitConfigs();
        Contexts pools = Contexts.sharedInstance;

        CreateUniversalSystems(pools.game);
        CreateEndFrameSystems(pools.game);
        CreateSystems(pools.game);

        ExecuteOneOffSystems(pools.game);
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
            .Add(new InitializeGameStateSystem());

        oneOffSystems.Initialize();
    }

    private void CreateUniversalSystems(GameContext context)
    {
        Systems universalSystems = new Feature("UniversalSystems")
            .Add(new SyncPositionAndViewSystem(context))
            .Add(new SyncVelocitySystem(context))
            .Add(new SyncMovementAnimationSystem(context))
            //Promises
            .Add(new InitPromisesSystem())
            //Input
            .Add(new InputSystem(context))
            .Add(new ProcessPauseInputSystem(context))
            .Add(new ProcessUnpauseInputSystem(context))
            .Add(new CheckPauseInputAvailabilitySystem(context))
            .Add(new ProcessWorldMoveInputSystem(context))
            .Add(new ProcessJumpInputSystem(context))
            .Add(new CheckJumpInputAvailableSystem(context))
            //Scene
            .Add(new EnterBattleStateSystem(context))
            .Add(new ExitBattleStateSystem(context))
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
            .Add(new EnterWaitingSubStateSystem(context))
            .Add(new ExitWaitingSubStateSystem(context))
            .Add(new EnterPlayerWonStateSystem(context))
            .Add(new ExitPlayerWonStateSystem(context))
            .Add(new EnterPlayerLostStateSystem(context))
            .Add(new ExitPlayerLostStateSystem(context))
            .Add(new EnterChooseActionStateSystem(context))
            .Add(new ExitChooseActionStateSystem(context))
            .Add(new EnterChooseTargetStateSystem(context))
            .Add(new ExitChooseTargetStateSystem(context))
            .Add(new EnterExecuteActionStateSystem(context))
            .Add(new ExitExecuteActionStateSystem(context))
            .Add(new EnterFinalizeActionStateSystem(context))
            .Add(new ExitFinalizeActionStateSystem(context))
            .Add(new EnterWorldNavigationSubStateSystem(context))
            .Add(new ExitWorldNavigationSubStateSystem(context))
            .Add(new RestartLevelSystem(context));

        GameSystemService.AddActiveSystems(universalSystems);
    }

    private void CreateEndFrameSystems(GameContext context)
    {
        endFrameSystems = new Feature("EndFrameSystems")
            // Gravity
            .Add(new CharacterGravitySystem(context))
            //Position
            .Add(new RenderPositionSystem(context))
            //Velocity
            .Add(new RenderVelocitySystem(context))
            //Animations
            .Add(new RenderVelocityAnimationsSystem(context))
            .Add(new RenderCharacterStateAnimationsSystem(context));
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
        GameConfigurations.MovementConstantsConfiguration = movementConstantsConfiguration;
    }

    // Update is called once per frame
    void Update()
    {
        List<Systems> activeSystems = GameSystemService.GetActiveSystems();

        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Execute();
        }

        endFrameSystems.Execute();

        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Cleanup();
        }

        GameSystemService.RefreshActiveSystems();
    }

    private void CreateSystems(GameContext context)
    {
        Systems executeActionSystems = new Feature("ExecuteActionSystems")
            //Actions
            .Add(new InitializeExecuteActionSystem(context))
            .Add(new ExecutePlayerAttackActionSystem(context))
            .Add(new ExecuteDefenseActionSystem(context))
            .Add(new ReleaseDefenseActionSystem(context))
            .Add(new ActionFinishedSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.ExecuteAction, executeActionSystems);

        Systems battleSystems = new Feature("BattleStateSystems")
            .Add(new InitializeBattleSystem(context))
            .Add(new InitializeATBSystem(context))
            //Battle
            .Add(new CharacterDeathSystem(context))
            .Add(new TeardownCharacterSystem(context))
            .Add(new TeardownBattleSystem(context))
            //WinConditions
            .Add(new InitializeAndTeardownWinConditionsSystem(context))
            .Add(new InitializeAndTeardownLoseConditionsSystem(context))
            .Add(new WinConditionControllerSystem(context))
            .Add(new LoseConditionControllerSystem(context));


        GameSystemService.AddSystemMapping(GameState.Battle, battleSystems);

        Systems waitStateSystems = new Feature("WaitingSubStateSystems")
            .Add(new ActionTimeSystem(context))
            //Actions
            .Add(new ExecuteChooseActionSystem(context))
            .Add(new ExecuteActionsSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.Waiting, waitStateSystems);

        Systems mainMenuSystems = new Feature("MainMenuSystems");
        mainMenuSystems.Add(new InitializeMainMenuSystem());

        GameSystemService.AddSystemMapping(GameState.MainMenu, mainMenuSystems);

        Systems finalizeActionSystems = new Feature("finalizeActionSystems")
            .Add(new AddActionTimeSystem(context))
            .Add(new ActionTimeAddedSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.FinalizeAction, finalizeActionSystems);

        Systems playerLostSystems = new Feature("PlayerLostSystems")
            .Add(new DisplayBattleLostSystem());

        GameSystemService.AddSubSystemMapping(SubState.PlayerLost, playerLostSystems);

        Systems playerWonSystems = new Feature("PlayerWonSystems")
            .Add(new DisplayBattleWonSystem());

        GameSystemService.AddSubSystemMapping(SubState.PlayerWon, playerWonSystems);

        Systems chooseTargetSystems = new Feature("ChooseTargetSystems")
            .Add(new InitializeChooseTargetSystem(context))
            .Add(new ActionTargetChosenSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.ChooseTarget, chooseTargetSystems);

        Systems chooseActionSystems = new Feature("ChooseActionSystems")
            .Add(new InitializeChooseActionSystem(context))
            .Add(new ActionChosenSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.ChooseAction, chooseActionSystems);

        Systems worldSystems = new Feature("WorldSystems")
            .Add(new CheckCharacterGroundStateSystem(context))
//            .Add(new AdjustCharacterMovementToSlopeSystem(context))
            .Add(new SetGravityScaleSystem(context))
            .Add(new SetCameraFollowTargetSystem(context))
            .Add(new InitializeWorldStateSystem(context))
            .Add(new SetCameraConfinerSystem(context))
            .Add(new WorldPlayerAddedSystem(context))
            .Add(new KittyAddedSystem(context))
            .Add(new CharacterDeathSystem(context))
            //WinConditions
            .Add(new InitializeAndTeardownWinConditionsSystem(context))
            .Add(new InitializeAndTeardownLoseConditionsSystem(context))
            .Add(new WinConditionControllerSystem(context))
            .Add(new LoseConditionControllerSystem(context))
            .Add(new CharacterFollowSystem(context))
            .Add(new CharacterScaredSystem(context))
            .Add(new CharacterReachedGoalSystem(context));

        GameSystemService.AddSystemMapping(GameState.World, worldSystems);

        Systems worldMovementSystems = new Feature("WorldMovementSystems")
            .Add(new ProcessInteractionInputSystem(context))
            .Add(new CheckInteractInputAvailableSystem(context))
            .Add(new KittyInteractionSystem(context))
            .Add(new CharacterStartFollowSystem(context))
            .Add(new CharacterDirectionSystem(context))
            .Add(new MoveCharacterSystem(context))
            .Add(new HandleCharacterMovementStateSystem(context))
            .Add(new ManageJumpingStateHandlingSystem(context))
            .Add(new ManageFallingStateHandlingSystem(context))
            .Add(new AdjustMoveEndingVelocitySystem(context))
            .Add(new HandleFallingStateSystem(context))
            .Add(new StartJumpCharacterSystem(context))
            .Add(new AdjustCharacterMovementToSlopeSystem(context))
            .Add(new CharacterOnGroundMovementVelocitySystem(context))
            .Add(new CharacterAirborneMovementVelocitySystem(context))
            // Some test systems
            .Add(new ProcessRaycastTestInputSystem(context))
            .Add(new RaycastTestSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.WorldNavigation, worldMovementSystems);
    }
}