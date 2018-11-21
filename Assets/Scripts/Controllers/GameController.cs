using Entitas;
using System;
using System.Collections.Generic;
using Configurations;
using Entitas.Actions.Systems;
using Entitas.Battle.Systems;
using Entitas.Position;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
    [SerializeField] private SpawnConfiguration spawnConfiguration;
    [SerializeField] private CharacterConfiguration characterConfiguration;

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
    }

    // add an id to every entity as it's created
    private void OnEntityCreated(IContext context, IEntity entity)
    {
        (entity as GameEntity).AddId(entity.creationIndex);
    }

    private void CreateUniversalSystems(GameContext context)
    {
        Systems universalSystems = new Feature("UniversalSystems")
            .Add(new SyncPositionAndViewSystem(context))
            //Promises
            .Add(new InitPromisesSystem())
            //Input
            .Add(new InputSystem(context))
            .Add(new ProcessPauseInputSystem(context))
            .Add(new ProcessUnpauseInputSystem(context))
            .Add(new CheckPauseInputAvailabilitySystem(context))
            .Add(new ProcessWorldMoveInputSystem(context))
            //Scene
            .Add(new EnterBattleStateSystem(context))
            .Add(new ExitBattleStateSystem(context))
            .Add(new EnterMainMenuStateSystem(context))
            .Add(new ExitMainMenuStateSystem(context))
            .Add(new EnterWorldStateSystem(context))
            .Add(new ChangeSceneSystem(context))
            .Add(new UnloadSceneSystem(context))
            .Add(new CleanupSceneLoadedSystem(context))
            .Add(new CleanupUnloadSceneSystem(context))
            //Game State
            .Add(new ChangeGameStateInputMapSystem(context))
            .Add(new InitializeGameStateSystem())
            //Sub State
            .Add(new ChangeSubStateInputMapSystem(context))
            .Add(new EnterPausedSubStateSystem(context))
            .Add(new ExitPausedSubStateSystem(context))
            .Add(new EnterWaitingSubStateSystem(context))
            .Add(new ExitWaitingSubStateSystem(context))
            .Add(new EnterBattleWonStateSystem(context))
            .Add(new ExitBattleWonStateSystem(context))
            .Add(new EnterBattleLostStateSystem(context))
            .Add(new ExitBattleLostStateSystem(context))
            .Add(new EnterChooseActionStateSystem(context))
            .Add(new ExitChooseActionStateSystem(context))
            .Add(new EnterChooseTargetStateSystem(context))
            .Add(new ExitChooseTargetStateSystem(context))
            .Add(new EnterExecuteActionStateSystem(context))
            .Add(new ExitExecuteActionStateSystem(context))
            .Add(new EnterFinalizeActionStateSystem(context))
            .Add(new ExitFinalizeActionStateSystem(context))
            .Add(new EnterWorldNavigationSubStateSystem(context))
            .Add(new ExitWorldNavigationSubStateSystem(context));

        GameSystemService.AddActiveSystems(universalSystems);
    }

    private void CreateEndFrameSystems(GameContext context)
    {
        endFrameSystems = new Feature("EndFrameSystems")
            //Position
            .Add(new RenderPositionSystem(context));
    }

    private void InitConfigs()
    {
        GameConfigurations.SpawnConfiguration = spawnConfiguration;
        GameConfigurations.CharacterConfiguration = characterConfiguration;
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
            .Add(new InitializeWorldStateSystem(context))
            .Add(new WorldPlayerAddedSystem(context));

        GameSystemService.AddSystemMapping(GameState.World, worldSystems);

        Systems worldMovementSystems = new Feature("WorldMovementSystems")
            .Add(new MoveCharacterSystem(context));

        GameSystemService.AddSubSystemMapping(SubState.WorldNavigation, worldMovementSystems);
    }
}