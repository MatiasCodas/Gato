using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gato.UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField]
        private string _nextSceneName;
        [SerializeField]
        private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(HandlePlayButtonPressed);
        }

        private void HandlePlayButtonPressed()
        {
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}
