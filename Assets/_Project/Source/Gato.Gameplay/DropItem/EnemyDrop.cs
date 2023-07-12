using Gato.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public enum DropType { Arrow, Life }
    public class EnemyDrop : MonoBehaviour
    {

        public DropType dropType = DropType.Arrow;
        public int dropValue;
        [HideInInspector]public ObjectPool pool;
        public FloatVariable currentAmmo;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                switch (dropType)
                {
                    case DropType.Arrow:
                        currentAmmo.value += dropValue;
                        break;

                    case DropType.Life:
                        Character character = other.GetComponent<Character>();
                        character.GainLife(dropValue);
                        break;
                }
			
                pool.ReturnToPool(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
