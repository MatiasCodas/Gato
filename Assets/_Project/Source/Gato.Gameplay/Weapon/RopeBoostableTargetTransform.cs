using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class RopeBoostableTargetTransform : MonoBehaviour
    {
        private bool _boosting;
        private Vector3 _boostableTargetPosition;
        private GameObject _player;

        public bool Pushing => _boosting;

        public static Action OnBoosted;

        private void Awake()
        {
            _boosting = false;
            _player = gameObject;
            CurseProjectile.OnBoosting += RopeBoostingMovement;
        }

        private void OnDestroy()
        {
            CurseProjectile.OnBoosting -= RopeBoostingMovement;
        }

        private void RopeBoostingMovement(Vector3 ropeTipPosition)
        {
            _boostableTargetPosition = ropeTipPosition;
        }

        private void Update()
        {
            if (_boostableTargetPosition != null && Keyboard.current.pKey.wasPressedThisFrame && !_boosting) // Temporary key
                _boosting = true;

            if (_boosting)
                _player.transform.position = Vector2.MoveTowards(_player.transform.position, _boostableTargetPosition, 1f);

            if (_player.transform.position == _boostableTargetPosition)
            {
                _boosting = false;
                OnBoosted?.Invoke();
            }
        }
    }
}
