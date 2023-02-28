using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CurseProjectile : MonoBehaviour
    {
        public delegate void TargetHitHandler();

        public event TargetHitHandler OnCurseTriggered;

        public event TargetHitHandler OnObjectTriggered;

        [SerializeField]
        private float _movementSpeed = 3f;
        [SerializeField]
        private PlayerStats _playerStats;

        private BasicEnemy _enemyHit;
        private HingeJoint2D _hingeJoint;
        private Rigidbody2D _rigidbody2D;
        private DistanceJoint2D _distanceJoint2D;
        private RopePoolAndLineHandler _rope;
        private bool _isMoving;
        private bool _isCursed;
        public static bool IsCursed;
        public static bool IsBlessed;
        private bool _sentCurse;
        private Vector2 _direction;
        public CurseRopeShooter RopeShooter;
        private LineRenderer _line;
        private static bool _isTarget = true;
        public List<GameObject> ConnectedToRope;
        private List<Vector2> _connectedRelativePosition;
        private Transform _connectedFinalTarget;
        private int _layerMask;
        private float _timeActive;
        private int _lineIndex = 0;
        private GameObject _player;

        public float TimerToDestroy;
        private float _timeGoingBack;
        private bool _goBack = false;
        public static bool goAllBack = false;

        public void Setup(Vector2 direction, bool isCurseActive, GameObject player)
        {
            Debug.Log(isCurseActive);
            _player = player;
            _isCursed = isCurseActive;
            _hingeJoint = GetComponent<HingeJoint2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rope = GetComponent<RopePoolAndLineHandler>();
            _isMoving = true;
            _direction = direction;
            _line = GetComponent<LineRenderer>();
            _layerMask = LayerMask.GetMask("Roped");
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
            try
            {
                LineUpdate();
            }
            catch
            {
                LineIndexReset();
                _goBack = true;
            }

            // gambiarra, favor trocar os inputs pro inputmanager depois
            // very nested also, sounds like a good place to refactor
            if (Input.GetKeyDown(KeyCode.Q) || _timeActive > _playerStats.RopeTime || LineSize() >= _playerStats.RopeSize || goAllBack)
            {
                // _goBack = true;
                //goAllBack = false;
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

            try
            {
                // transform.position = Vector3.Lerp(transform.position, _connectedFinalTarget.position, 0.1f);
            }
            catch
            {
                //this error doesn't do much so I wanted a cooler error message
                Debug.Log("You got an error but it's not a big deal so I'm doing this chill message");
            } 
        }

        private void FixedUpdate()
        {
            LineCollisions();

            if (_isCursed)
            {
                SendNewCurseToAll();
            }

            if (!_isMoving && !_goBack)
            {
                _timeGoingBack = 1.5f;
            
                return;
            }

            _timeGoingBack += Time.deltaTime;

            if (_goBack && _timeGoingBack > 5)
            {
                RopeComeBack();
                //LineArrayRemove(ConnectedToRope.Count - 2);
            }

            Vector2 backForce =  Vector2.ClampMagnitude(ConnectedToRope[^2].transform.position - transform.position, 1) * _timeGoingBack;
            transform.Translate((_movementSpeed * Time.deltaTime * _direction) + backForce);
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
                Debug.Log("IS CURSED");
                _isCursed = true;
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
            ConnectedToRope.Insert(_lineIndex + 1, ray2D.collider.gameObject);
        }


        private void LineUpdate()
        {
            _line.positionCount = ConnectedToRope.Count;

            for (int i = 0; i < ConnectedToRope.Count; i++)
            {
                _line.SetPosition(i, ConnectedToRope[i].transform.position);
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
            float pointDistance = 0;
            for (int i = 1; i < ConnectedToRope.Count; i++)
            {
                if (ConnectedToRope[i] == null)
                {
                    LineArrayRemove(i);

                    continue;
                }

                pointDistance += Vector2.Distance(ConnectedToRope[i - 1].transform.position, ConnectedToRope[i].transform.position);
            }

            return pointDistance;
        }

        #endregion

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
            {
                return;
            }

            _rope.ActivateJoints();
            // transform.SendMessage("ActivateJoints", transform);
            if (collision.gameObject.CompareTag("Player") && _goBack)
            {
                Destroy(gameObject);
            
                return;
            }

            if(_goBack)
            {
                RopeComeBack();
                /* if (collision.gameObject == ConnectedToRope[^2])
                {
                    LineArrayRemove(ConnectedToRope.Count -2);
                }*/

                return;
            }

            /* if (!_isMoving)
            {
                Debug.Log("IS _isMoving");
                return;
            } */

            _isMoving = false;
            _connectedFinalTarget = collision.transform;

            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D hitRigidBody))
            {
                _rigidbody2D.bodyType = RigidbodyType2D.Static;
                _hingeJoint.connectedBody = hitRigidBody;
            }

            _enemyHit = collision.gameObject.GetComponent<BasicEnemy>();

            if (_enemyHit != null)
            {
                if (_isCursed)
                {
                    _enemyHit.Curse(gameObject);

                    return;
                }
                else
                {
                    OnObjectTriggered?.Invoke();
                }
            }

            if (collision.gameObject.name == "Curse")
            {
                OnCurseTriggered?.Invoke();
                // _isCursed = true;
                Debug.Log("CURSE");
                _isCursed = true;
            switch (collision.gameObject.tag)
            {
                default:
                    if (IsCursed) collision.SendMessage("Curse1", gameObject);
                    if (IsBlessed) collision.SendMessage("Bless");
                    break;
                case "Curse":
                    IsCursed = true;
                    break;
                case "Blessing":
                    IsBlessed = true;
                    break;
            }

            _isTarget = !_isTarget;
        }

        private void OnCollisionExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
            {
                return;
            }

            if (collision.gameObject.name == "Curse")
            switch (collision.gameObject.tag)
            {
                // _isCursed = false;
                _sentCurse = false;
                case "Curse":
                    IsCursed = false;
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

        private void OnDestroy()
        {
            /* for (int i = 0; i < ConnectedToRope.Count; i++)
            {
                ConnectedToRope[i].layer = 0;
            } */ 
        }

        private void RopeComeBack()
        {
            Destroy(gameObject);
            return;
            // _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("COMEBACK");
            //_hingeJoint.connectedBody = null;
            // _rigidbody2D.MovePosition(_player.transform.position);
            // _hingeJoint.connectedBody = null;
            // _rigidbody2D.gameObject.SetActive(false);
            // _rope.ComeBack();
            for (int i = 0; i < ConnectedToRope.Count; i ++)
            {
                //Destroy(ConnectedToRope[i]);
            }
        }
    }
}
