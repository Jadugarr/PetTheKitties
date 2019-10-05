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
    }
}