using System;
using UnityEngine;

namespace Configurations
{
    [Serializable]
    [CreateAssetMenu(fileName ="SpawnConfiguration", menuName ="Configurations/SpawnConfiguration")]
    public class SpawnConfiguration : ScriptableObject
    {
        [SerializeField]
        private Transform playerSpawn;

        [SerializeField]
        private Transform[] enemySpawns; 

        public Transform PlayerSpawn
        {
            get
            {
                return playerSpawn;
            }
        }

        public Transform[] EnemySpawns
        {
            get
            {
                return enemySpawns;
            }
        }
    }
}
