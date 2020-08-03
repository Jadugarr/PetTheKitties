using Entitas;
using UnityEngine.SceneManagement;

public class InitializeSceneSystem : IInitializeSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.ReplaceCurrentScene(SceneManager.GetActiveScene().name);
    }
}