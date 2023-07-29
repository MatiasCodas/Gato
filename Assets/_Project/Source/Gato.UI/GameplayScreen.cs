using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Gato.UI
{
    public class GameplayScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private GameObject _pauseMenuContainer;

        [SerializeField]
        private InputActionReference _pauseInput;


        private void Awake()
        {
            _pauseButton.onClick.AddListener(HandlePauseButtonPressed);
        }

        private void FixedUpdate()
        {
            if (_pauseInput.action.IsPressed())
            {
                HandlePauseButtonPressed();
            }
        }

        private void HandlePauseButtonPressed()
        {
            _pauseMenuContainer.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
