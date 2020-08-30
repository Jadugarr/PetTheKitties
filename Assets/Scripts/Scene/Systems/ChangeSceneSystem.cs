using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSystem : GameReactiveSystem, ICleanupSystem
{
    private IGroup<GameEntity> sceneChangeGroup;
    private IGroup<GameEntity> loadingGroup;

    public ChangeSceneSystem(IContext<GameEntity> context) : base(context)
    {
        sceneChangeGroup = context.GetGroup(GameMatcher.ChangeScene);
        loadingGroup = context.GetGroup(GameMatcher.Loading);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChangeScene);
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
        if (entities.Count > 1)
        {
            throw new ArgumentException("There are too many entites for changing a scene!");
        }
        else
        {
            _context.CreateEntity().isLoading = true;
            SceneManager.LoadSceneAsync(entities[0].changeScene.SceneName, entities[0].changeScene.LoadSceneMode).completed += OnSceneLoadCompleted;
        }
    }

    private void OnSceneLoadCompleted(AsyncOperation asyncOperation)
    {
        if (asyncOperation.isDone)
        {
            GameEntity[] loadingEntities = loadingGroup.GetEntities();
            loadingEntities[0].isLoading = false;
            loadingEntities[0].Destroy();
        }
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in sceneChangeGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}