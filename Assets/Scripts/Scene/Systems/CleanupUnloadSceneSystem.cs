using Entitas;

public class CleanupUnloadSceneSystem : ICleanupSystem
{
    private IGroup<GameEntity> unloadSceneGroup;

    public CleanupUnloadSceneSystem(GameContext context)
    {
        unloadSceneGroup = context.GetGroup(GameMatcher.UnloadScene);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in unloadSceneGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }

}