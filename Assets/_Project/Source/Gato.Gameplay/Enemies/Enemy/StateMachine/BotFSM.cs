using UnityEngine;

//Script que cria os states e "setta" eles
namespace Gato.Gameplay
{
    public enum States
    {
        Follow,
        MosquitoFollow,
        StrafeFollow,
        Idle,
        Attack,
        Aim,
        Shoot,
        Die,
        None
    }

    public class BotFSM : StateMachine
    {
        [SerializeField]
        private RangeWeapon _rangeWeapon;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private float _cooldownTime = 1f;
        [SerializeField]
        private States _firstState = States.None;
        [SerializeField]
        private States _secondState = States.None;

        private Enemy _enemy;
        private Follow _followState;
        private MosquitoFollow _mosquitoFollowState;
        private StrafeFollow _strafeFollowState;
        private Attack _attackState;
        private Aim _aimState;
        private Shoot _shootState;
        private Die _dieState;
        private Idle _idleState;
        private float _cooldown;
        private bool _inCooldown;

        // TODO: Change to the type of animation we will use.
        // public Animator animController;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        public void Idle()
        {
            if (_idleState == null)
            {
                _idleState = new Idle();
            }

            if (_firstState != States.Idle)
            {
                SetState01(_idleState);
                _firstState = States.Idle;
            }
        }

        public void Follow()
        {
            if (_followState == null)
            {
                _followState = new Follow(this, _enemy);
            }

            if (_firstState != States.Follow)
            {
                SetState01(_followState);
                _firstState = States.Follow;
            }
        }

        public void MosquitoFollow()
        {
            if (_mosquitoFollowState == null)
            {
                _mosquitoFollowState = new MosquitoFollow(this, _enemy);
            }

            if (_firstState != States.MosquitoFollow)
            {
                SetState01(_mosquitoFollowState);
                _firstState = States.MosquitoFollow;
            }
        }

        public void StrafeFollow()
        {
            if (_strafeFollowState == null)
            {
                _strafeFollowState = new StrafeFollow(this, _enemy);
            }

            if (_firstState != States.StrafeFollow)
            {
                SetState01(_strafeFollowState);
                _firstState = States.StrafeFollow;
            }
        }

        public void Attack()
        {
            if (_inCooldown)
                return;

            if (_attackState == null)
            {
                _attackState = new Attack(this, _enemy, _weapon);
            }

            if (_secondState != States.Attack)
            {
                SetState02(_attackState);
                _secondState = States.Attack;
            }
        }

        public void Aim()
        {
            if (_aimState == null)
            {
                _aimState = new Aim(this, _enemy);
            }

            if (_firstState != States.Aim)
            {
                SetState01(_aimState);
                _firstState = States.Aim;
            }
        }

        public void Shoot()
        {
            if (_inCooldown)
                return;

            if (_shootState == null)
            {
                _shootState = new Shoot(this, _rangeWeapon);
            }

            if (_secondState != States.Shoot)
            {
                SetState02(_shootState);
                _secondState = States.Shoot;
            }
        }

        public void Die()
        {
            if (_dieState == null)
            {
                _dieState = new Die(this, _enemy);
            }

            if (_firstState != States.Die)
            {
                SetState01(_dieState);
                _firstState = States.Die;
            }
        }


        public void AttackCooldown()
        {
            _inCooldown = true;
            _cooldown += Time.deltaTime;

            if (_cooldown >= _cooldownTime)
            {
                _inCooldown = false;
                _cooldown = 0;
                ResetState(1);
            }
        }

        //Resetar o state quando tiver o cooldown do ataque
        public void ResetState(int index)
        {
            if (index == 1 && _firstState != States.None)
            {
                _firstState = States.None;
            }

            else if (index == 2 && _secondState != States.None)
            {
                _secondState = States.None;
            }
        }
    }
}
