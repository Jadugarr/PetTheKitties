using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Entitas.Test
{
    public class RestartSceneBehaviour : MonoBehaviour
    {
        [SerializeField] private Button _restartSceneButton;

        private void Awake()
        {
            _restartSceneButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnRestartClicked()
        {
            
        }
    }
}