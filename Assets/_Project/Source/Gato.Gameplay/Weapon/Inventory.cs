using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gato.Gameplay
{
    public class Inventory : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            eventData.pointerDrag.transform.SetParent(transform, false);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.transform.localScale = new Vector3(.8f, .8f, .8f);
        }
    }
}
