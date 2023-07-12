using Gato.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class DropController : MonoBehaviour
    {
        public static DropController Instance;
        [SerializeField]
        private GameObject[] dropPrefab;
        [SerializeField]
        private ObjectPool[] dropPool;

        private void Awake()
        {
            Instance = this;
        }

        public void DropItem(Vector3 pos)
        {
            int index = Random.Range(0, 2);

            if (dropPool[index].objPool.Count == 0)
            {
                CreateNewDrop(dropPrefab[index], pos, index);
            }
            else
            {
                UseFromPool(pos, index);
            }
        }

        private void CreateNewDrop(GameObject drop, Vector3 pos, int index)
        {
            GameObject instance = Instantiate(drop, new Vector3(pos.x, pos.y + 1, pos.z), Quaternion.identity);
            EnemyDrop enemyDrop = instance.GetComponent<EnemyDrop>();
            enemyDrop.pool = dropPool[index];
        }

        private void UseFromPool(Vector3 pos, int index)
        {
            GameObject instance = dropPool[index].objPool[0];
            dropPool[index].objPool.Remove(instance);
            instance.SetActive(true);
            instance.transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
        }
    }
}
