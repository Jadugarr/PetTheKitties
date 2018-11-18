using UnityEngine;

public class OverlayUiLayer : MonoBehaviour
{
    public void Awake()
    {
        UIService.RegisterOverlayLayer(gameObject);
    }
}