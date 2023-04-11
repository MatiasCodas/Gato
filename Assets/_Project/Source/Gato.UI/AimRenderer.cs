using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    public class AimRenderer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _aimTransform;

        private void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            _aimTransform.position = mousePos;
        }
    }
}
