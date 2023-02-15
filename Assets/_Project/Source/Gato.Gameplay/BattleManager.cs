using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Gato.Gameplay
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;
        
        public GameObject BasicEnemy;

        public List<Transform> SpawnPositions;
        private List<Vector2> _unbundledSpawn;
        private List<GameObject> _activeEnemies;
        public float WaveSize;
        public float SpawnTime;


        [Header( "HUD info")]
        public BattleStats BattleStats;
       // public TMP_Text EnemiesDefeatedText;

        private void Start()
        {
            Instance = this;
            StartCoroutine(SpawnNew());
            BattleStats.StartAtTime = 0;
            GetSpawnPositions();
            _activeEnemies = new List<GameObject>();
        }

        private void Update()
        {
       //     EnemiesDefeatedText.text = "Enemies defeated: " + BattleStats.BasicEnemiesDefeated;
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

            if (_activeEnemies.Count <= 1)
            {
                StartCoroutine(SpawnNew());
            }

        }

        private void GetSpawnPositions()
        {
            _unbundledSpawn = new List<Vector2>();
            for (int i = 0; i < SpawnPositions.Count; i++)
            {
                _unbundledSpawn.Add(SpawnPositions[i].position);
            }
        }
        

        private IEnumerator SpawnNew()
        {
            yield return new WaitForSeconds(SpawnTime);
            if (BattleStats.StartAtTime == 0) BattleStats.StartAtTime = Time.time;

            int randomPosition;
            for (int i = 0; i < WaveSize; i++)
            {
                randomPosition = Random.Range(0, _unbundledSpawn.Count);
                //Debug.Log(randomPosition);
                GameObject instance = Instantiate(BasicEnemy, _unbundledSpawn[randomPosition], Quaternion.identity);
                _activeEnemies.Add(instance);
                _unbundledSpawn.RemoveAt(randomPosition);
            }
            GetSpawnPositions();
            Debug.Log(_unbundledSpawn.Count);
            Debug.Log(SpawnPositions.Count);
        }


    }
}
