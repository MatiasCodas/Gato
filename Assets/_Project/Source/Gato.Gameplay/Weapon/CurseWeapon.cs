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
        [SerializeField]
        private GameObject _aim;

        private bool _hasHitCurse;
        private bool _hasHitObj;
        private bool _inCooldown;
        private List<CurseProjectile> _projectilePool = new List<CurseProjectile>();
        private HingeJoint2D _hinge;

        public void Awake()
        {
            _inCooldown = false;
            _hinge = GetComponent<HingeJoint2D>();
        }
        private void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _aim.transform.LookAt(mousePos, Vector3.forward);

            
        }

        public void ThrowWeapon(Vector2 direction)
        {
            if (_inCooldown ||_projectilePool.Count >= _maxProjectileAvailable)
            {
                return;
            }

            _hasHitCurse = false;
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

            if(_projectilePool.Count % 2 != 1)
            {
                instance.Rope.Deactivate();
                
                _projectilePool[_projectilePool.Count - 2].Rope.FirstHinge = instance.HingeJoint;
                _projectilePool[_projectilePool.Count - 2].Rope.ActivateJoints();
                _hinge.connectedBody = null;
                _hinge.enabled = false;
            }
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
