using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class UnloadSceneSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Undefined};
    
    public UnloadSceneSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.UnloadScene);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            var currentEntity = entities[i];
            SceneManager.UnloadSceneAsync(currentEntity.unloadScene.SceneNameToUnload);
        }
    }
}