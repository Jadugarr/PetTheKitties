using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSystem : GameReactiveSystem, ICleanupSystem
{
    private IGroup<GameEntity> sceneChangeGroup;
    private List<GameEntity> loadingList = new List<GameEntity>();

    public ChangeSceneSystem(IContext<GameEntity> context) : base(context)
    {
        sceneChangeGroup = context.GetGroup(GameMatcher.ChangeScene);
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
            GameEntity loadingEntity = _context.CreateEntity();
            loadingEntity.isLoading = true;
            loadingList.Add(loadingEntity);
            SceneManager.LoadSceneAsync(entities[0].changeScene.SceneName, entities[0].changeScene.LoadSceneMode)
                .completed += OnSceneLoadCompleted;
        }
    }

    private void OnSceneLoadCompleted(AsyncOperation asyncOperation)
    {
        if (asyncOperation.isDone)
        {
            for (int i = loadingList.Count - 1; i >= 0; i--)
            {
                GameEntity loadingEntity = loadingList[i];

                if (loadingEntity != null && loadingEntity.isLoading)
                {
                    loadingEntity.isLoading = false;
                    loadingEntity.Destroy();
                    break;
                }
            }
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