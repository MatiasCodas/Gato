using Gato.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class BasicEnemy : MonoBehaviour
    {
        [SerializeField]
        protected BasicEnemyStats _stats;
        public GameObject Target;

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

        public float EnemyHitCooldown;

        public static Action OnIncreaseHitPoints;

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
            if (EnemyHitCooldown > 0)
                EnemyHitCooldown -= Time.deltaTime;

            if (!_cursed) return;
            Sprite.color = Color.magenta;
        }

        public void BasicMovement()
        {
            NextPosition = (Vector2)transform.position + Vector2.ClampMagnitude(Target.transform.position - transform.position, _stats.Speed);
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

            yield return new WaitForSeconds(_stats.TimeToDie);
            CurseProjectile.GoAllBack = true;
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
                if (EnemyHitCooldown <= 0)
                {
                    collision.transform.SendMessage("EnemyHit");
                    OnIncreaseHitPoints?.Invoke();
                    AudioManager.Instance.ToggleSFX(collision.transform.GetComponent<AudioSource>(), collision.transform.GetComponent<PlayerControlSystem>().PlayerSFX.DamageSFX);
                    EnemyHitCooldown = 2f;
                    CollisionShock(collision);
                }
            }
        }

        private void CollisionShock(Collision2D collision)
        {
            RB2D.velocity = Vector2.zero;

            float enemyX = transform.position.x;
            float enemyY = transform.position.y;
            float playerX = collision.transform.position.x;
            float playerY = collision.transform.position.y;

            float enemyCollisionOffset = .5f;
            float playerCollisionOffset = .5f;

            Vector3 dir = transform.position - collision.transform.position;
            dir.Normalize();

            // transform.position = new Vector3(enemyX + (enemyCollisionOffset * dir.x), enemyY + (enemyCollisionOffset * dir.y), 0);
            collision.transform.position = new Vector3(playerX + (playerCollisionOffset * -dir.x), playerY + (playerCollisionOffset * -dir.y), 0);
        }
    }
}
