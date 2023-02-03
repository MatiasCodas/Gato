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

        private bool _isMoving;
        public static bool IsCursed;
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

        public float TimerToDestroy;
        private float _timeGoingBack;
        public bool GoBack = false;
        public static bool goAllBack = false;
        
        public void Setup(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
            _line = GetComponent<LineRenderer>();
            _layerMask = LayerMask.GetMask("Default");

        }


        public void ActivateCurse(bool cursed)
        {
            
            
        }

        private void Update() 
        {
            try { LineUpdate(); }
            catch { 
                LineIndexReset();
                GoBack = true;
            }
            // gambiarra, favor trocar os inputs pro inputmanager depois
            // very nested also, sounds like a good place to refactor
            if (Input.GetKeyDown(KeyCode.Q) || _timeActive > _playerStats.RopeTime || LineSize() >= _playerStats.RopeSize || goAllBack)
            {
                GoBack = true;
                goAllBack = false;
            }
            if (!_isMoving)
            {
                _timeActive += Time.deltaTime;
                GoBack = _connectedFinalTarget == null ? true : GoBack;
            }
            if (GoBack) return;

            try{ transform.position = Vector3.Lerp(transform.position, _connectedFinalTarget.position, 0.1f);}
            catch { Debug.Log("You got an error but it's not a big deal so I'm doing this chill message");} //this error doesn't do much so I wanted a cooler error message



        }

        private void FixedUpdate()
        {
            LineCollisions();
            if (IsCursed) SendNewCurseToAll();
            if (!_isMoving && !GoBack)
            {
                _timeGoingBack = 1.5f;
                return;
            }
            _timeGoingBack += Time.deltaTime;
            if (GoBack && _timeGoingBack > 5) LineArrayRemove(ConnectedToRope.Count - 2);
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
            if (ray2D.collider.name == "Curse") IsCursed = true; ;
            if (IsCursed) ray2D.collider.SendMessage("Curse1", gameObject);
            if (GoBack && _connectedFinalTarget != null ) return;
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
            if (_sentCurse) return;
            _sentCurse = true;
            for (int i = 0; i < ConnectedToRope.Count; i++)
            {
                ConnectedToRope[i].transform.SendMessage("Curse1", gameObject);
            }
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
                return;
            }
            if(GoBack)
            {
                if (collision.gameObject == ConnectedToRope[^2])
                {
                    LineArrayRemove(ConnectedToRope.Count -2);
                }
                return;
            }
            if (!_isMoving) return;
            //transform.parent = collision.transform;
            _isMoving = false;
            _connectedFinalTarget = collision.transform;

            if (IsCursed) collision.SendMessage("Curse1", gameObject);
            if (collision.gameObject.name == "Curse")
            {
                IsCursed = true;
            }
        

            _isTarget = !_isTarget;
           // ActivateCurse(IsCursed);
            /*
            if(isTarget)
            {
                OnCurseTriggered?.Invoke();
                return;
            }
            
            OnObjectTriggered?.Invoke();*/
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Curse")
            {
                IsCursed = false;
                _sentCurse = false;
            }
            if(_connectedFinalTarget == null) GoBack = true;
            if (!GoBack) return;
            if (collision.gameObject.layer != 7) return;
            collision.gameObject.layer = 0;
            
            
        }






        private void OnDestroy()
        {
            for (int i = 0; i < ConnectedToRope.Count; i++)
            {
                ConnectedToRope[i].layer = 0;
            }
        }
        
    }
}
