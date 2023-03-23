using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Gato.Gameplay
{
    public class BullEnemy : BasicEnemy
    {
        public float DashSpeed;
        public float TelegraphTime;
        public float DistanceToAggro;
        public float ChargeTime;
        public float RestTime;
        private bool _angry;
        private bool _tired;

        public AIDestinationSetter DestinationSetter;
        public AIPath AIPath;

        private void Start()
        {
            BasicStart();
            DestinationSetter.target = Target.transform;
        }


        private void FixedUpdate()
        {
            switch (MovementState)
            {
                default:
                case 0:
                    if (_angry) return;
                    LookingAtTarget();
                    //BasicMovement();
                    AIPath.enabled = true;
                    break;

                case 1:
                    Telegraph();
                    AIPath.enabled = false;
                    break;

                case 2:
                    Dash();
                    AIPath.enabled = false;
                    break;

            }
            NextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(Target.transform.position - transform.position, Speed);
            FaceDirection();
        }
        private void Update()
        {
            BasicUpdate();
        }

        private void LookingAtTarget()
        {
            if (_tired) return;
            if(Vector2.Distance( Target.transform.position, transform.position) < DistanceToAggro && !_angry)
            {
                MovementState = 1;
                _angry = true;
                _tired = true;
                StartCoroutine(ChargingUp());
            }
        }
        private void Telegraph()
        {
            Sprite.color = new Color(Random.value, Random.value, Random.value);
            RB2D.velocity = Vector2.zero;
        }
        private IEnumerator ChargingUp()
        {
            yield return new WaitForSeconds(TelegraphTime);
            MovementState = 2;
        }

        private void Dash()
        {
            if (!_angry) return;
            Sprite.color = Color.white;
            RB2D.AddForce(DashSpeed * Vector2.ClampMagnitude((Target.transform.position - transform.position), 10));
            MovementState = 0;
            StartCoroutine(Dashing());
            StartCoroutine(Cooldown());
        }

        private IEnumerator Dashing()
        {
            yield return new WaitForSeconds(ChargeTime);
            _angry = false;
        }
        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(RestTime);
            _tired = false;
        }
    }
}
