﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//Script que chama os states do StateMachine
namespace Gato.Gameplay
{
    public enum EnemyType
    {
        Melee,
        Range,
        Bull,
        Mosquito,
        Strafer,
        None
    }

    [RequireComponent(typeof(BotFSM))]
    public class BotController : MonoBehaviour
    {
        [SerializeField]
        private EnemyType _enemyType = EnemyType.None;
        [SerializeField]
        private float _attackDistance = 3f;

        private BotFSM _botFSM;
        private Enemy _enemy;
        private Transform _player;

        public EnemyType EnemyType => _enemyType;

        // public LayerMask playerMask;

        public void SetPlayer(Transform player)
        {
            _player = player;
        }

        private void Start()
        {
            _botFSM = GetComponent<BotFSM>();
            _enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            /*if (GameManager.isPaused)
            {
                return;
            }*/

            if (_enemy.life <= 0)
            {
                _botFSM.Die();

                return;
            }

            EnemyAction();
        }

        private void EnemyAction()
        {
            bool canAttack = false;
            // TODO: talvez melhorar como é chamado cada os states e separar a logica de cada enemyType em diferentes scrpits

            switch (_enemyType)
            {
                case EnemyType.Bull:
                {
                    _botFSM.Follow();

                    // canAttack = Physics.CheckSphere(gameObject.transform.position, attackDistance, playerMask);

                    if (canAttack)
                    {
                        _botFSM.Attack();
                    }

                    break;
                }

                case EnemyType.Mosquito:
                {
                    _botFSM.MosquitoFollow();

                    // canAttack = Physics.CheckSphere(gameObject.transform.position, attackDistance, playerMask);

                    if (canAttack)
                    {
                        _botFSM.Attack();
                    }

                    break;
                }

                case EnemyType.Strafer:
                {
                    _botFSM.StrafeFollow();

                    // canAttack = Physics.CheckSphere(gameObject.transform.position, attackDistance, playerMask);

                    if (canAttack)
                    {
                        _botFSM.Attack();
                    }

                    break;
                }

                // TODO:  criar um state de inimigo melee
                case EnemyType.Melee:

                    _botFSM.Follow();

                    // canAttack = Physics.CheckSphere(gameObject.transform.position, attackDistance, playerMask);

                    if (canAttack)
                    {
                        _botFSM.Attack();
                    }

                    break;

                // TODO:  criar um state de inimigo a distancia
                case EnemyType.Range:

                    _botFSM.Aim();
                    _botFSM.Shoot();

                    break;
            }
        }
    }
}