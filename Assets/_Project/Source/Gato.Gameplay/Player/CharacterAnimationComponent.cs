using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationComponent : MonoBehaviour
    {
        private const string HorizontalAnimatorParameter = "Horizontal";
        private const string VerticalAnimatorParameter = "Vertical";
        private int _animVertHash;
        private int _animHorizHash;

        [SerializeField]
        private Animator _animator;

        public void Awake()
        {
            _animVertHash = Animator.StringToHash(VerticalAnimatorParameter);
            _animHorizHash = Animator.StringToHash(HorizontalAnimatorParameter);
        }

        public void FixedUpdate()
        {
            _animator.SetFloat(_animVertHash, Input.GetAxis("Vertical"));
            _animator.SetFloat(_animHorizHash, Input.GetAxis("Horizontal"));
        }
    }
}
