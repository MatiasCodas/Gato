using Cysharp.Threading.Tasks;
using Gato.Audio;
using Gato.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    internal class PlayerControlSystem : MonoSystem, IPlayerControlService
    {
        [Header("Player Stats")]
        [SerializeField]
        private PlayerStats _playerStats;
        public static PlayerControlSystem Player;

        [Space(5)]
        [Header("Audio Settings")]
        [SerializeField] private AudioSource _playerAudioSource;
        [SerializeField] private PlayerSFXLibrary _playerSFX;

        private bool _canDash = true;
        private bool _canWalk = true;
        private bool _isDashing = false;
        private IRangedWeapon _rangedWeapon;
        private Rigidbody2D _rigidbody2d;
        private CurseWeapon _curseWeapon;
        private CharacterAnimationComponent _animationComponent;

        private bool _boosting;
        private List<GameObject> _ropeList;
        private Vector3 _boostableTargetPosition;

        [Space(5)]
        [Header("Rope Pulling Movement")]
        public Transform RopePullableTarget;

        public ServiceLocator OwningLocator { get; set; }

        public static Action OnBoosted;

        public override void Setup()
        {
            ServiceLocator.Shared.Set<IPlayerControlService>(this);
            _rangedWeapon = gameObject.GetComponent<IRangedWeapon>();
            _rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
            _curseWeapon = gameObject.GetComponent<CurseWeapon>();
            _animationComponent = gameObject.GetComponent<CharacterAnimationComponent>();
            Player = this;

            _boosting = false;
            CurseProjectile.OnBoosting += RopeBoostingMovement;

            Teleport.OnTeleporting += PlayTeleportSFX;
            BasicEnemy.OnIncreaseHitPoints += PlayHitByEnemySFX;
        }

        public override void Dispose()
        {
            CurseProjectile.OnBoosting -= RopeBoostingMovement;

            Teleport.OnTeleporting -= PlayTeleportSFX;
            BasicEnemy.OnIncreaseHitPoints -= PlayHitByEnemySFX;
        }

        public void Dash(Vector2 direction)
        {
            if (!_canDash)
            {
                return;
            }

            DashAsync(direction);
            _animationComponent.Dashing(direction);
        }

        public void Move(Vector2 direction)
        {
            if (!_canWalk)
            {
                return;
            }

            _rigidbody2d.MovePosition(_rigidbody2d.position + (direction * _playerStats.MovementSpeed * Time.fixedDeltaTime));
            _animationComponent.Walking(direction);

            // Walking SFX was disabled as it may not be needed
            /*
            if (direction != Vector2.zero)
                AudioManager.Instance.ToggleSFX(_playerAudioSource, _playerSFX.WalkSFX, true);
            else
                AudioManager.Instance.ToggleSFX(_playerAudioSource, _playerSFX.WalkSFX, false);
            */
        }

        public void WeaponAim(Vector2 direction)
        {
            if (direction != new Vector2(0, 0))
            {
                _curseWeapon.Aim("Controller", direction + (Vector2)transform.position);
                return;
            }
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _curseWeapon.Aim("Mouse", direction);

        }

        public void EnemyHit()
        {
            if (_isDashing) return;

            if (_playerStats.HitPoints + 1 < _playerStats.MaxHP)
            {
                _playerStats.HitPoints += 1;
            }
            else
            {
                _playerStats.HitPoints = 0; // Value will be replaced by the stats set in the Game Design SO
                _playerStats.MaxHP = 3; // Value will be replaced by the stats set in the Game Design SO
                Destroy(gameObject);
            }
        }

        public void ShootWeapon(Vector2 direction)
        {
            if (_isDashing)
            {
                return;
            }

            if (direction == new Vector2(0, 0)) 
            {
                direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            }
            direction = direction.normalized;

            _rangedWeapon.ThrowWeapon(direction);
        }

        public void RecoverWeapon()
        {
            CurseProjectile.AllRopesComeBack();
        }

        public PlayerStats FetchPlayerStats()
        {
            return _playerStats;
        }

        private async UniTask DashAsync(Vector2 direction)
        {
            _isDashing = true;
            _canDash = false;
            _canWalk = false;
            _rigidbody2d.velocity = (direction.normalized * _playerStats.DashSpeed);
            _animationComponent.Dashing(direction);
            await UniTask.Delay((int)(_playerStats.DashTime * 1000));
            _rigidbody2d.velocity = Vector2.zero;
            _animationComponent.WalkOrIdle(direction);
            _canWalk = true;

            await UniTask.Delay(250);
            _isDashing = false;

            await UniTask.Delay((int)(_playerStats.DashCooldown * 1000));

            _canDash = true;
        }

        private void PlayTeleportSFX()
        {
            AudioManager.Instance.ToggleSFX(_playerAudioSource, _playerSFX.TeleportingSFX);
        }

        private void PlayHitByEnemySFX()
        {
            AudioManager.Instance.ToggleSFX(_playerAudioSource, _playerSFX.HitByEnemySFX);
        }

        private void RopeBoostingMovement(List<GameObject> ropeList, Vector3 ropeTipPosition)
        {
            _ropeList = ropeList;
            _boostableTargetPosition = ropeTipPosition;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            // Rope Boost

            if (CurseWeapon.ProjectilePoolCounter == 1
                && _ropeList != null
                && _ropeList.Count > 0
                && !_boosting
                && Keyboard.current.pKey.wasPressedThisFrame) // Temporary key
                _boosting = true;

            if (_boosting)
            {
                AudioManager.Instance.ToggleSFX(_playerAudioSource, _playerSFX.BoostByRopeSFX);
                transform.position = Vector2.MoveTowards(transform.position, _boostableTargetPosition, 1f);
            }

            if (_ropeList != null
                && transform.position == _boostableTargetPosition)
            {
                _boosting = false;
                _ropeList.Clear();
                OnBoosted?.Invoke();
            }
        }
    }
}
