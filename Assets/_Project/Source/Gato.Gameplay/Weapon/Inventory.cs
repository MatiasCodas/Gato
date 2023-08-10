using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class Inventory : MonoBehaviour, IDropHandler
    {
        [SerializeField] private InputActionReference _mousePosInput;
        [SerializeField] private InputActionReference _gamepadStickPosInput;
        [SerializeField] private InputActionReference _dropItemInput;
        [SerializeField] private Transform _sceneObjects;

        private void OnEnable()
        {
            _dropItemInput.action.started += DropItemOnFloor;
            InventoryItem.PutInInventory += AddToInventory;
        }

        private void OnDisable()
        {
            _dropItemInput.action.started -= DropItemOnFloor;
            InventoryItem.PutInInventory -= AddToInventory;
        }

        private void DropItemOnFloor(InputAction.CallbackContext context)
        {
            if (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                child.transform.SetParent(_sceneObjects);

                Vector2 newPos = Vector2.zero;

                if (_gamepadStickPosInput.action.IsPressed())
                {
                    _mousePosInput.action.Disable();
                    newPos = Camera.main.ScreenToWorldPoint(_gamepadStickPosInput.action.ReadValue<Vector2>());
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
            }
        }

        private void AddToInventory(Transform itemTransform)
        {
            if (transform.childCount == 0)
            {
                itemTransform.SetParent(transform, false);
                itemTransform.localPosition = Vector3.zero;
                itemTransform.localScale = new Vector3(.8f, .8f, .8f);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            eventData.pointerDrag.transform.SetParent(transform, false);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.transform.localScale = new Vector3(.8f, .8f, .8f);
        }
    }
}
