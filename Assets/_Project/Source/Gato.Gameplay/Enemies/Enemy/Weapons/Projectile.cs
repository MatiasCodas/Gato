using Gato.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
        public class Projectile : Weapon
        {
            [SerializeField] private float force = 10f;
            [HideInInspector] public Rigidbody rb;
            [HideInInspector] public ObjectPool pool;

            private void Awake()
            {
                rb = GetComponent<Rigidbody>();
                rb.AddRelativeForce(Vector3.forward * -1 * force);
            }

            private void FixedUpdate()
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }

            public void ShootProjectile()
            {
                canDamage = true;
                if (rb.constraints == RigidbodyConstraints.FreezeAll) rb.constraints = RigidbodyConstraints.None;

                rb.AddRelativeForce(Vector3.forward * -1 * force);
            }

            void Hit()
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;

                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                pool.ReturnToPool(gameObject);
                gameObject.SetActive(false);
            }

            public override void OnTriggerEnter(Collider other)
            {
                base.OnTriggerEnter(other);

                if (other.CompareTag(targetTag) || other.CompareTag("Ground"))
                {
                    Hit();
                }
            }
        }
}
