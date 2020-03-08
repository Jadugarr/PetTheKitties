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
    private void Update()
    {
        List<Systems> activeSystems = GameSystemService.GetActiveSystems();

        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Execute();
        }
    }

    private void LateUpdate()
    {
        List<Systems> activeSystems = GameSystemService.GetActiveSystems();
        endFrameSystems.Execute();
        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Cleanup();
        }
        GameSystemService.RefreshActiveSystems();
    }
}