using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    public class InventoryMenuOption : MonoBehaviour
    {
        [SerializeField] private Button _inventoryOptionButton;
        [SerializeField] private GameObject _inventoryCanvas;
        [SerializeField] private GameObject _pauseCanvas;

        private void Awake()
        {
            _inventoryOptionButton.onClick.AddListener(OpenInventoryCanvas);
        }

        private void OnDestroy()
        {
            _inventoryOptionButton.onClick.RemoveAllListeners();
        }

        private void OpenInventoryCanvas()
        {
            _pauseCanvas.SetActive(false);
            _inventoryCanvas.SetActive(true);
        }
    }
}
