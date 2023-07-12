using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que cria os states e "setta" eles
namespace Gato.Gameplay
{
    public enum States
    {
        Follow,
        Idle,
        Attack,
        Aim,
        Shoot,
        Die,
        None
    }

    public class BotFSM : StateMachine
    {
        public RangeWeapon rangeWeapon;
        public Weapon weapon;
        [SerializeField]
        private float cooldownTime = 1f;
        [SerializeField]
        private int pointsValue = 10;
        [HideInInspector]
        public States firstState = States.None;
        [HideInInspector]
        public States secondState = States.None;
        private Enemy enemy;
        private Follow followState;
        private Attack attackState;
        private Aim aimState;
        private Shoot shootState;
        private Die dieState;
        private Idle idleState;
        public Animator animController;
        private float cooldown;
        private bool inCooldown;

        private void Start()
        {
            enemy = GetComponent<Enemy>();
        }

        public void Follow()
        {
            if (followState == null)
            {
                followState = new Follow(this, enemy, animController);
            }

            if (firstState != States.Follow)
            {
                SetState01(followState);
                firstState = States.Follow;
            }
        }

        public void Attack()
        {
            if (inCooldown)
                return;

            if (attackState == null)
            {
                attackState = new Attack(this, enemy, animController, weapon);
            }

            if (secondState != States.Attack)
            {
                SetState02(attackState);
                secondState = States.Attack;
            }
        }

        public void Aim()
        {
            if (aimState == null)
            {
                aimState = new Aim(this, enemy, animController);
            }

            if (firstState != States.Aim)
            {
                SetState01(aimState);
                firstState = States.Aim;
            }
        }

        public void Shoot()
        {
            if (inCooldown)
                return;

            if (shootState == null)
            {
                shootState = new Shoot(this, rangeWeapon, animController);
            }

            if (secondState != States.Shoot)
            {
                SetState02(shootState);
                secondState = States.Shoot;
            }
        }

        public void Die()
        {
            if (dieState == null)
            {
                dieState = new Die(this, enemy, animController);
            }

            if (firstState != States.Die)
            {
                SetState01(dieState);
                firstState = States.Die;
            }
        }


        public void AttackCooldown()
        {
            inCooldown = true;
            cooldown += Time.deltaTime;

            if (cooldown >= cooldownTime)
            {
                inCooldown = false;
                cooldown = 0;
                ResetState(2);
            }
        }

        //Resetar o state quando tiver o cooldown do ataque
        public void ResetState(int index)
        {
            if (index == 1 && firstState != States.None)
            {
                firstState = States.None;
            }

            else if (index == 2 && secondState != States.None)
            {
                secondState = States.None;
            }
        }
    }
}
