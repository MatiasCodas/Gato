using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gato.UI
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _resetButton;
        [SerializeField]
        private Button _returnButton;

        private void Awake()
        {
            _returnButton.onClick.AddListener(HandleReturnButtonPressed);
            _resetButton.onClick.AddListener(HandleResetButtonPressed);
        }

        private void HandleResetButtonPressed()
        {
            Time.timeScale = 1;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        private void HandleReturnButtonPressed()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
