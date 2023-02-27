using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class BasicExplodingStone : MonoBehaviour
    {
        public GameObject explosion;
        private CircleCollider2D collider2D;

        private void Start()
        {
            collider2D = GetComponent<CircleCollider2D>();
        }

        private void Curse1()
        {
            collider2D.enabled = false;
            explosion.SetActive(true);
        }
    }
}
