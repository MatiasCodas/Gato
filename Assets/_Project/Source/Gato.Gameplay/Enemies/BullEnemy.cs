using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Gato.Audio;

namespace Gato.Gameplay
{
    // TODO: Deletar depois de ter passado toda funcionalidade para os novos inimigos
    public class BullEnemy : BasicEnemy
    {
        [Header("Bull Stats")]
        [SerializeField]
        private BullStats _bullStats;
        private bool _angry;
        private bool _tired;
        private EnemyAnimation _animatComponent;
        [SerializeField] private float _timeToWalkAgain;

        [Space(5)]
        [Header("Audio Settings")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private PlayerSFXLibrary _playerSFXLibrary;

        [Space(5)]
        [Header("AI")]
        public AIDestinationSetter DestinationSetter;
        public AIPath AIPath;

        

        private void Awake()
        {
            CurseProjectile.OnCursedStatus += PlayCurseSFX;
        }

        private void OnDestroy()
        {
            CurseProjectile.OnCursedStatus -= PlayCurseSFX;
        }

        private void Start()
        {
            BasicStart();
            DestinationSetter.target = Target.transform;
            _animatComponent = GetComponent<EnemyAnimation>();
            //_animatComponent.Walking(Vector2.zero);
        }


        private void FixedUpdate()
        {
            if(_timeToWalkAgain >= 0)
            {
                _timeToWalkAgain -= Time.deltaTime/2;
                return;
            }
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
            NextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(Target.transform.position - transform.position, Stats.Speed);
            _animatComponent.Walking(NextPosition - (Vector2)transform.position);
            //Debug.Log(NextPosition - (Vector2)transform.position);
            FaceDirection();
        }
        private void Update()
        {
            if (_timeToWalkAgain >= 0)
            {
                _timeToWalkAgain -= Time.deltaTime / 2;
                return;
            }
            BasicUpdate();
        }

        private void LookingAtTarget()
        {
            if (_tired) return;
            if(Vector2.Distance( Target.transform.position, transform.position) < _bullStats.DistanceToAggro && !_angry)
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
            _animatComponent.ChargingDash();
        }
        private IEnumerator ChargingUp()
        {
            yield return new WaitForSeconds(_bullStats.TelegraphTime);
            MovementState = 2;
        }

        private void Dash()
        {
            if (!_angry) return;
            Sprite.color = Color.white;
            RB2D.AddForce(_bullStats.DashSpeed * Vector2.ClampMagnitude((Target.transform.position - transform.position), 10));
            MovementState = 0;
            StartCoroutine(Dashing());
            StartCoroutine(Cooldown());
            _animatComponent.Dashing(Vector2.ClampMagnitude((Target.transform.position - transform.position), 10));
        }

        public void Curse(GameObject rope)
        {
            cursed = true;
            StartCoroutine(TimerToDie(rope));
            _animatComponent.Die();
            _timeToWalkAgain = 20;
        }

        private IEnumerator TimerToDie(GameObject rope)
        {

            yield return new WaitForSeconds(Stats.TimeToDie);
            CurseProjectile.GoAllBack = true;
            Destroy(gameObject);

            // Destroy(rope);
        }

        private IEnumerator Dashing()
        {
            yield return new WaitForSeconds(_bullStats.ChargeTime);
            _angry = false;
        }
        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_bullStats.RestTime);
            _tired = false;
        }

        private void PlayCurseSFX()
        {
            AudioManager.Instance.ToggleSFX(_audioSource, _playerSFXLibrary.HitSFX);
        }
    }
}
