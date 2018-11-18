using UnityEngine;

public class StaticUiLayer : MonoBehaviour
{
    public void Awake()
    {
        UIService.RegisterStaticLayer(gameObject);
    }
}
