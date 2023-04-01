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

        private bool _dashHalfPress; //Variável criada pra evitar do player só deixar o dash pressionado e dar vários dashes

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

            
            _playerControlSystem.Move(_direction);
            if (!Input.GetKey(_inputSettings.DashKeyCode)) { 
                _dashHalfPress = false;
                return;
            }

            if (_direction == Vector2.zero || _dashHalfPress) return;
            _playerControlSystem.Dash(_direction);
            _dashHalfPress = true;

        }

        public override void Tick(float deltaTime)
        {
            if(_playerControlSystem == null)
            {
                _playerControlSystem = ServiceLocator.Shared.Get<IPlayerControlService>();
            }

            if (Input.GetKeyDown(_inputSettings.ShootWeaponKeyCode))
            {
                _playerControlSystem.ShootWeapon();
            }
        }
    }
}
