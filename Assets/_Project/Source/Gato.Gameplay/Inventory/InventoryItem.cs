using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inventoryInput;

        private bool _playerIsColliding;

        public static Action<Transform> PutInInventory;

        private void Awake()
        {
            _playerIsColliding = false;
        }

        private void OnEnable()
        {
            _inventoryInput.action.started += AddToInvetory;
        }

        private void OnDisable()
        {
            _inventoryInput.action.started -= AddToInvetory;
        }

        private void AddToInvetory(InputAction.CallbackContext context)
        {
            if (_playerIsColliding)
                PutInInventory?.Invoke(transform.parent);
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
