using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    internal class PlayerControlSystem : MonoSystem, IPlayerControlService
    {
        [SerializeField]
        private PlayerStats _playerStats;
        public static PlayerControlSystem Player;

        private IRangedWeapon _rangedWeapon;
        private Rigidbody2D _rigidbody2d;

        public ServiceLocator OwningLocator { get; set; }

        public override void LateSetup()
        {
            ServiceLocator.Shared.Set<IPlayerControlService>(this);
            _rangedWeapon = gameObject.GetComponent<IRangedWeapon>();
            _rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
            Player = this;
        }

        public void Move(Vector2 direction)
        {
           _rigidbody2d.velocity = (direction * _playerStats.MovementSpeed);
        }

        public void ShootWeapon(Vector2 direction)
        {
            _rangedWeapon.ThrowWeapon(direction);
        }
    }
}
