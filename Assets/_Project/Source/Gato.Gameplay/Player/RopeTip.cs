using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class RopeTip : MonoBehaviour
    {
        public Transform firstParent;
        public static Transform globalTarget;
        private Transform target;
        public bool isChasing;
        private Transform followTransform;

        private RopePoolAndLineHandler poolTongue;
        public bool isGettingLonger;
        private SpriteRenderer sprite;
        private Rigidbody2D rigidbody2D;
        public HingeJoint2D hinge;
        public SpringJoint2D spring;
        public float ropeSpeed;
        public GameObject currentAttachedBody;
        public RopePoolAndLineHandler PoolTongue;
        public CurseProjectile CurseProjectile;
        public bool cursed;

        public Sprite hand;
        public Sprite fist;


        private void Start()
        {
            poolTongue = GetComponentInParent<RopePoolAndLineHandler>();
            sprite = GetComponent<SpriteRenderer>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spring = GetComponent<SpringJoint2D>();
            target = globalTarget;
        }


        private void Update()
        {
            if (isGettingLonger)
            {
                sprite.sprite = hand;
                transform.up = Vector3.Normalize(target.position - transform.position);
                rigidbody2D.velocity = Vector3.Normalize(target.position - transform.position) * ropeSpeed;
                return;
            }
            sprite.sprite = fist;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == currentAttachedBody) return;
            if (!isGettingLonger) return;
            collision.gameObject.AddComponent<HingeJoint2D>().connectedBody = rigidbody2D;
            isGettingLonger = false;
            rigidbody2D.velocity = Vector2.zero;
            PoolTongue.ActivateJoints(transform);
            Debug.Log(cursed);
            if (cursed) collision.gameObject.SendMessage("Curse1", gameObject);
        }

        private void OnDestroy()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
