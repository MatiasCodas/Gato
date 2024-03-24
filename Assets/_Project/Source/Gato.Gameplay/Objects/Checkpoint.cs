using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField]
        private int _index = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                CheckpointManager checkpointManager = collision.gameObject.GetComponent<CheckpointManager>();
                checkpointManager.SetCheckpoint(transform.position, _index);
            }
        }
    }
}
