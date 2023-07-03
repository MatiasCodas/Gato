using Cysharp.Threading.Tasks;
using Gato.Core;
using Gato.UI;
using System;
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
        private CurseWeapon _curseWeapon;

        public ServiceLocator OwningLocator { get; set; }

        public override void Setup()
        {
            ServiceLocator.Shared.Set<IPlayerControlService>(this);
            _rangedWeapon = gameObject.GetComponent<IRangedWeapon>();
            _rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
            _curseWeapon = gameObject.GetComponent<CurseWeapon>();
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

        public void WeaponAim(Vector2 direction)
        {
            if (direction != new Vector2(0, 0))
            {
                _curseWeapon.Aim("Controller", direction + (Vector2)transform.position);
                return;
            }
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _curseWeapon.Aim("Mouse", direction);

        }

        public void EnemyHit()
        {
            if (_isDashing) return;

            if (_playerStats.HitPoints + 1 < _playerStats.MaxHP)
            {
                _playerStats.HitPoints += 1;
            }
            else
            {
                _playerStats.HitPoints = 0; // Value will be replaced by the stats set in the Game Design SO
                _playerStats.MaxHP = 3; // Value will be replaced by the stats set in the Game Design SO
                Destroy(gameObject);
            }
        }

        public void ShootWeapon(Vector2 direction)
        {
            if (_isDashing)
            {
                return;
            }

            if (direction == new Vector2(0, 0)) 
            {
                direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            }
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
