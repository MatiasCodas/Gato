using Gato.Core;
using Gato.Audio;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class InputControlManager : MonoSystem
    {

        [SerializeField]
        private InputCodeSettings _inputSettings;
        [SerializeField]
        private InputActionReference _movement, _dash, _mousePosition, _aimPosition, _ropePlus, _ropeMinus, _ropeAction1, _ropeAction2;
        public bool MenuOpenCloseInput { get; private set; }

        private Vector2 _direction;
        private Vector2 _directionWeapon = new Vector2(0, -1);
        private IPlayerControlService _playerControlSystem;

        private bool _dashHalfPress; //Variável criada pra evitar do player só deixar o dash pressionado e dar vários dashes
        private bool _mouseAiming;
        private Vector2 _previousMousePos;

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

            _direction = _movement.action.ReadValue<Vector2>();

            _playerControlSystem.Move(_direction);

            if (!_dash.action.IsPressed()){
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

            _directionWeapon = MouseOrGamepad();
            _playerControlSystem.WeaponAim(_directionWeapon);

            if (_ropePlus.action.IsPressed())
            {
                _playerControlSystem.ShootWeapon(_directionWeapon);
            }

            if(_ropeMinus.action.IsPressed() )
            {
                _playerControlSystem.RecoverWeapon();
            }

            MenuOpenCloseInput = _ropePlus.action.WasPressedThisFrame();
        }


        private Vector2 MouseOrGamepad()
        {
            Vector2 mousePos = _mousePosition.action.ReadValue<Vector2>();
            if (mousePos != _previousMousePos || _aimPosition.action.IsInProgress())
            {
                _mouseAiming = !_aimPosition.action.IsInProgress();
            }
            _previousMousePos = mousePos;
            if (!_mouseAiming)
            {
                return _aimPosition.action.ReadValue<Vector2>();
            }

            return Camera.main.ScreenToWorldPoint(_mousePosition.action.ReadValue<Vector2>());
        }
    }
}
