using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configurations
{
    [Serializable]
    [CreateAssetMenu(fileName = "AssetReferenceConfiguration", menuName = "Configurations/AssetReferenceConfiguration")]
    public class AssetReferenceConfiguration : ScriptableObject
    {
        #region Characters

        [Header("Characters")] public AssetReference WorldPlayerReference;
        public AssetReference KittyReference;

        #endregion

        #region Levels

        [Header("Levels")] public AssetReference[] Levels;

        #endregion


        #region Particles

        [Header("Particles")] public AssetReference HitGroundParticles;

        #endregion

        #region Misc Components

        [Header("Misc Components")] public AssetReference GrapplingHookReticle;
        public AssetReference GrapplingHookLine;

        #endregion

        #region Ui elements

        [Header("UI Elements")] public AssetReference BattleResultWidget;
        public AssetReference PauseOverlay;
        public AssetReference MainMenu;

        #endregion
    }
}