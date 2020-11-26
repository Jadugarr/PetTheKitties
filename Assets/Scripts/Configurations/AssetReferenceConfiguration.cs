using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configurations
{
    [Serializable]
    [CreateAssetMenu(fileName = "AssetReferenceConfiguration", menuName = "Configurations/AssetReferenceConfiguration")]
    public class AssetReferenceConfiguration : ScriptableObject
    {
        public AssetReference WorldPlayerReference;
        public AssetReference KittyReference;

        #region Levels

        public AssetReference[] Levels;

        #endregion

        #region Particles

        public AssetReference HitGroundParticles;

        #endregion

        #region Ui elements

        public AssetReference BattleResultWidget;
        public AssetReference PauseOverlay;
        public AssetReference MainMenu;

        #endregion
    }
}