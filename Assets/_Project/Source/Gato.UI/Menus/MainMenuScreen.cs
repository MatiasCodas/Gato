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
        private Button _playButton, _optionsButton, _creditsButton;
        [SerializeField]
        private Button[] _returnToMainButton;

        private CordelPositioning _cordelPositioning;

        private void Awake()
        {
            _cordelPositioning = GetComponent<CordelPositioning>();
            _playButton.onClick.AddListener(HandlePlayButtonPressed);
            _optionsButton.onClick.AddListener(HandleOptionsButtonPressed);
            _creditsButton.onClick.AddListener(HandleCreditsButtonPressed);
            for (int i = 0; i < _returnToMainButton.Length; i++)
            {
                _returnToMainButton[i].onClick.AddListener(HandleReturnToMainButtonPressed);
            }
        }

        private void HandlePlayButtonPressed()
        {
            SceneManager.LoadScene(_nextSceneName);
        }

        private void HandleReturnToMainButtonPressed()
        {
            _cordelPositioning.SetIndex(0);
        }
        private void HandleOptionsButtonPressed()
        {
            _cordelPositioning.SetIndex(1);
        }
        private void HandleCreditsButtonPressed()
        {
            _cordelPositioning.SetIndex(2);
        }
    }
}
