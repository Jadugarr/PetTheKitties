using Configurations;
using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class WorldSceneLoadedSystem : GameInitializeSystem, ITearDownSystem
{
    private IGroup<GameEntity> levelEntities;

    public WorldSceneLoadedSystem(GameContext context) : base(context)
    {
        levelEntities = context.GetGroup(GameMatcher.Level);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        GameEntity levelEntity = _context.CreateEntity();
        levelEntity.isLevel = true;
        levelEntity.ReplaceLevelIndex(0);
        levelEntity.isLoading = true;

        AssetReference levelReference =
            GameConfigurations.AssetReferenceConfiguration.Levels[levelEntity.levelIndex.Value];
        Addressables.InstantiateAsync(levelReference).Completed += handle =>
        {
            levelEntity.AddView(handle.Result);
            handle.Result.Link(levelEntity);
            levelEntity.isLoading = false;
        };
    }

    public void TearDown()
    {
        GameEntity levelEntity = levelEntities.GetSingleEntity();
        if (levelEntity.hasView && levelEntity.view.View != null)
        {
            levelEntity.view.View.Unlink();
            levelEntity.view.View.DestroyGameObject();
            levelEntity.Destroy();
        }
    }
}