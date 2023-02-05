using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;
        public BattleStats BattleStats;
        public GameObject BasicEnemy;

        public List<Transform> SpawnPositions;
        private List<Transform> _unbundledSpawn;
        private List<GameObject> _activeEnemies;
        public float WaveSize;
        public float SpawnTime;

        private void Start()
        {
            Instance = this;
            StartCoroutine(SpawnNew());
            BattleStats.StartAtTime = 0;
            _unbundledSpawn = SpawnPositions;
            _activeEnemies = new List<GameObject>();
        }

        private void Update()
        {
            
        }

        public void EnemyDestroyed()
        {
            BattleStats.BasicEnemiesDefeated++;
            for (int i = 0; i < _activeEnemies.Count-1; i++)
            {
                if (_activeEnemies[i].gameObject == null)
                {
                    _activeEnemies.RemoveAt(i);
                    i--;
                }
            }

            if (_activeEnemies.Count == 0)
            {
                StartCoroutine(SpawnNew());
            }

        }
        

        private IEnumerator SpawnNew()
        {
            

            yield return new WaitForSeconds(SpawnTime);
            if (BattleStats.StartAtTime == 0) BattleStats.StartAtTime = Time.time;

            int randomPosition;
            for (int i = 1; i < WaveSize; i++)
            {
                randomPosition = Random.Range(0, _unbundledSpawn.Count-1);
                GameObject instance = Instantiate(BasicEnemy, _unbundledSpawn[randomPosition].position, Quaternion.identity);
                _activeEnemies.Add(instance);
                _unbundledSpawn.RemoveAt(randomPosition);
                Debug.Log(randomPosition);
            }
            _unbundledSpawn = SpawnPositions;
        }


    }
}
