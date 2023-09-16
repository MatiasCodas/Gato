using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    public class ResumeInventory : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;

        public static Action OnBackToGameplay;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(Resume);
        }

        private void OnDestroy()
        {
            _resumeButton.onClick.RemoveAllListeners();
        }

        private void Resume()
        {
            Time.timeScale = 1;
            OnBackToGameplay?.Invoke();
        }
    }
}
