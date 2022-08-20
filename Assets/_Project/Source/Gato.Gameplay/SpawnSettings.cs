using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gato.Gameplay
{
    public class SpawnSettings : ScriptableObject, ISpawnSettings
    {
        [SerializeField]
        private ISpawnInstance _spawnInstance;

        public ISpawnInstance Instance()
        {
            return _spawnInstance;
        } 
    }
}
