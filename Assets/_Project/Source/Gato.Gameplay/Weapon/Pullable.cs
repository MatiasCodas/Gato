using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class Pullable : MonoBehaviour
    {
        private bool _pulling;
        private UnityEngine.Transform _originalTransformParent;

        private void Awake()
        {
            _pulling = false;
            _originalTransformParent = transform.parent;
            CurseProjectile.OnPulling += PullingMovement;
        }

        private void OnDestroy()
        {
            CurseProjectile.OnPulling -= PullingMovement;
        }

        private void PullingMovement()
        {
            _pulling = true;
        }

        private void Update()
        {
            if (_pulling)
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, 1f);

            if (transform.localPosition == Vector3.zero)
                _pulling = false;

            // Temporary for tests:
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                transform.SetParent(_originalTransformParent);
                // Testing movement:
                // transform.position = Vector2.MoveTowards(transform.localPosition, transform.localPosition + Vector3.one, 1f);
            }
        }
    }
}
