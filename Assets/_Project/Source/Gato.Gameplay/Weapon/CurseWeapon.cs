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

        public void ThrowWeapon(Vector2 direction)
        {
            if (_inCooldown ||_projectilePool.Count >= _maxProjectileAvailable)
            {
                return;
            }

            CurseProjectile instance = Instantiate(_projectilePrefab, (Vector2)gameObject.transform.position + direction *3, Quaternion.identity);
            instance.Setup(direction);
            instance.OnCurseTriggered += HandleCurseTriggered;
            instance.OnObjectTriggered += HandleObjectTriggered;
            instance.GetComponent<CurseProjectile>().ConnectedToRope.Add(gameObject);
            instance.GetComponent<RopePoolAndLineHandler>().hand = transform;
            instance.GetComponent<CurseProjectile>().ConnectedToRope.Add(instance.gameObject);
            //instance.GetComponent<CurseProjectile>().playerObject = gameObject;
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
