using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class BasicEnemy : MonoBehaviour
    {
        public float timeToDie = 3;
        public GameObject target;
        public float speed = 1;


        private Rigidbody2D rigidbody2D;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rigidbody2D.MovePosition((Vector2)transform.position + Vector2.ClampMagnitude(target.transform.position - transform.position, speed));
        }

        private void Curse1(GameObject rope)
        {
            StartCoroutine(TimerToDie(rope));
        }

        private IEnumerator TimerToDie(GameObject rope)
        {

            yield return new WaitForSeconds(timeToDie);
            Destroy(gameObject);
            Destroy(rope);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.name == "Player")
            {
                Destroy(collision.gameObject);

            }
            
        }

    }
}
