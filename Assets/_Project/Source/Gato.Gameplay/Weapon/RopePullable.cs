using Gato.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    [RequireComponent(typeof(AudioSource))]
    public class RopePullable : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RopePullableSFXLibrary _ropePullableSFXLibrary;

        private bool _pulling;
        private Transform _originalTransformParent;
        private Collision2D _pullableCollider;
        private Transform _pullableTransform;

        public static Action OnPulled;

        private void Awake()
        {
            _pulling = false;
            _originalTransformParent = transform.parent;
            CurseProjectile.OnPulling += HookPullMovement;
        }

        private void OnDestroy()
        {
            CurseProjectile.OnPulling -= HookPullMovement;
        }

        private void HookPullMovement(Collision2D pullableCollider, Transform pullableTransform)
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
                AudioManager.Instance.ToggleSFX(_audioSource, _ropePullableSFXLibrary.RopeDraggingSFX);
            }

            if (transform.localPosition == Vector3.zero)
            {
                _pulling = false;
                _pullableCollider = null;
                _pullableTransform = null;
                OnPulled?.Invoke();

                // Temporary:
                transform.SetParent(_originalTransformParent);
            }

            /*
            // Temporary:

            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                transform.SetParent(_originalTransformParent);
            }
            */
        }
    }
}
