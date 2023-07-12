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
        private Transform _originalTransformParent;
        private Collision2D _pullableCollider;
        private Transform _pullableTransform;

        public static Action OnPulled;

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

        private void PullingMovement(Collision2D pullableCollider, Transform pullableTransform)
        {
            _pullableCollider = pullableCollider;
            _pullableTransform = pullableTransform;
        }

        private void Update()
        {
            if (_pullableCollider != null && Keyboard.current.oKey.wasPressedThisFrame && !_pulling) // Temporary key
                _pulling = true;

            if (_pulling)
            {
                _pullableCollider.gameObject.transform.SetParent(_pullableTransform);
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero, 1f);
            }

            if (transform.localPosition == Vector3.zero)
            {
                _pulling = false;
                _pullableCollider = null;
                _pullableTransform = null;
                OnPulled?.Invoke();
            }

            // Temporary for tests:
            if (Keyboard.current.tKey.wasPressedThisFrame) // Temporary key
            {
                transform.SetParent(_originalTransformParent);
                // Testing movement:
                // transform.position = Vector2.MoveTowards(transform.localPosition, transform.localPosition + Vector3.one, 1f);
            }
        }
    }
}
