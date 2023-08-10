using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gato.Gameplay
{
    public class Inventory : MonoBehaviour, IDropHandler
    {
        private void OnEnable()
        {
            InventoryItem.PutInInventory += AddToInventory;
        }

        private void OnDisable()
        {
            InventoryItem.PutInInventory -= AddToInventory;
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
