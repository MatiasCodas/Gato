using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gato.Gameplay
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        private BasicEnemyStats _stats;
        [SerializeField]
        private NPCMovement[] _movements;
        [SerializeField]
        private NPCDetection _detection;

        private Vector2 _nextPosition;
        private NPCMovement _currentMovement;

        private void Start()
        {
            _detection.OnDetection += HandleDetection;
            _detection.Initialize();

            foreach (NPCMovement movement in _movements)
            {
                movement.Initialize();
            }
        }

        private void HandleDetection(NPCActionTypes action, NPCBehaviour behaviourafterdetection)
        {
            switch (action)
            {
                case NPCActionTypes.Movement:
                {
                    foreach (NPCMovement movement in _movements)
                    {
                        _currentMovement = movement;
                    }
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            _nextPosition = _currentMovement.Move(transform, _stats.Speed);
            _rigidbody2D.MovePosition(_nextPosition);
        }

        private void HandleDetection(NPCActionTypes type)
        {
            switch (type)
            {
                case NPCActionTypes.Movement:
                {
                    foreach (NPCMovement movement in _movements)
                    {
                        _currentMovement = movement;
                    }
                    break;
                }
            }
        }
    }
}
