using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    public class InputControlManager : MonoSystem
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        [SerializeField]
        private InputCodeSettings _inputSettings;

        private Vector2 _directionWeapon = new Vector2(0, -1);
        private IPlayerControlService _playerControlSystem;

        public override void Setup()
        {
            if (!ServiceLocator.Shared.TryGet(out _playerControlSystem))
            {
                return;
            }
        }
        public void FixedUpdate()
        {
            Vector2 direction = new Vector2(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));

            if (direction != Vector2.zero)
            {
                _playerControlSystem.Move(direction);

                if (direction.x > 0.5f || direction.y > 0.5f || direction.x < -0.5f || direction.y < -0.5f)
                {
                    _directionWeapon = direction;
                }
            }
        }
        public override void Tick(float deltaTime)
        {
            if(_playerControlSystem == null)
            {
                _playerControlSystem = ServiceLocator.Shared.Get<IPlayerControlService>();
            }

            

            if (Input.GetKey(_inputSettings.ShootWeaponKeyCode))
            {
                _playerControlSystem.ShootWeapon(_directionWeapon);
            }
        }
    }
}
