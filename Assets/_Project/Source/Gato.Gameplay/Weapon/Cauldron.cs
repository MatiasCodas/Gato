using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gato.Gameplay
{
    public class Cauldron : MonoBehaviour, IDropHandler
    {
        [SerializeField] private bool _testing;
        [SerializeField] private GameObject _dragDropPrefab;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable()
        {
            CauldronItem.PutInCauldron += AddToCauldron;
        }

        private void OnDisable()
        {
            CauldronItem.PutInCauldron -= AddToCauldron;
        }

        private void AddToCauldron(Transform itemTransform)
        {
            if (itemTransform != null)
            {
                Destroy(itemTransform.gameObject);
                _particleSystem.Play();
                if (_testing)
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

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.transform.GetComponent<DragDrop>())
            {
                Destroy(eventData.pointerDrag.gameObject);
                _particleSystem.Play();
                if (_testing)
                    StartCoroutine(ReinitiateDragDropTest());
            }
        }
    }
}
