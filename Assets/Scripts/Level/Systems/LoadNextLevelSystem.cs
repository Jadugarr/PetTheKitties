using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadNextLevelSystem : GameReactiveSystem
{
    private IGroup<GameEntity> levelEntities;

    public LoadNextLevelSystem(IContext<GameEntity> context) : base(context)
    {
        levelEntities = context.GetGroup(GameMatcher.Level);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.LoadNextLevel, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameEntity levelEntity = levelEntities.GetSingleEntity();
        levelEntity.isLoading = true;
        int levelIndex = levelEntity.levelIndex.Value;
        int levelAmount = GameConfigurations.AssetReferenceConfiguration.Levels.Length;
        levelEntity.ReplaceLevelIndex(levelIndex == levelAmount - 1 ? 0 : levelIndex + 1);
        AssetReference nextLevelReference =
            GameConfigurations.AssetReferenceConfiguration.Levels[levelEntity.levelIndex.Value];
        Addressables.InstantiateAsync(nextLevelReference).Completed += handle =>
        {
            levelEntity.ReplaceView(handle.Result);
            handle.Result.Link(levelEntity);
            levelEntity.isLoading = false;
            _context.isLoadNextLevel = false;
        };
    }
}