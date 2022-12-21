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
        public bool _isCursed;
        private GameObject _collisionObject;
        private Vector2 _direction;
        private CurseRopeShooter _ropeShooter;
        private LineRenderer _line;
        private static bool isTarget = true;
        public GameObject currentTargetObject;
        private float timeActive;

        public float timerToDestroy;

        
        public void Setup(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
            _ropeShooter = gameObject.GetComponent<CurseRopeShooter>();
            _collisionObject = null;
            _line = GetComponent<LineRenderer>();
        }

        public void ActivateCurse(bool cursed)
        {
            _ropeShooter.TargetHit(_collisionObject, _isCursed);
            
            if(_isCursed)
            {
               // StartCoroutine( SetTimer(true));
                _isCursed = false;

            }
        }

        private void FixedUpdate()
        {
            //StartCoroutine(SetTimer(false));
            
            RaycastHit2D ray2D = Physics2D.Raycast(transform.position, currentTargetObject.transform.position - transform.position);
            Debug.DrawRay(transform.position, currentTargetObject.transform.position - transform.position);
            LineUpdate();
            if (!_isMoving)
            {
                return;
            }

            timeActive += Time.deltaTime;
            Vector2 backForce = Vector2.ClampMagnitude(currentTargetObject.transform.position - transform.position, 1) * timeActive;
            transform.Translate((_direction * _movementSpeed * Time.deltaTime) + backForce);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("bateu em vc");
                Destroy(gameObject);
                return;
            }
            if (!_isMoving) return;
            transform.parent = collision.transform;
            _isMoving = false;
            _collisionObject = collision.gameObject;
            
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

        private void LineUpdate()
        {
            _line.positionCount = 2;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, currentTargetObject.transform.position);
        }
        private IEnumerator SetTimer(bool both)
        {

            yield return new WaitForSeconds(timerToDestroy);
            if (!_isMoving && !both) yield break;
            if (both && _collisionObject != null)
            {
                Destroy(currentTargetObject);
            }
            Destroy(gameObject);
        }
    }
}
