using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "MovementConstantsConfiguration",
        menuName = "Configurations/MovementConstantsConfiguration")]
    public class MovementConstantsConfiguration : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Threshold of the velocity on the x-axis at which the character's velocity will be set to 0")]
        public float MovementEndThresholdX = 0.05f;

        [SerializeField, Tooltip("Time after which the jump input is available again after pressing it")]
        public float TimeUntilJumpInputAvailable = 0.01f;

        [SerializeField, Tooltip("Time after which the pause input is available again after pressing it")]
        public float TimeUntilPauseInputAvailable = 0.5f;
    }
}