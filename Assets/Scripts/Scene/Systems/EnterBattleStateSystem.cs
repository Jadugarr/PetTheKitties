using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
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

        if (!GameSystemService.HasSystemMapping(GameState.Battle))
        {
            Debug.LogError("Didn't create battle state systems yet!");
            return;
        }

        Systems battleSystems = GameSystemService.GetSystemMapping(GameState.Battle);
        GameSystemService.AddActiveSystems(battleSystems);
        _context.SetNewSubstate(SubState.Waiting);
    }
}