using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using Entitas.Common;
using Entitas.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBattleStateSystem : GameReactiveSystem
{
    private IGroup<GameEntity> sceneLoadedGroup;

    public EnterBattleStateSystem(GameContext context) : base(context)
    {
        sceneLoadedGroup = context.GetGroup(GameMatcher.SceneLoaded);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.gameState.CurrentGameState == GameState.Battle;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        sceneLoadedGroup.OnEntityAdded += OnBattleSceneLoaded;

        GameEntity changeSceneEntity = _context.CreateEntity();
        changeSceneEntity.AddChangeScene(GameSceneConstants.BattleScene, LoadSceneMode.Additive);
    }

    private void OnBattleSceneLoaded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        sceneLoadedGroup.OnEntityAdded -= OnBattleSceneLoaded;

        if (!GameSystemService.HasSystemMapping(GameSystemType.Battle))
        {
            CreateBattleSystems();
        }

        Systems battleSystems = GameSystemService.GetSystemMapping(GameSystemType.Battle);
        GameSystemService.AddActiveSystems(battleSystems);
        _context.SetNewSubstate(SubState.Waiting);
    }

    private void CreateBattleSystems()
    {
        Systems battleSystems = new Feature("BattleStateSystems")
            .Add(new InitializeBattleSystem(_context))
            .Add(new InitializeATBSystem(_context))
            //Battle
            .Add(new CharacterDeathSystem(_context))
            .Add(new TeardownCharacterSystem(_context))
            .Add(new TeardownBattleSystem(_context))
            //WinConditions
            .Add(new InitializeAndTeardownWinConditionsSystem(_context))
            .Add(new InitializeAndTeardownLoseConditionsSystem(_context))
            .Add(new WinConditionControllerSystem(_context))
            .Add(new LoseConditionControllerSystem(_context));
        
        GameSystemService.AddSystemMapping(GameSystemType.Battle, battleSystems);
    }
}