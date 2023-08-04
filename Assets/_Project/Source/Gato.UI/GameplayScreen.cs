using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    public class GameplayScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private GameObject _pauseMenuContainer;
        [SerializeField]
        private Button _quitButton;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(HandlePauseButtonPressed);
            _quitButton.onClick.AddListener(QuitGame);
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandlePauseButtonPressed();
            }
        }

        private void HandlePauseButtonPressed()
        {
            _pauseMenuContainer.SetActive(true);
            Time.timeScale = 0;
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}
