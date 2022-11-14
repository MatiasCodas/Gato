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
        private GameObject _collisionObject;
        private Vector2 _direction;
        private CurseRopeShooter _ropeShooter;

        

        public void Setup(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
            _ropeShooter = gameObject.GetComponent<CurseRopeShooter>();
        }

        public void ActivateCurse()
        {
            _ropeShooter.TargetHit(_collisionObject);
        }

        private void FixedUpdate()
        {
            if (!_isMoving)
            {
                return;
            }

            transform.Translate(_direction * _movementSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _isMoving = false;
            _collisionObject = collision.gameObject;

            if (collision.gameObject.name == "Curse")
            {
                OnCurseTriggered?.Invoke();

                return;
            }

            OnObjectTriggered?.Invoke();
        }
    }
}
