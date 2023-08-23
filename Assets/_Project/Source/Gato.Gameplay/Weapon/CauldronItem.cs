using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class CauldronItem : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inventoryInput;

        private Transform _itemTransform;

        private bool _playerIsColliding;

        public static Action<Transform> PutInCauldron;

        private void Awake()
        {
            _playerIsColliding = false;
        }

        private void OnEnable()
        {
            _inventoryInput.action.started += AddToCauldron;
        }

        private void OnDisable()
        {
            _inventoryInput.action.started -= AddToCauldron;
        }

        private void AddToCauldron(InputAction.CallbackContext context)
        {
            if (_playerIsColliding)
            {
                FindItemsInInventory();
                PutInCauldron?.Invoke(_itemTransform);
            }
        }

        private void FindItemsInInventory()
        {
            Inventory[] slots = FindObjectsOfType<Inventory>();
            foreach (Inventory slot in slots)
            {
                if (slot.transform.childCount > 0)
                    _itemTransform = slot.transform.GetChild(0).transform;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.transform.tag.Equals("Player"))
                _playerIsColliding = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _playerIsColliding = false;
        }
    }
}
