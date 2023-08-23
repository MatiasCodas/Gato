using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class DragDrop : MonoBehaviour, IDragHandler
    {
        [SerializeField] private InputActionReference _mousePos;
        
        private Transform _originalParent;

        private void Awake()
        {
            _originalParent = transform.root;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerDrag.transform.root != _originalParent)
            {
                eventData.pointerDrag.transform.SetParent(_originalParent);
                eventData.pointerDrag.transform.localScale = new Vector3(.0065f, .0065f, .0065f);
            }

            if (eventData.pointerDrag.transform.root == _originalParent)
            {
                Vector3 newPos = Camera.main.ScreenToWorldPoint(_mousePos.action.ReadValue<Vector2>());
                newPos = new Vector3(newPos.x, newPos.y, 0);
                transform.position = newPos;
            }
        }
    }
}
