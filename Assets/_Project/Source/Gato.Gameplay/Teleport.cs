using System;
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
        public GameObject Player;
        public static Action OnTeleporting;

        public virtual void Update()
        {
            float h, s, v;
            Color.RGBToHSV(SpriteRenderer.color, out h, out s, out v);
            SpriteRenderer.color = Color.HSVToRGB(h + ColorSpeed, s, v);
        }
        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            Player = collision.gameObject;
            TeleportNow(Vector3.zero);
        }

        public virtual void TeleportNow(Vector3 offset)
        {
            if (!Player.CompareTag("Player")) return;
            Player.transform.position = TeleportTo.position + offset;
            OnTeleporting?.Invoke();
        }
    }
}
