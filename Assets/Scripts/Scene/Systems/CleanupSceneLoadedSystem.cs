using Entitas;

public class CleanupSceneLoadedSystem : ICleanupSystem
{
    private IGroup<GameEntity> sceneloadedGroup;

    public CleanupSceneLoadedSystem(GameContext context)
    {
        sceneloadedGroup = context.GetGroup(GameMatcher.SceneLoaded);
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in sceneloadedGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}