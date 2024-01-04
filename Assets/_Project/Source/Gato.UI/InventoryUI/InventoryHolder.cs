using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    public class InventoryHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _HUD;
        [SerializeField] private GameObject _inventory;
        [SerializeField] private GameObject _pauseCanvas;
        [SerializeField] private GameObject _pauseContainer;
        [SerializeField] private Button _pauseButton;

        private Vector3 _originalInventoryPos;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(HideInventoryCanvas);

            ResumeInventory.OnBackToGameplay += BackToHUD;

            _originalInventoryPos = _inventory.transform.localPosition;
            _inventory.transform.SetParent(transform);
            _inventory.transform.localPosition = Vector3.zero;
        }

        private void OnDisable()
        {
            ResumeInventory.OnBackToGameplay -= BackToHUD;
        }

        private void BackToHUD()
        {
            _inventory.transform.localPosition = _originalInventoryPos;
            _inventory.transform.SetParent(_HUD.transform);
            transform.parent.gameObject.SetActive(false);
            _pauseCanvas.SetActive(true);
            _pauseContainer.SetActive(false);
        }

        private void HideInventoryCanvas()
        {
            _inventory.transform.localPosition = _originalInventoryPos;
            _inventory.transform.SetParent(_HUD.transform);
            transform.parent.gameObject.SetActive(false);
            _pauseCanvas.SetActive(true);
        }
    }
}
