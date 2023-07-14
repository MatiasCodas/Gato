using Gato.Core;
using Gato.Audio;
using System;
using UnityEngine;

namespace Gato.Gameplay
{
    public class InputControlManager : MonoSystem
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        private const string HorizontalTurnAxisName = "HorizontalTurn";
        private const string VerticalTurnAxisName = "VerticalTurn";
        private const string LeftTriggerAxisName = "LeftTrigger";
        private const string RightTriggerAxisName = "RightTrigger";


        [Header("Input Settings")]
        [SerializeField]
        private InputCodeSettings _inputSettings;

        [Space(10)]
        [Header("Audio Settings")]
        [SerializeField]
        private PlayerAudio _playerAudio;

        private Vector2 _direction;
        private Vector2 _directionWeapon = new Vector2(0, -1);
        private IPlayerControlService _playerControlSystem;

        private bool _dashHalfPress; //Variável criada pra evitar do player só deixar o dash pressionado e dar vários dashes
        private bool _leftTriggerPressed;
        private bool _rightTriggerPressed;

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

            if (_direction != Vector2.zero)
                AudioManager.Instance.ToggleSFX(_playerAudio.PlayerAudioSource, _playerAudio.PlayerSFX.WalkSFX, true);
            else
                AudioManager.Instance.ToggleSFX(_playerAudio.PlayerAudioSource, _playerAudio.PlayerSFX.WalkSFX, false);

            if (!Input.GetKey(_inputSettings.DashKeyCode) && !Input.GetKey(_inputSettings.DashKeyCodeGamepad)) { 
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

            _directionWeapon = new Vector2(Input.GetAxis(HorizontalTurnAxisName), Input.GetAxis(VerticalTurnAxisName));
            _playerControlSystem.WeaponAim(_directionWeapon);

            if (Input.GetKeyDown(_inputSettings.ShootWeaponKeyCode) || (Input.GetAxis(RightTriggerAxisName) > 0.5f && !_rightTriggerPressed))
            {
                _playerControlSystem.ShootWeapon(_directionWeapon);
                _rightTriggerPressed = true;
            }

            if(Input.GetKeyDown(_inputSettings.RecoverWeaponKeyCode) || (Input.GetAxis(LeftTriggerAxisName) > 0.5f && !_leftTriggerPressed))
            {
                _playerControlSystem.RecoverWeapon();
                _leftTriggerPressed = true;
            }

            if (Input.GetAxis(LeftTriggerAxisName) < 0.5f) _leftTriggerPressed = false;
            if (Input.GetAxis(RightTriggerAxisName) < 0.5f) _rightTriggerPressed = false;
        }
    }
}
