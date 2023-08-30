using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gato.Gameplay
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private InputActionReference _mousePosInput;
        [SerializeField] private InputActionReference _gamepadStickPosInput;
        [SerializeField] private InputActionReference _inventoryInput;
        [SerializeField] private Transform _sceneObjects;

        private bool _canDrop;

        private void Awake()
        {
            _canDrop = false;
        }

        private void OnEnable()
        {
            _inventoryInput.action.started += DropItemOnFloor;
            InventoryItem.PutInInventory += AddToInventory;
        }

        private void OnDisable()
        {
            _inventoryInput.action.started -= DropItemOnFloor;
            InventoryItem.PutInInventory -= AddToInventory;
        }

        private void DropItemOnFloor(InputAction.CallbackContext context)
        {
            if (_canDrop)
            {
                if (transform.childCount > 0)
                {
                    Transform child = transform.GetChild(0);
                    child.transform.SetParent(_sceneObjects);

                    Vector2 newPos = Vector2.zero;

                    if (_gamepadStickPosInput.action.IsPressed())
                    {
                        _mousePosInput.action.Disable();
                        /// Temporary:
                        // newPos = _gamepadStickPosInput.action.ReadValue<Vector2>();
                        newPos = Vector2.zero;
                        child.transform.localPosition = new Vector3(-10f, 38f, 0);
                    }
                    else if (!_gamepadStickPosInput.action.IsPressed())
                    {
                        _mousePosInput.action.Enable();
                        newPos = Camera.main.ScreenToWorldPoint(_mousePosInput.action.ReadValue<Vector2>());
                    }

                    if (_mousePosInput.action.IsPressed())
                    {
                        newPos = Camera.main.ScreenToWorldPoint(_mousePosInput.action.ReadValue<Vector2>());
                    }

                    child.transform.position = newPos;
                    child.transform.localScale = new Vector3(.0065f, .0065f, .0065f);
                    child.transform.GetComponent<DragDrop>().CanDrop = false;
                }
            }
        }

        private void AddToInventory(Transform itemTransform)
        {
            if (transform.childCount == 0)
            {
                itemTransform.SetParent(transform, false);
                itemTransform.localPosition = Vector3.zero;
                itemTransform.localScale = new Vector3(.8f, .8f, .8f);
                itemTransform.transform.GetComponent<DragDrop>().CanDrop = true;
                StartCoroutine(InventoryCoolDown());
            }
        }

        private IEnumerator InventoryCoolDown()
        {
            yield return new WaitForSeconds(1.25f);
            _canDrop = true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            eventData.pointerDrag.transform.SetParent(transform, false);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.transform.localScale = new Vector3(.8f, .8f, .8f);
            eventData.pointerDrag.transform.GetComponent<DragDrop>().CanDrop = true;
            _canDrop = true;
        }
    }
}
