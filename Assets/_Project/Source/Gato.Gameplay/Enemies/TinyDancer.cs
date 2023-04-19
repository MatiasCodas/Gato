using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class TinyDancer : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public Color BlessColor;
        public Collider2D Collider;
        private void Bless()
        {
            SpriteRenderer.color = BlessColor;
            Collider.enabled = false;
        }
    }
}
