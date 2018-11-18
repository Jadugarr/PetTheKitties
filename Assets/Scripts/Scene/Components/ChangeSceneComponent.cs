using Entitas;
using UnityEngine.SceneManagement;

public class ChangeSceneComponent : IComponent
{
    public string SceneName;
    public LoadSceneMode LoadSceneMode;
}