using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "Gato/Settings/PlayerSpawnSystemSettings", fileName = "PlayerSpawnSystemSettings")]
    public class PlayerSpawnSettings : SpawnSettings
    {
        [SerializeField]
        private AssetReference _playerPrefabReference;

        public AssetReference PlayerPrefabReference => _playerPrefabReference;
    }
}
