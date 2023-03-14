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

        private Vector2 _direction;
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
            if (!ServiceLocator.Shared.TryGet(out _playerControlSystem))
            {
                return;
            }

            _direction = new Vector2(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));

            if (Input.GetKey(_inputSettings.DashKeyCode) && _directionWeapon != Vector2.zero)
            {
                _playerControlSystem.Dash(_directionWeapon);

                return;
            }

            _playerControlSystem.Move(_direction);
            _directionWeapon = _direction != Vector2.zero ?_direction : _directionWeapon;
            _directionWeapon.Normalize();
            /*if (_direction.x > 0.5f || _direction.y > 0.5f || _direction.x < -0.5f || _direction.y < -0.5f)
            {
                _directionWeapon = _direction;
            }*/
        }
        public override void Tick(float deltaTime)
        {
            if(_playerControlSystem == null)
            {
                _playerControlSystem = ServiceLocator.Shared.Get<IPlayerControlService>();
            }

            if (Input.GetKeyDown(_inputSettings.ShootWeaponKeyCode) && _directionWeapon != Vector2.zero)
            {
                Debug.LogError(_directionWeapon);
                _playerControlSystem.ShootWeapon(_directionWeapon);
            }
        }
    }
}
