using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CauldronInventoryArea : MonoBehaviour
    {
        public static Action OnNearCauldron;
        public static Action OnFarCauldron;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("Player"))
                OnNearCauldron?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            OnFarCauldron?.Invoke();
        }
    }
}
