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
        private int _cooldownTime = 1000;
        [SerializeField]
        private CurseProjectile _projectilePrefab;

        private bool _hasHitCurse;
        private bool _hasHitObj;
        private bool _inCooldown;
        private List<CurseProjectile> _projectilePool = new List<CurseProjectile>();

        public void ThrowWeapon(Vector2 direction)
        {
            if (_inCooldown ||_projectilePool.Count >= _maxProjectileAvailable)
            {
                return;
            }

            CurseProjectile instance = Instantiate(_projectilePrefab, gameObject.transform.position, Quaternion.identity);
            instance.Setup(direction);
            instance.OnCurseTriggered += HandleCurseTriggered;
            instance.OnObjectTriggered += HandleObjectTriggered;
            _projectilePool.Add(instance);

            WeaponCooldown();
        }

        private async UniTask WeaponCooldown()
        {
            _inCooldown = true;

            await UniTask.Delay(_cooldownTime);

            _inCooldown = false;
        }

        private void ActivateCurse()
        {
            foreach(CurseProjectile projectile in _projectilePool)
            {
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
    }
}