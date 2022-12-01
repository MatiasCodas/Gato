using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class BasicEnemy : MonoBehaviour
    {
        public float timeToDie = 3;
        private void Curse1()
        {
            StartCoroutine(TimerToDie());
        }

        private IEnumerator TimerToDie()
        {

            yield return new WaitForSeconds(timeToDie);
            Destroy(gameObject);
        }
    }
}
