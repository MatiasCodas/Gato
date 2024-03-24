using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Explosion : MonoBehaviour
    {

        private void Update()
        {
            transform.localScale = transform.localScale * 1.02f;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
