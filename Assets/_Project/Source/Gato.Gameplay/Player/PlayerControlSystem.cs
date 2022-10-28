using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    internal class PlayerControlSystem : MonoSystem, IPlayerControlService
    {
        [SerializeField]
        private float _movementSpeed;

        private IRangedWeapon _rangedWeapon;

        public ServiceLocator OwningLocator { get; set; }

        public override void LateSetup()
        {
            ServiceLocator.Shared.Set<IPlayerControlService>(this);
            _rangedWeapon = gameObject.GetComponent<IRangedWeapon>();
        }

        public void Move(Vector2 direction)
        {
            transform.Translate(direction * _movementSpeed);
        }

        public void ShootWeapon(Vector2 direction)
        {
            _rangedWeapon.ThrowWeapon(direction);
        }
    }
}
