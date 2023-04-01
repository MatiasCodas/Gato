using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CurseWeapon : MonoBehaviour, IRangedWeapon
    {
        [SerializeField]
        private int _maxProjectileAvailable = 2;
        [SerializeField]
        private PlayerStats _playerStats;
        [SerializeField]
        private CurseProjectile _projectilePrefab;

        private bool _hasHitCurse;
        private bool _hasHitObj;
        private bool _inCooldown;
        private List<CurseProjectile> _projectilePool = new List<CurseProjectile>();

        public void Awake()
        {
            Debug.LogError("AWAKE");
            _inCooldown = false;
        }

        public void ThrowWeapon(Vector2 direction)
        {
            if (_inCooldown ||_projectilePool.Count >= _maxProjectileAvailable)
            {
                return;
            }

            Debug.LogError(direction);
            CurseProjectile instance = Instantiate(_projectilePrefab, (Vector2)gameObject.transform.position + direction, Quaternion.identity);
            instance.Setup(direction, _hasHitCurse, gameObject);
            instance.OnCurseTriggered += HandleCurseTriggered;
            instance.OnObjectTriggered += HandleObjectTriggered;
            instance.OnRopeDestroy += HandleRopeDestroy;

            Rigidbody2D playerRigidBody = GetComponent<Rigidbody2D>();
            Rigidbody2D projectileRigidBody = instance.GetComponent<Rigidbody2D>();
            RopePoolAndLineHandler ropePool = instance.GetComponent<RopePoolAndLineHandler>();
            ropePool.Setup(playerRigidBody, projectileRigidBody);
            _projectilePool.Add(instance);

            WeaponCooldown();
        }

        private async UniTask WeaponCooldown()
        {
            _inCooldown = true;

            await UniTask.Delay((int)(_playerStats.RopeCooldown * 1000));

            _inCooldown = false;
        }

        private void ActivateCurse()
        {
            foreach (CurseProjectile projectile in _projectilePool)
            {
                Debug.Log("ACTIVE");
                projectile.ActivateCurse(_hasHitCurse);
            }

            _hasHitCurse = false;
            _hasHitObj = false;
            _projectilePool.Clear();
        }

        private void HandleCurseTriggered()
        {
            _hasHitCurse = true;

            if (_hasHitObj)
            {
                ActivateCurse();
            }
        }

        private void HandleObjectTriggered()
        {
            _hasHitObj = true;

            if (_hasHitCurse)
            {
                ActivateCurse();
            }
        }

        private void HandleRopeDestroy()
        {
            List<CurseProjectile> projectilePool = _projectilePool;
            _projectilePool.Clear();
            foreach(CurseProjectile curseProjectile in projectilePool)
            {
                if (curseProjectile != null)
                {
                    _projectilePool.Add(curseProjectile);
                }
            }
        }
    }
}
