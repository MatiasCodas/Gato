using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class DragDrop : MonoBehaviour
    {
        [SerializeField] private InputActionReference _dragDropUInputAction;
        [SerializeField] private InputActionReference _mousePositionInputAction;

        private Transform _dragDropObject;
        private Vector3 _clickPos;

        private void OnEnable()
        {
            _dragDropUInputAction.action.started += Drag;
            _dragDropUInputAction.action.canceled += Drop;
        }

        private void OnDisable()
        {
            _dragDropUInputAction.action.started -= Drag;
            _dragDropUInputAction.action.canceled -= Drop;
        }

        private void Update()
        {
            MousePosition();
            if (_dragDropObject != null)
                _dragDropObject.position = _clickPos;
        }

        private void MousePosition()
        {
            _clickPos = Camera.main.ScreenToWorldPoint(_mousePositionInputAction.action.ReadValue<Vector2>());
            _clickPos = new Vector3(_clickPos.x, _clickPos.y, 0);
        }

        private void Drag(InputAction.CallbackContext context)
        {
            RaycastHit2D hit = Physics2D.Raycast(_clickPos, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == transform.tag)
                _dragDropObject = hit.transform;
        }

        private void Drop(InputAction.CallbackContext context)
        {
            _dragDropObject = null;
        }
    }
}
