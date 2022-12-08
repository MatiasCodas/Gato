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

        public Sprite left;
        public Sprite right;
        public Sprite up;
        public Sprite down;
        private bool cursed;

        private Rigidbody2D rigidbody2D;
        private SpriteRenderer sprite;

        private Vector2 previousPosition;
        private Vector2 nextPosition;

        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            nextPosition = transform.position;
            cursed = false;
        }

        private void FixedUpdate()
        {
            nextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(target.transform.position - transform.position, speed);
            rigidbody2D.MovePosition(nextPosition);
            FaceDirection();
        }
        private void Update()
        {
            if (!cursed) return;
            sprite.color = Color.magenta;
        }

        private void FaceDirection()//placeholder for  until we get some animations
        {
            Vector2 dir = Vector2.ClampMagnitude(nextPosition - (Vector2)transform.position, 1) *100;
            Debug.Log(dir);

            if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            {
                if (dir.x >= 0)
                {
                    sprite.sprite = right;
                    return;
                }
                sprite.sprite = left;
                return;
            }
            if (dir.y >= 0)
            {
                sprite.sprite = up;
                return;
            }
            sprite.sprite = down;

        }

        private void Curse1(GameObject rope)
        {
            cursed = true;
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
