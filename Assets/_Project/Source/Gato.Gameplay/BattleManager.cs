using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gato.Gameplay
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;
        
        public GameObject BasicEnemy;

        public List<Transform> SpawnPositions;
        private List<Vector2> _unbundledSpawn;
        [SerializeField]
        private List<GameObject> _activeEnemies;
        private BoxCollider2D _collider2D;
        public float WaveSize;
        public float SpawnTime;
        private bool _activated = false;

        public GameObject BlessingPrefab;
        public GameObject[] Curses;

        [Header( "HUD info")]
        public BattleStats BattleStats;
       // public TMP_Text EnemiesDefeatedText;

        private void Start()
        {
            Instance = this;
            //_collider2D = GetComponent<BoxCollider2D>();
            _activeEnemies = new List<GameObject>();

            foreach (GameObject item in Curses)
            {
                item.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player") || _activated) return;
            _activated = true;
            Instance = this;
            StartCoroutine(SpawnNew());
            BattleStats.StartAtTime = 0;
            GetSpawnPositions();
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
                if(WaveSize > 0)
                StartCoroutine(SpawnNew());
                else
                {
                    FinishedArea();
                }
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
            WaveSize -= _activeEnemies.Count;
            GetSpawnPositions();
        }

        private void FinishedArea()
        {
            
            foreach (GameObject item in Curses)
            {
                Instantiate(BlessingPrefab, item.transform.position, Quaternion.identity, transform).transform.parent = transform.parent;
                Destroy(item);
            }
            Destroy(gameObject);
        }

        private IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(1);
            
        }
    }
}
