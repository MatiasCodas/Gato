using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class StraferEnemy : BasicEnemy
    {
        [SerializeField]
        private StraferStats _straferStats;
        private bool _angry;
        private bool _tired;
        private Vector2 _variationMomentum;
        private bool _clockwise;
        private void Start()
        {
            BasicStart();
            if (Random.Range(0, 1) == 0) _clockwise = false;
            else _clockwise = true;
        }


        private void FixedUpdate()
        {
            switch (MovementState)
            {
                default:
                case 0:
                    if (_angry) return;
                    //BasicMovement();
                    StrafeMovement();
                    break;


            }
            
            FaceDirection();
        }

        private void StrafeMovement()
        {
            Vector2 perpendicularMovement = Vector2.Perpendicular( Vector2.ClampMagnitude(Target.transform.position - transform.position, _stats.Speed));
            NextPosition = (Vector2)transform.position + (_clockwise ? perpendicularMovement : -perpendicularMovement);
            
            _variationMomentum = Vector2.ClampMagnitude(_variationMomentum + NextPosition, _straferStats.MovementVariation);
           // NextPosition = new Vector2(NextPosition.x + _variationMomentum.x, NextPosition.y + _variationMomentum.y);
            RB2D.MovePosition(NextPosition);
            //RB2D.MovePosition(randomized * Time.deltaTime);
        }
        private void Update()
        {
            BasicUpdate();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                Destroy(collision.gameObject);
                return;
            }
            _clockwise = !_clockwise;

        }

    }
}
