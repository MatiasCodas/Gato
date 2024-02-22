using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CheckpointManager : MonoBehaviour
    {
        private Vector3 _firstCheckpoint;
        private Vector3 _currentCheckpoint;
        private int _currentCheckpointIndex;

        public void GoToCheckpoint()
        {
            gameObject.transform.position = _currentCheckpoint;
        }

        public void SetCheckpoint(Vector3 checkpoint, int index)
        {
            if (_currentCheckpointIndex >= index)
            {
                return;
            }

            _currentCheckpoint = checkpoint;
            _currentCheckpointIndex = index;
        }

        private void Awake()
        {
            _firstCheckpoint = transform.position;
            _currentCheckpoint = _firstCheckpoint;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Hole")
            {
                GoToCheckpoint();
            }
        }
    }
}
