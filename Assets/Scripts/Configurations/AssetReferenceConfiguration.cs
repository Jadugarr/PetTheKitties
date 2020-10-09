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
    }
}