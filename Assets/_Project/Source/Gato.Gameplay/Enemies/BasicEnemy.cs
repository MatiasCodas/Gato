using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class BasicEnemy : MonoBehaviour
    {
        public float TimeToDie = 3;
        public GameObject Target;
        public float Speed = 0.02f;

        public Sprite Left;
        public Sprite Right;
        public Sprite Up;
        public Sprite Down;
        private bool _cursed;
        [HideInInspector]
        public Rigidbody2D RB2D;
        [HideInInspector]
        public SpriteRenderer Sprite;
        [HideInInspector]
        public Vector2 PreviousPosition;
        [HideInInspector]
        public Vector2 NextPosition;
        [HideInInspector]
        public int MovementState;

        #region Base stuff to start any code inheriting this
        private void Start()
        {
            BasicStart();
        }
       

        private void FixedUpdate()
        {
            BasicMovement();
            FaceDirection();
            
        }
        private void Update()
        {
            BasicUpdate();
        }
        #endregion
        public void BasicStart()
        {
            RB2D = GetComponent<Rigidbody2D>();
            Sprite = GetComponent<SpriteRenderer>();
            NextPosition = transform.position;
            _cursed = false;
            Target = PlayerControlSystem.Player.gameObject;
        }

        public void BasicUpdate()
        {
            if (!_cursed) return;
            Sprite.color = Color.magenta;
        }

        public void BasicMovement()
        {
            NextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(Target.transform.position - transform.position, Speed);
            RB2D.MovePosition(NextPosition);
        }

        public void FaceDirection()//placeholder for  until we get some animations
        {
            
            Vector2 dir = Vector2.ClampMagnitude(NextPosition - (Vector2)transform.position, 1) *100;

            if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            {
                if (dir.x >= 0)
                {
                    Sprite.sprite = Right;
                    return;
                }
                Sprite.sprite = Left;
                return;
            }
            if (dir.y >= 0)
            {
                Sprite.sprite = Up;
                return;
            }
            Sprite.sprite = Down;

        }

        public void Curse(GameObject rope)
        {
            _cursed = true;
            StartCoroutine(TimerToDie(rope));
        }

        private IEnumerator TimerToDie(GameObject rope)
        {

            yield return new WaitForSeconds(TimeToDie);
            CurseProjectile.goAllBack = true;
            Destroy(gameObject);
            
           // Destroy(rope);
        }

        private void OnDestroy()
        {
            BattleManager.Instance.EnemyDestroyed();
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
