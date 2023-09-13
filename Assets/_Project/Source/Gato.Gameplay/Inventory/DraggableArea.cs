using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class DraggableArea : MonoBehaviour
    {
        public static DraggableArea Instance;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.GetComponentInParent<DragDropItem>())
                collision.transform.GetComponentInParent<DragDropItem>().CanDrag = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.GetComponentInParent<DragDropItem>())
                collision.transform.GetComponentInParent<DragDropItem>().CanDrag = false;
        }
    }
}
