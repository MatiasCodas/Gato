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

        private bool _isMoving;
        public static bool _isCursed;
        private GameObject _collisionObject;
        private Vector2 _direction;
        public CurseRopeShooter _ropeShooter;
        private LineRenderer _line;
        private static bool isTarget = true;
        //public GameObject currentTargetObject;
        public List<GameObject> connectedToRope;
        private float timeActive;
        private int layerMask;

        public float timerToDestroy;

        
        public void Setup(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
            //_ropeShooter = gameObject.GetComponent<CurseRopeShooter>();
            _collisionObject = null;
            _line = GetComponent<LineRenderer>();
            layerMask = LayerMask.GetMask("Default");
        }


        public void ActivateCurse(bool cursed)
        {
           // _ropeShooter.TargetHit(_collisionObject, _isCursed);
            
            
        }

        private void FixedUpdate()
        {
            //StartCoroutine(SetTimer(false));

            LineCollisions();
            LineUpdate();
            if (!_isMoving)
            {
                return;
            }

            timeActive += Time.deltaTime;
            Vector2 backForce = Vector2.ClampMagnitude(connectedToRope[connectedToRope.Count-2].transform.position - transform.position, 1) * timeActive;
            transform.Translate((_direction * _movementSpeed * Time.deltaTime) + backForce);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
                return;
            }
            if (!_isMoving) return;
            transform.parent = collision.transform;
            _isMoving = false;
            _collisionObject = collision.gameObject;

            if (_isCursed) collision.SendMessage("Curse1", gameObject);
            if (collision.gameObject.name == "Curse")
            {
                _isCursed = true;
            }
        

            isTarget = !isTarget;
            ActivateCurse(_isCursed);
            /*
            if(isTarget)
            {
                OnCurseTriggered?.Invoke();
                return;
            }
            
            OnObjectTriggered?.Invoke();*/
        }

        private void LineCollisions()//this is almost working, I'm trying
        {
            Vector2 target = connectedToRope[connectedToRope.Count - 2].transform.position;
            RaycastHit2D ray2D = Physics2D.Raycast(transform.position, target - (Vector2)transform.position, Vector2.Distance(target, transform.position), layerMask);
            Debug.DrawRay(transform.position, target-(Vector2)transform.position, ray2D.collider != null ? Color.blue : Color.red);
            if (ray2D.collider == null) return;
            for (int i = 0; i < connectedToRope.Count; i++)
            {
                if (ray2D.collider.gameObject == connectedToRope[i]) return;
            }
            Debug.Log(ray2D.collider.name);
            connectedToRope.Insert(connectedToRope.Count-1, ray2D.collider.gameObject);
           
        }
        private void LineUpdate()
        {
            _line.positionCount = connectedToRope.Count;
            //_line.SetPosition(0, transform.position);
            for (int i = 0; i < connectedToRope.Count; i++)
            {
                _line.SetPosition(i, connectedToRope[i].transform.position);
            }
        }
        private IEnumerator SetTimer(bool both)
        {

            yield return new WaitForSeconds(timerToDestroy);
            if (!_isMoving && !both) yield break;
            if (both && _collisionObject != null)
            {
                Destroy(connectedToRope[0]);
            }
            Destroy(gameObject);
        }
    }
}
