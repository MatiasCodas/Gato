using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class PushableTargetTransform : MonoBehaviour
    {
        private bool _pushing;
        private Vector3 _pushableTargetPosition;
        private GameObject _player;

        public bool Pushing => _pushing;

        public static Action OnPushed;

        private void Awake()
        {
            _pushing = false;
            _player = gameObject;
            CurseProjectile.OnPushing += PushingMovement;
        }

        private void OnDestroy()
        {
            CurseProjectile.OnPushing -= PushingMovement;
        }

        private void PushingMovement(Vector3 tipPosition)
        {
            _pushableTargetPosition = tipPosition;
        }

        private void Update()
        {
            if (_pushableTargetPosition != null && Keyboard.current.pKey.wasPressedThisFrame && !_pushing) // Temporary key
                _pushing = true;

            if (_pushing)
                _player.transform.position = Vector2.MoveTowards(_player.transform.position, _pushableTargetPosition, 1f);

            if (_player.transform.position == _pushableTargetPosition)
            {
                _pushing = false;
                OnPushed?.Invoke();
            }
        }
    }
}
