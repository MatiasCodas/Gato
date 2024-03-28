using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Gato.Gameplay
{
    public class TinyDancer : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public Color BlessColor;
        public Collider2D Collider;

        [SerializeField]
        private SkeletonAnimation _spineSkel;
        public Spine.AnimationState AnimationState;
        public Spine.Skeleton Skeleton;

        [SpineAnimation]
        public string Idle, Saved;

        public bool DebugBless;

        private void Start()
        {
            AnimationState = _spineSkel.AnimationState;
            Skeleton = _spineSkel.skeleton;
        }
        private void Bless()
        {
           // SpriteRenderer.color = BlessColor;
            Collider.enabled = false;
            AnimationState.SetAnimation(0, Saved, false);
            DebugBless = false;
        }


        private void Update()
        {
            if(DebugBless)
            {
                Bless();
            }
        }
    }
}
