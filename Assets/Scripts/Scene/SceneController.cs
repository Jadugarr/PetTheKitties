using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private GameContext context;

    public void Awake()
    {
        context = Contexts.sharedInstance.game;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    } 

    private void OnSceneLoaded(Scene newScene, LoadSceneMode loadMode)
    {
        GameEntity newEntity = context.CreateEntity();
        newEntity.AddSceneLoaded(newScene.name);
    }
}