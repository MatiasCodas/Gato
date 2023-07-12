using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que chama os states do StateMachine
namespace Gato.Gameplay
{
    public enum EnemyType
    {
        Melee,
        Range,
        None
    }

    [RequireComponent(typeof(BotFSM))]
    public class BotController : MonoBehaviour
    {
        public EnemyType enemyType = EnemyType.None;
        private BotFSM botFSM;
        [HideInInspector]
        public Transform player;
        [SerializeField]
        private float attackDistance = 3f;
        public LayerMask playerMask;
        private bool canAttack;
        private Enemy enemy;


        void Start()
        {
            botFSM = GetComponent<BotFSM>();
            enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            /*if (GameManager.isPaused)
            {
                return;
            }*/

            if (enemy.life <= 0)
            {
                botFSM.Die();

                return;
            }

            EnemyAction();
        }

        void EnemyAction()
        {
            switch (enemyType)
            {
                case EnemyType.Melee:

                    botFSM.Follow();

                    canAttack = Physics.CheckSphere(gameObject.transform.position, attackDistance, playerMask);

                    if (canAttack)
                    {
                        botFSM.Attack();
                    }

                    break;

                case EnemyType.Range:

                    botFSM.Aim();
                    botFSM.Shoot();

                    break;
            }
        }
    }
}
