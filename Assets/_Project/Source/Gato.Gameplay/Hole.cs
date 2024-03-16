using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Hole : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Player")
            {
                PlayerControlSystem player = collision.gameObject.GetComponent<PlayerControlSystem>();

                if (!player.IsImmuneToHole)
                {
                    Debug.Log("GOT");
                    CheckpointManager checkpointManager = collision.gameObject.GetComponent<CheckpointManager>();
                    checkpointManager.GoToCheckpoint();
                }
            }
        }
    }
}
