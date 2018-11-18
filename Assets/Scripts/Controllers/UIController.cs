using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController controller;

    private void Awake()
    {
        if (controller == null)
        {
            //DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}