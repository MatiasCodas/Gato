using Gato.Core;
using UnityEngine.AI;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Enemy : Character
    {
        [SerializeField]
        private Transform enemy;
        [HideInInspector]
        public GameObject player;
        [SerializeField]
        private float moveSpeed = 10f;
        [SerializeField]
        private float gravity = -10f;
        private ObjectPool enemyPool;
        private Vector3 velocity;
        [HideInInspector]
        public CapsuleCollider capsuleCollider;
        [SerializeField]
        private BotController botController;

        public SkinnedMeshRenderer mesh;
        public Material[] materials;
        [HideInInspector]
        public Transform spawn;

        public NavMeshAgent navMeshAgent;
        public FloatVariable difficulty;

        private void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
            botController = GetComponent<BotController>();

            maxLife *= difficulty.value;
            life = maxLife;
        }

        //Setar a infos necessárias para os inimigos
        public void StartEnemy(Vector3 _spawnPos, ObjectPool _enemyPool, GameObject _player)
        {
            gameObject.transform.position = _spawnPos;
            enemyPool = _enemyPool;
            player = _player;
            botController.player = player.transform;
            life = maxLife;

            int materialIndex = Random.Range(0, materials.Length);
            mesh.material = materials[materialIndex];
        }

        public void Follow()
        {
            if (/*GameManager.isPaused || */ life <= 0)
            {
                navMeshAgent.destination = gameObject.transform.position;

                return;
            }

            navMeshAgent.destination = player.transform.position;
            Vector3 target = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
            enemy.LookAt(target);
        }

        public void Aim()
        {
            enemy.LookAt(player.transform);
        }

        public void Die()
        {
            enemyPool.ReturnToPool(gameObject);

            DropController.Instance.DropItem(this.transform.position);

            if (botController.enemyType == EnemyType.Range)
            {
                EnemyController.rangeSpawn.Add(spawn);
            }

            capsuleCollider.enabled = false;
        }
    }
}
