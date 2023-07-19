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
            if (CurseWeapon.ProjectilePoolCounter == 1
                && _pullableCollider != null
                && !_pulling
                && Keyboard.current.oKey.wasPressedThisFrame) // Temporary key
                _pulling = true;

            if (_pulling)
            {
                _pullableCollider.gameObject.transform.SetParent(_pullableTransform);
                _pullableCollider.transform.localPosition = Vector2.MoveTowards(_pullableCollider.transform.localPosition, Vector2.zero, 1f);
                AudioManager.Instance.ToggleSFX(_audioSource, _ropePullableSFXLibrary.RopeDraggingSFX);
            }

            if (_pullableCollider != null
                && _pullableCollider.transform.localPosition == Vector3.zero)
            {
                // Temporary:
                _pullableCollider.transform.SetParent(_originalTransformParent);

                _pulling = false;
                _pullableCollider = null;
                _pullableTransform = null;
                OnPulled?.Invoke();
            }

            /*
            // Temporary:

            if (_pullableCollider != null && Keyboard.current.tKey.wasPressedThisFrame)
            {
                _pullableCollider.transform.SetParent(_originalTransformParent);
            }
            */
        }
    }
}
