using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class MosquitoEnemy : BasicEnemy
    {
        public float MovementVariation;
        public float ShakeStrength;
        private bool _angry;
        private bool _tired;
        private Vector2 _variationMomentum;
        private void Start()
        {
            BasicStart();
        }


        private void FixedUpdate()
        {
            switch (MovementState)
            {
                default:
                case 0:
                    if (_angry) return;
                    //BasicMovement();
                    WeirdMovement();
                    break;


            }
            
            FaceDirection();
        }

        private void WeirdMovement()
        {
            Vector2 randomized = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * ShakeStrength;
            _variationMomentum = Vector2.ClampMagnitude(_variationMomentum + randomized, MovementVariation);
            NextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(Target.transform.position - transform.position, Speed);
            NextPosition = new Vector2(NextPosition.x + _variationMomentum.x, NextPosition.y + _variationMomentum.y);
            RB2D.MovePosition(NextPosition);
            //RB2D.MovePosition(randomized * Time.deltaTime);
        }
        private void Update()
        {
            BasicUpdate();
        }

    }
}
