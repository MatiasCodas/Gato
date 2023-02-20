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
        private IRangedWeapon _rangedWeapon;
        private Rigidbody2D _rigidbody2d;

        public ServiceLocator OwningLocator { get; set; }

        public override void LateSetup()
        {
            ServiceLocator.Shared.Set<IPlayerControlService>(this);
            _rangedWeapon = gameObject.GetComponent<IRangedWeapon>();
            _rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
            Player = this;
            _canDash = true;
        }

        public void Move(Vector2 direction)
        {
           _rigidbody2d.velocity = (direction * _playerStats.MovementSpeed);
        }

        public void Dash(Vector2 direction)
        {
            if (!_canDash)
            {
                return;
            }

            DashAsync(direction);
        }

        public void ShootWeapon(Vector2 direction)
        {
            _rangedWeapon.ThrowWeapon(direction);
        }

        private async UniTask DashAsync(Vector2 direction)
        {
            _canDash = false;

             _rigidbody2d.AddForce(direction * _playerStats.DashSpeed);
            
            Debug.Log("DASH");
            await UniTask.Delay((int)(_playerStats.DashCooldown * 1000));

            _canDash = true;
        }
    }
}
