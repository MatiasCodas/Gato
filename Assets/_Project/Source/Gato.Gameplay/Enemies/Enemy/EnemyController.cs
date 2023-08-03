using Gato.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public enum EnemyControlState { Spawning, Paused, None }
    public class EnemyController : MonoBehaviour
    {
        public EnemyControlState controllerState = EnemyControlState.None;
        public GameObject[] enemyPrefab;
        public ObjectPool[] enemyPool;
        public static List<Transform> rangeSpawn;
        public List<Transform> spawnPoints;
        public GameObject playerObj;
        public ObjectPool[] projectilePool;
        public FloatVariable initialCountdown;
        public FloatVariable difficulty;
        public float timeToRepeat = 5f;
        private float enemyCount;
        private GameObject instance;

        private void Awake()
        {
            timeToRepeat /= difficulty.value;

            rangeSpawn = spawnPoints;

            Invoke("SpawnEnemy", initialCountdown.value);
        }

        private void Update()
        {
            /*if (GameManager.isPaused)
            {
                controllerState = EnemyControlState.Paused;
                return;
            }
            else
            {
                if (controllerState == EnemyControlState.Paused)
                {
                    controllerState = EnemyControlState.None;
                }
            }*/

            if (controllerState == EnemyControlState.None)
            {
                controllerState = EnemyControlState.Spawning;
                Invoke("SpawnEnemy", timeToRepeat);
            }
        }

        public void SpawnEnemy()
        {
            // if (GameManager.isPaused) return;

            int index;
            //Criar um inimigo King a cada 5 inimigos criados
            if (enemyCount % 5 == 0 && enemyCount != 0) { index = 3; }
            else
            {
                index = Random.Range(0, enemyPrefab.Length - 1);
            }

            if (enemyPool[index].objPool.Count == 0)
            {
                instance = Instantiate(enemyPrefab[index], new Vector3(0, 0, 0), Quaternion.identity, enemyPool[index].transform);
            }
            else
            {
                instance = enemyPool[index].objPool[0];
                enemyPool[index].objPool.Remove(enemyPool[index].objPool[0]);
            }

            SetEnemy(index);
            enemyCount++;

            Invoke("SpawnEnemy", timeToRepeat);
        }

        private void SetEnemy(int index)
        {
            switch (index)
            {
                //para os inimigos que usam armas a distância
                case 0:
                case 1:
                    Enemy rangeEnemy = instance.GetComponent<Enemy>();
                    int spawnIndex = Random.Range(0, rangeSpawn.Count);

                    rangeEnemy.StartEnemy(rangeSpawn[spawnIndex].transform.position, enemyPool[index], playerObj);

                    rangeSpawn.Remove(rangeSpawn[spawnIndex]);

                    RangeWeapon rangeWeapon = instance.GetComponent<RangeWeapon>();
                    rangeWeapon.pool = projectilePool[index];
                    break;

                //para inimigos de ataque corpo a corpo
                case 2:
                case 3:
                    Enemy meleeEnemy = instance.GetComponent<Enemy>();
                    Vector3 spawnPos = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));
                    meleeEnemy.StartEnemy(spawnPos, enemyPool[index], playerObj);
                    break;
            }

            instance.SetActive(true);
        }
    }
}
