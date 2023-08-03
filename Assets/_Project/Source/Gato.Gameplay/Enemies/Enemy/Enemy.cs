using Cysharp.Threading.Tasks;
using Gato.Core;
using Pathfinding;
using UnityEngine.AI;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Enemy : Character
    {
        private bool _cursed;
        private bool _cooldown;
        private bool? _strafeClockwise;
        private Vector3 _velocity;
        private Vector2 _variationMomentum;
        private Vector2 _previousPosition;
        private Vector2 _nextPosition = Vector2.zero;
        private Rigidbody2D _rb;
        private GameObject _player;
        private Transform _spawn;
        private CapsuleCollider _capsuleCollider;
        private ObjectPool _enemyPool;
        private BotController _botController;
        private AIDestinationSetter _navMeshAgent;

        private void Start()
        {
            Initalize();
        }

        private void Initalize()
        {
            _rb = GetComponent<Rigidbody2D>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _botController = GetComponent<BotController>();
            _navMeshAgent = GetComponent<AIDestinationSetter>();
            _player = PlayerControlSystem.Player.gameObject;
            _botController.SetPlayer(_player.transform);

            maxLife *= 1;
            life = maxLife;
        }

        //TODO: Setar a infos necessárias para os inimigos, vindo de um controller ou de um system, para poder controlar melhor e reutilizar as instancias
        public void StartEnemy(Vector3 spawnPos, ObjectPool enemyPool, GameObject player)
        {
            gameObject.transform.position = spawnPos;
            _enemyPool = enemyPool;
            _player = player;
            _botController.SetPlayer(_player.transform);
            life = maxLife;
        }

        public void Follow()
        {
            if (/*GameManager.isPaused || */ life <= 0)
            {
                _navMeshAgent.target = gameObject.transform;

                return;
            }

            _navMeshAgent.target = _player.transform;
        }

        public async void MosquitoFollow()
        {
            await UniTask.WaitUntil(() => ! _cooldown);

            Vector2 randomized = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 0.02f;
            _variationMomentum = Vector2.ClampMagnitude(_variationMomentum + randomized, 0.1f);
            _nextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(_player.transform.position - transform.position, 2);
            _nextPosition = new Vector2(_nextPosition.x + _variationMomentum.x, _nextPosition.y + _variationMomentum.y);
            _rb.MovePosition(_nextPosition);
            Cooldown(1);

            // TODO: tentar usar o navmesh no futuro ao inves do rb
            // _navMeshAgent.target = _nextPosition;
        }

        public void StrafeFollow()
        {
            if (_strafeClockwise == null)
            {
                _strafeClockwise = Random.Range(0, 1) != 0;
            }

            Vector2 perpendicularMovement = Vector2.Perpendicular( Vector2.ClampMagnitude(_player.transform.position - transform.position, 0.02f));
            _nextPosition = (Vector2)transform.position + (_strafeClockwise.Value ? perpendicularMovement : -perpendicularMovement);

            _variationMomentum = Vector2.ClampMagnitude(_variationMomentum + _nextPosition, 1);
            _rb.MovePosition(_nextPosition);
        }


        public void Aim()
        {
            // _enemy.LookAt(_player.transform);
        }

        public void Die()
        {
            _enemyPool.ReturnToPool(gameObject);

            DropController.Instance.DropItem(transform.position);

            if (_botController.EnemyType == EnemyType.Range)
            {
                EnemyController.rangeSpawn.Add(_spawn);
            }

            _capsuleCollider.enabled = false;
        }

        private async UniTask Cooldown(int duration)
        {
            _cooldown = true;

            await UniTask.Delay(duration * 1000);

            _cooldown = false;
        }
    }
}
