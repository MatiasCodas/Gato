using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class InventoryAnim : MonoBehaviour
    {
        private void OnEnable()
        {
            DragDrop.OnDragging += ShowInventory;
            DragDrop.OnDropping += HideInventory;
            InventorySlot.OnDropping += delegate { StartCoroutine(HideAnimCoroutine()); };
            CauldronInventoryArea.OnNearCauldron += ShowInventory;
            CauldronInventoryArea.OnFarCauldron += HideInventory;
        }

        private void OnDisable()
        {
            DragDrop.OnDragging -= ShowInventory;
            DragDrop.OnDropping -= HideInventory;
            InventorySlot.OnDropping -= delegate { StartCoroutine(HideAnimCoroutine()); };
            CauldronInventoryArea.OnNearCauldron -= ShowInventory;
            CauldronInventoryArea.OnFarCauldron -= HideInventory;
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
