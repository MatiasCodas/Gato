using Cysharp.Threading.Tasks;
using Gato.Core;
using UnityEngine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Gato.Gameplay
{
    internal class SpawnSystem : MonoSystem, ISpawnService
    {
        [SerializeField]
        private PlayerSpawnSettings _playerSpawnSettings;
        [SerializeField]
        private Transform[] _playerSpawnPosition;

        public ServiceLocator OwningLocator { get; set; }

        public void SpawnPlayer()
        {
            SpawnAsync(_playerSpawnSettings.PlayerPrefabReference, _playerSpawnPosition[0].position);
        }

        private async UniTask SpawnAsync(AssetReference assetReference, Vector3 position)
        {
            AsyncOperationHandle<GameObject> operationHandle = Addressables.InstantiateAsync(assetReference, position, Quaternion.identity);
        }
    }
}
