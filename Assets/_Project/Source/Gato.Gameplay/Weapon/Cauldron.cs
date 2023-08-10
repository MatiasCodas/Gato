using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Cauldron : MonoBehaviour
    {
        [Header("Test")]
        [SerializeField] private bool _isTest;
        [SerializeField] private GameObject _dragDropPrefab;

        [Space(5)]
        [Header("Cauldron")]
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("DragDrop"))
            {
                Destroy(collision.gameObject);
                _particleSystem.Play();
                if (_isTest)
                    StartCoroutine(ReinitiateDragDropTest());
            }
        }

        private IEnumerator ReinitiateDragDropTest()
        {
            yield return new WaitForSeconds(4f);
            _particleSystem.Stop();
            GameObject dragDropClone = Instantiate(_dragDropPrefab, transform.parent);
            dragDropClone.transform.localPosition = new Vector3(-10f, -38f, 0);
        }
    }
}
