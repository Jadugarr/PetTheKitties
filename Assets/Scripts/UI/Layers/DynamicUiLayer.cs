using UnityEngine;

public class DynamicUiLayer : MonoBehaviour
{
    public void Awake()
    {
        UIService.RegisterDynamicLayer(gameObject);
    }
}