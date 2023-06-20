using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Teleport : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public float ColorSpeed;
        public Transform TeleportTo;

        private void Update()
        {
            float h, s, v;
            Color.RGBToHSV(SpriteRenderer.color, out h, out s, out v);
            SpriteRenderer.color = Color.HSVToRGB(h + ColorSpeed, s, v);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            collision.gameObject.transform.position = TeleportTo.position;
        }
    }
}
