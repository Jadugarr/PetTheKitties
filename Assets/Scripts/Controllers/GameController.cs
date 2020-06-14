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
    [SerializeField] private AssetReferenceConfiguration assetReferenceConfiguration;

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
            //Promises
            .Add(new InitPromisesSystem())
            //Input
            .Add(new InputSystem(context))
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
        
        Systems universalFixedUpdateSystems = new Feature("UniversalFixedUpdateSystems");

        GameSystemService.AddActiveSystems(universalSystems);
        GameSystemService.AddActiveSystems(universalFixedUpdateSystems, SystemsUpdateType.FixedUpdate);
    }

    private void CreateEndFrameSystems(GameContext context)
    {
        // Systems endFrameSystems = new Feature("EndFrameSystems");
        //
        // GameSystemService.AddActiveSystems(endFrameSystems, SystemsUpdateType.LateUpdate);
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
        List<Systems> activeSystems = GameSystemService.GetActiveSystems();

        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Execute();
        }
    }

    private void FixedUpdate()
    {
        List<Systems> fixedUpdateSystems = GameSystemService.GetActiveFixedUpdateSystems();

        foreach (Systems fixedUpdateSystem in fixedUpdateSystems)
        {
            fixedUpdateSystem.Execute();
        }
    }

    private void LateUpdate()
    {
        List<Systems> lateUpdateSystems = GameSystemService.GetActiveLateUpdateSystems();
        foreach (Systems lateUpdateSystem in lateUpdateSystems)
        {
            lateUpdateSystem.Execute();
        }
        
        List<Systems> activeSystems = GameSystemService.GetActiveSystems();
        List<Systems> fixedUpdateSystems = GameSystemService.GetActiveFixedUpdateSystems();
        foreach (Systems activeSystem in activeSystems)
        {
            activeSystem.Cleanup();
        }
        
        foreach (Systems activeSystem in fixedUpdateSystems)
        {
            activeSystem.Cleanup();
        }
        
        foreach (Systems activeSystem in lateUpdateSystems)
        {
            activeSystem.Cleanup();
        }
        GameSystemService.RefreshActiveSystems();
    }
}