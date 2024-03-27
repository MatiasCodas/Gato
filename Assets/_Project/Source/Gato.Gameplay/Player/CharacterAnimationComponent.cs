using Gato.Core;
using UnityEngine;
using Spine.Unity;

namespace Gato.Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationComponent : MonoBehaviour
    {
       
        private Vector2 _direction;

        [SerializeField]
        private SkeletonAnimation _spineSkel;

        [SpineAnimation]
        public string[] IdleAnimationName; // using indexes 0 as back, 1 as front and 2 as side

        [SpineAnimation]
        public string[] WalkAnimationName;

        [SpineAnimation]
        public string[] DashAnimationName;

        public Spine.AnimationState AnimationState;
        public Spine.Skeleton Skeleton;

        public void Start()
        {
            AnimationState = _spineSkel.AnimationState;
            Skeleton = _spineSkel.skeleton;
            AnimationState.SetAnimation(0, IdleAnimationName[1], true);
        }

        public void Walking(Vector2 direction)
        {
            if (_direction == direction) return;
            if ((_direction.x * direction.x > 0) && (_direction.y * direction.y > 0)) return;
            _direction = direction;
            InvertWhenLeft();
            FaceDirection();
            WalkOrIdle(direction);
        }

        public void Dashing(Vector2 direction)
        {
            _direction = direction;
            InvertWhenLeft();
            FaceDirection();
            AnimationState.SetAnimation(0, DashAnimationName[_faceDirection], true);
        }

        public void WalkOrIdle(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                AnimationState.SetAnimation(0, IdleAnimationName[_faceDirection], true);
                _spineSkel.timeScale = 1f;
                return;
            }

            AnimationState.SetAnimation(0, WalkAnimationName[_faceDirection], true);
            _spineSkel.timeScale = 1.5f;
        }
        private int _faceDirection;
        private void FaceDirection()
        {
            if(_direction.y > 0)
            {
                _faceDirection = 0;
            }
            if(_direction.y < 0)
            {
                _faceDirection = 1;
            }
            if(_direction.x != 0)
            {
                _faceDirection = 2;
            }
        }
        private void InvertWhenLeft()
        {
            Vector3 skelScale = new Vector3(Mathf.Abs(_spineSkel.transform.localScale.x), Mathf.Abs(_spineSkel.transform.localScale.y), Mathf.Abs(_spineSkel.transform.localScale.z));
            if(_direction.x > 0)
            {
                _spineSkel.transform.localScale = new Vector3(skelScale.x, skelScale.y, skelScale.z);
            }
            if (_direction.x < 0)
            {
                _spineSkel.transform.localScale = new Vector3(-skelScale.x, skelScale.y, skelScale.z);
            }
        }
    }
}
