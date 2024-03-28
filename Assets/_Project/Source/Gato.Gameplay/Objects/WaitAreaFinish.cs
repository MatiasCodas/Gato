using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class WaitAreaFinish : MonoBehaviour
    {
        [SerializeField] private GameObject _enemySpawner;
        [SerializeField] private Collider2D _boxCollider;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _boxCollider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = Color.grey;
            _boxCollider.enabled = false;

        }

        private void Update()
        {
            if (_enemySpawner != null) return;
            _spriteRenderer.color = Color.white;
            _boxCollider.enabled = true;

        }
    }
}
