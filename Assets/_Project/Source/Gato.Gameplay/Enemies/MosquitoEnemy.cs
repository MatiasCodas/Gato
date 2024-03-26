using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    // TODO: Deletar depois de ter passado toda funcionalidade para os novos inimigos
    public class MosquitoEnemy : BasicEnemy
    {
        [SerializeField]
        private StraferStats _straferStats;
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
            Vector2 randomized = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _straferStats.ShakeStrength;
            _variationMomentum = Vector2.ClampMagnitude(_variationMomentum + randomized, _straferStats.MovementVariation);
            NextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(Target.transform.position - transform.position, Stats.Speed);
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
