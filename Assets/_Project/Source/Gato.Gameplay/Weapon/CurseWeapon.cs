using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CurseWeapon : MonoBehaviour, IRangedWeapon
    {
        [SerializeField]
        private int _maxProjectileAvailable = 1;
        [SerializeField]
        private PlayerStats _playerStats;
        [SerializeField]
        private CurseProjectile _projectilePrefab;
        [SerializeField]
        private GameObject _aim;
        [SerializeField]
        private LineRenderer _laserAim;

        private bool _hasHitCurse;
        private bool _hasHitObj;
        private bool _inCooldown;
        private List<CurseProjectile> _projectilePool = new List<CurseProjectile>();
        private HingeJoint2D _hinge;

        private RopePoolAndLineHandler _ropePool;

        public static int ProjectilePoolCounter;

        public void Awake()
        {
            _inCooldown = false;
            _hinge = GetComponent<HingeJoint2D>();
        }
        private void Update()
        {

        }

        public void Aim(string mode, Vector2 position)
        {
            switch (mode)
            {
                default:
                case "Controller":
                    _aim.transform.LookAt(position, Vector3.forward);
                    _laserAim.SetPosition(0, _aim.transform.position);
                    _laserAim.SetPosition(1, position);
                    break;

                case "Mouse":
                    _aim.transform.LookAt(position, Vector3.forward);
                    _laserAim.SetPosition(0, _aim.transform.position);
                    _laserAim.SetPosition(1, position);
                    break;
            }

        }

        public void ThrowWeapon(Vector2 direction)
        {
            if (_inCooldown ||_projectilePool.Count >= _maxProjectileAvailable*2)
            {
                return;
            }

            _hasHitCurse = false;
            CurseProjectile instance = Instantiate(_projectilePrefab, (Vector2)gameObject.transform.position + direction, Quaternion.identity);
            instance.Setup(direction, _hasHitCurse, gameObject, _projectilePool.Count / 2);
            instance.OnCurseTriggered += HandleCurseTriggered;
            instance.OnObjectTriggered += HandleObjectTriggered;
            instance.OnRopeDestroy += HandleRopeDestroyed;

            Rigidbody2D playerRigidBody = GetComponent<Rigidbody2D>();
            Rigidbody2D projectileRigidBody = instance.GetComponent<Rigidbody2D>();
            
            _ropePool = instance.GetComponent<RopePoolAndLineHandler>();
            _ropePool.Setup(playerRigidBody, projectileRigidBody);
            _projectilePool.Add(instance);

            if(_projectilePool.Count % 2 != 1)
            {
                instance.Rope.Deactivate();
                _projectilePool[_projectilePool.Count - 2].Rope.FirstHinge = instance.HingeJoint;
                _projectilePool[_projectilePool.Count - 2].Rope.ActivateJoints();
                _projectilePool[_projectilePool.Count - 2].GetComponent<CurseProjectile>().ConnectedRopeTip = _projectilePool[_projectilePool.Count - 1].gameObject;
                _projectilePool[_projectilePool.Count - 1].GetComponent<CurseProjectile>().ConnectedRopeTip = _projectilePool[_projectilePool.Count - 2].gameObject;
                _hinge.connectedBody = null;
                _hinge.enabled = false;
            }
            WeaponCooldown();

            ProjectilePoolCounter = _projectilePool.Count;
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
                projectile.ActivateCurse(_hasHitCurse);
            }

            _hasHitCurse = false;
            _hasHitObj = false;
          //  _projectilePool.Clear();
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

        private void HandleRopeDestroyed()
        {

            for (int i = 0; i < _projectilePool.Count; i++)
            {
                if(_projectilePool[i] == null || _projectilePool[i].IsAlreadyDead == true)
                {
                    _projectilePool.RemoveAt(i);
                }
            }
        }

    }
}
