using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Weapon : MonoBehaviour
    {
        public float damageValue = 10f;
        public bool canDamage = false;
        public string targetTag;
    
        public virtual void OnTriggerEnter(Collider other)
        {
            if (!canDamage) return;
            if (other.CompareTag(targetTag))
            {
                Character character = other.GetComponent<Character>();
                character.TakeDamage(damageValue);
                canDamage = false;
            }
        }
    }
}
