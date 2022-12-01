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

        public float timerToDestroy;

        

        public void Setup(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
            _ropeShooter = gameObject.GetComponent<CurseRopeShooter>();
        }

        public void ActivateCurse(bool cursed)
        {
            _ropeShooter.TargetHit(_collisionObject, cursed);
            
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
        private static GameObject currentTargetObject;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) return;
            if (!_isMoving) return;
            _isMoving = false;
            _collisionObject = collision.gameObject;

            if (collision.gameObject.name == "Curse")
            {
                OnCurseTriggered?.Invoke();
                _isCursed = true;
                return;
            }
            else
            {

                currentTargetObject = collision.gameObject;
            }

            OnObjectTriggered?.Invoke();
        }

        private IEnumerator SetTimer(bool both)
        {

            yield return new WaitForSeconds(timerToDestroy);
            if (!_isMoving && !both) yield break;
            if (both && currentTargetObject != null)
            {
                Destroy(currentTargetObject);
            }
            Destroy(gameObject);
        }
    }
}