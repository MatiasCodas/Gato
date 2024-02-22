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
            Vector3 difference = transform.position - transform.parent.position;
            _aimTransform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) - 90);
        }
    }
}
