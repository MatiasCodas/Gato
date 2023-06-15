using Cysharp.Threading.Tasks;
using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    internal class PlayerControlSystem : MonoSystem, IPlayerControlService
    {
        [SerializeField]
        private PlayerStats _playerStats;
        public static PlayerControlSystem Player;

        private bool _canDash = true;
        private bool _canWalk = true;
        private bool _isDashing = false;
        private IRangedWeapon _rangedWeapon;
        private Rigidbody2D _rigidbody2d;

        public ServiceLocator OwningLocator { get; set; }

        public override void Setup()
        {
            ServiceLocator.Shared.Set<IPlayerControlService>(this);
            _rangedWeapon = gameObject.GetComponent<IRangedWeapon>();
            _rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
            Player = this;
        }

        public void Dash(Vector2 direction)
        {
            if (!_canDash)
            {
                return;
            }

            DashAsync(direction);
        }

        

        public void Move(Vector2 direction)
        {
            if (!_canWalk)
            {
                return;
            }

            _rigidbody2d.MovePosition(_rigidbody2d.position + (direction * _playerStats.MovementSpeed * Time.fixedDeltaTime));
        }

        public void EnemyHit()
        {
            if (_isDashing) return;
            Destroy(gameObject);

        }

        public void ShootWeapon()
        {
            if (_isDashing)
            {
                return;
            }

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - (Vector2)transform.position;
            direction = direction.normalized;

            _rangedWeapon.ThrowWeapon(direction);
        }

        public void RecoverWeapon()
        {
            CurseProjectile.AllRopesComeBack();
        }

        public PlayerStats FetchPlayerStats()
        {
            return _playerStats;
        }

        private async UniTask DashAsync(Vector2 direction)
        {
            _isDashing = true;
            _canDash = false;
            _canWalk = false;
            _rigidbody2d.velocity = (direction.normalized * _playerStats.DashSpeed);
            await UniTask.Delay((int)(_playerStats.DashTime * 1000));
            _rigidbody2d.velocity = Vector2.zero;
            _canWalk = true;

            await UniTask.Delay(250);
            _isDashing = false;

            await UniTask.Delay((int)(_playerStats.DashCooldown * 1000));

            _canDash = true;
        }
    }
}
