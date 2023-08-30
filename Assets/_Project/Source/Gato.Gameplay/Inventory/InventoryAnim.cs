using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class InventoryAnim : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inventoryGameplayDisplay;
        [SerializeField] private InputActionReference _inventoryMenuDisplay;

        private void Awake()
        {
            HideInventory();
        }

        private void OnEnable()
        {
            _inventoryGameplayDisplay.action.started += GameplayDisplay;
            _inventoryMenuDisplay.action.started += MenuDisplay;

            DragDropItem.OnDragging += ShowInventory;
            DragDropItem.OnDropping += HideInventory;
            InventorySlot.OnDropping += delegate { StartCoroutine(HideAnimCoroutine()); };
            CauldronInventoryArea.OnNearCauldron += ShowInventory;
            CauldronInventoryArea.OnFarCauldron += HideInventory;
        }

        private void OnDisable()
        {
            _inventoryGameplayDisplay.action.started -= GameplayDisplay;
            _inventoryMenuDisplay.action.started -= MenuDisplay;

            DragDropItem.OnDragging -= ShowInventory;
            DragDropItem.OnDropping -= HideInventory;
            InventorySlot.OnDropping -= delegate { StartCoroutine(HideAnimCoroutine()); };
            CauldronInventoryArea.OnNearCauldron -= ShowInventory;
            CauldronInventoryArea.OnFarCauldron -= HideInventory;
        }

        private void GameplayDisplay(InputAction.CallbackContext context)
        {
            if (transform.position.y == -150)
            {
                ShowInventory();
                StartCoroutine(HideAnimCoroutine());
            }
            else if (transform.position.y == 0)
            {
                StopCoroutine(HideAnimCoroutine());
                HideInventory();
            }
        }

        private void MenuDisplay(InputAction.CallbackContext context)
        {
            // TODO
        }

        private void ShowInventory()
        {
            transform.DOMoveY(0, .5f);
        }

        private void HideInventory()
        {
            transform.DOMoveY(-150, .5f);
        }

        private IEnumerator HideAnimCoroutine()
        {
            yield return new WaitForSeconds(1.5f);
            transform.DOMoveY(-150, .5f);
        }
    }
}
