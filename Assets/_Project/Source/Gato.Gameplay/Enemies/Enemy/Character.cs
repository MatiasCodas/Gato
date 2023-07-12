using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Character : MonoBehaviour
    {

        public float life = 100f;
        public float maxLife = 10f;

        public void GainLife(float value)
        {
            life += value;

            if (life > 100)
            {
                life = 100;
            }
        }

        public void TakeDamage(float _damage)
        {
            life -= _damage;
        }
    }
}
