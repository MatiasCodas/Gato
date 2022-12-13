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
        private static bool isTarget = true;
        private static GameObject currentTargetObject;

        public float timerToDestroy;

        

        public void Setup(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
            _ropeShooter = gameObject.GetComponent<CurseRopeShooter>();
            _collisionObject = null;
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
            StartCoroutine(SetTimer(false));
            if (!_isMoving)
            {
                return;
            }

            transform.Translate(_direction * _movementSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) return;
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
