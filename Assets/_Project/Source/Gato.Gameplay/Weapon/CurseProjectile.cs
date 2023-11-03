using Gato.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CurseProjectile : MonoBehaviour
    {

        //events
        public delegate void TargetHitHandler();

        public event TargetHitHandler OnCurseTriggered;

        public event TargetHitHandler OnObjectTriggered;

        public event TargetHitHandler OnRopeDestroy;

        [SerializeField]
        private PlayerStats _playerStats;
        [SerializeField]
        private PlayerSFXLibrary _playerSFX;

        //Outside objects
        [HideInInspector]
        public HingeJoint2D HingeJoint;
        [HideInInspector]
        public RopePoolAndLineHandler Rope;
        [HideInInspector]
        public GameObject ConnectedRopeTip;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _direction;
        [HideInInspector]
        public CurseRopeShooter RopeShooter;
        public List<GameObject> ConnectedToRope;
        private Transform _connectedFinalTarget;
        private GameObject _player;

        //internal communications
        private bool _goBack = false;
        private bool _isMoving;
        private int _ropeProjectileIndex;
        private int _layerMask;
        private float _timeActive;
        private int _lineIndex = 0;

        //Curse and bless stuff
        public static bool IsBlessed;
        public static List<bool> IsCursed = new List<bool>();
        private bool _sentCurse;
        private bool _isCursed;
        private BasicEnemy _enemyHit;
        private Transform _target = null;
        private static List<Transform> _targets = new List<Transform>();

        //outside communications
        [HideInInspector]
        public bool IsAlreadyDead = false;
        public static bool GoAllBack = false;
        public static Action<Collision2D, Transform> OnPulling;
        public static Action<List<GameObject>, Vector3, Collision2D> OnBoosting;
        public static Action OnCursedStatus;
        private static bool _isTarget = true;

        public void Setup(Vector2 direction, bool isCurseActive, GameObject player, int index)
        {
            _player = player;
            _isCursed = isCurseActive;
            HingeJoint = GetComponent<HingeJoint2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Rope = GetComponent<RopePoolAndLineHandler>();
            Rope.IsMoving = true;
            _isMoving = true;
            _direction = direction;
            _layerMask = LayerMask.GetMask("Roped");
            ConnectedToRope.Add(player);
            ConnectedToRope.Add(gameObject);
            HingeJoint.enabled = false;
            _ropeProjectileIndex = index;
            if (IsCursed.Count > index) return;
            IsCursed.Add(false);
        }

        public void ActivateCurse(bool cursed)
        {
            if (_enemyHit != null)
            {
                _enemyHit.Curse(gameObject);
            }
        }

        private void Update()
        {

            // gambiarra, favor trocar os inputs pro inputmanager depois
            if (_timeActive > _playerStats.RopeTime || LineSize() >= _playerStats.RopeSize || GoAllBack)
            {
                RopeComeBack();
            }

            if (!_isMoving)
            {
                _timeActive += Time.deltaTime;
                _goBack = _connectedFinalTarget == null ? true : _goBack;
            }

            if (_goBack)
            {
                return;
            }
        }

        private void FixedUpdate()
        {
            LineCollisions();

            if (_isCursed)
            {
                SendNewCurseToAll();
            }

            Directionator();
            Vector2 translation = _playerStats.ProjectileSpeed * Time.deltaTime * _direction;
            _rigidbody2D.velocity = (translation);
        }

        #region Main Line functions
        private void LineCollisions()
        {
            Vector2 rayShooter = ConnectedToRope[_lineIndex + 1].transform.position;
            Vector2 target = ConnectedToRope[_lineIndex].transform.position;
            RaycastHit2D ray2D = Physics2D.Raycast(rayShooter, target - rayShooter, Vector2.Distance(target, rayShooter), _layerMask);
            Debug.DrawRay(rayShooter, target - rayShooter, ray2D.collider != null ? Color.blue : Color.red);
            
            if (ray2D.collider == null)
            {
                ShouldLoopLineCollision();
                
                return;
            }

            if (ray2D.collider.name == "Curse")
            {
                _isCursed = true;
                IsCursed[_ropeProjectileIndex] = true;
            }

            if (_isCursed)
            {
                BasicEnemy basicEnemy = ray2D.collider.gameObject.GetComponent<BasicEnemy>();

                if (basicEnemy != null)
                {
                    basicEnemy.Curse(gameObject);
                }
            }

            if (_goBack && _connectedFinalTarget != null)
            {
                return;
            }

            ray2D.collider.gameObject.layer = 7;
            for (int i = 0; i < ConnectedToRope.Count-1; i++)
            {
                if (ray2D.collider.gameObject == ConnectedToRope[i].gameObject) return;
            }
        }


        private void SendNewCurseToAll()
        {
            if (_sentCurse)
            {
                return;
            }

            _sentCurse = true;
        }
        #endregion

        #region Line internal tools 
        //Everything inside this region is used for internal calculations for easier code understanding
        private void LineArrayRemove(int index)
        {
            ConnectedToRope.RemoveAt(index);
            LineIndexReset();
        }

        private void LineIndexReset()//When the line points may get off sync you should call for this
        {
            _lineIndex = ConnectedToRope.Count - 2;
        }

        private void ShouldLoopLineCollision()
        {
            if (_lineIndex <= 0)
            {
                LineIndexReset();
                return;
            }

            _lineIndex--;
            LineCollisions();
        }

        private float LineSize()
        {
            float pointDistance = Vector2.Distance(ConnectedToRope[0].transform.position, ConnectedToRope[ConnectedToRope.Count-1].transform.position);

            return pointDistance;
        }

        #endregion

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
            {
                return;
            }

            Rope.ActivateJoints();
            if (collision.gameObject.CompareTag("Player") && _goBack)
            {
                Destroy(gameObject);
            
                return;
            }

            if(_goBack)
            {
                RopeComeBack();

                return;
            }

            _isMoving = false;
            Rope.IsMoving = false;
            _connectedFinalTarget = collision.transform;
            HingeJoint.enabled = true;

            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D hitRigidBody))
            {
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                HingeJoint.connectedBody = hitRigidBody;
            }
            
            _enemyHit = collision.gameObject.GetComponent<BasicEnemy>();
            
            if (_enemyHit != null)
            {
                if (_isCursed || IsCursed[_ropeProjectileIndex])
                {
                    _enemyHit.Curse(gameObject);

                    return;
                }
                else
                {
                    OnObjectTriggered?.Invoke();
                }
            }

            switch (collision.gameObject.tag)
            {
                default:
                    if (IsCursed[_ropeProjectileIndex]) OnCurseTriggered?.Invoke();
                    if (IsBlessed) collision.gameObject.SendMessageUpwards("Bless");
                    break;
                case "Curse":
                    IsCursed[_ropeProjectileIndex] = true;
                    OnCurseTriggered?.Invoke();
                    OnCursedStatus?.Invoke();
                    break;
                case "Blessing":
                    IsBlessed = true;
                    break;
                case "RopePullable":
                    OnPulling?.Invoke(collision, PlayerControlSystem.Player.RopePullableTarget);
                    break;
                case "RopeBoostable":
                    int lastIndex = ConnectedToRope.Count - 1;
                    OnBoosting?.Invoke(ConnectedToRope, ConnectedToRope[lastIndex].transform.position, collision);
                    break;
            }

            _isTarget = !_isTarget;
            Rope.ActivateJoints();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
            {
                return;
            }

                // _isCursed = false;
                _sentCurse = false;
            //if (collision.gameObject.name == "Curse")
            switch (collision.gameObject.tag)
            {
                case "Curse":
                    IsCursed[_ropeProjectileIndex] = false;
                    _sentCurse = false;
                    break;
                case "Blessing":
                    IsBlessed = false;
                    break;
                default:
                    break;
            }

            
            if (_connectedFinalTarget == null)
            {
                // _goBack = true;
                RopeComeBack();
            }

            if (!_goBack)
            {
                return;
            }

            if (collision.gameObject.layer != 7)
            {
                return;
            }

            collision.gameObject.layer = 0;



            
        }

        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            for (int i = 0; i < _targets.Count; i++)
            {
                if (collision.transform == _targets[i]) return;
            }

            if (_target == null || Vector2.Distance(collision.transform.position, transform.position) < Vector2.Distance(_target.position, transform.position))
            {
                _target = collision.transform;
                _targets.Add(_target);
            }
         
        }

        private void Directionator()
        {
            if (_target == null) return;
            _direction = Vector3.Lerp(_direction,Vector3.ClampMagnitude(_target.position-transform.position, 1) , _playerStats.HomingStrength);
        }

        private void Awake()
        {
            RopePullable.OnPulled += RopeComeBack;
            PlayerControlSystem.OnBoosted += RopeComeBack;
        }

        private void OnDestroy()
        {
            RopePullable.OnPulled -= RopeComeBack;
            PlayerControlSystem.OnBoosted -= RopeComeBack;

            IsCursed[_ropeProjectileIndex] = false;
            IsAlreadyDead = true;
            OnRopeDestroy?.Invoke();
            GoAllBack = false;
            if (ConnectedRopeTip != null)
            {
                Destroy(ConnectedRopeTip);
            }
            if (_target == null) return;
            _targets.Remove(_target);
        }

        public static void AllRopesComeBack()
        {
            GoAllBack = true;
        }
        private void RopeComeBack()
        {
            CurseWeapon.ProjectilePoolCounter = 0;
            IsAlreadyDead = true;
            OnRopeDestroy?.Invoke();
            Destroy(gameObject);
            AudioManager.Instance.ToggleSFX(_player.GetComponent<AudioSource>(), _playerSFX.MissSFX);

            return;
        }
    }
}
