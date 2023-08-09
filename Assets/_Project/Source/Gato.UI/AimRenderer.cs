using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Gato.UI
{
    public class AimRenderer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _aimTransform;

        [SerializeField]
        private InputActionReference _aimPos;

        private void Update()
        {
            Vector3 mousePos = _aimPos.action.ReadValue<Vector2>();
            _aimTransform.position = mousePos;
        }
    }
}
