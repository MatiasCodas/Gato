using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Gato.Backend;

namespace Gato.UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField]
        private string _nextSceneName;
        [SerializeField]
        private Button _newGameButton, _loadGameButton, _optionsButton, _creditsButton;
        [SerializeField]
        private Button[] _returnToMainButton;
        [SerializeField]
        private DataPersistenceManager _saveGameData;

        private CordelPositioning _cordelPositioning;

        private void Start()
        {
            _cordelPositioning = GetComponent<CordelPositioning>();
            _newGameButton.onClick.AddListener(HandleNewGameButtonPressed);
            _loadGameButton.onClick.AddListener(HandleLoadGameButtonPressed);
            if (_saveGameData.GameData == null)
                _loadGameButton.gameObject.SetActive(false);
            _optionsButton.onClick.AddListener(HandleOptionsButtonPressed);
            _creditsButton.onClick.AddListener(HandleCreditsButtonPressed);
            for (int i = 0; i < _returnToMainButton.Length; i++)
            {
                _returnToMainButton[i].onClick.AddListener(HandleReturnToMainButtonPressed);
            }
        }

        private void HandleNewGameButtonPressed()
        {
            _saveGameData.NewGame();
            SceneManager.LoadScene(_nextSceneName);
        }

        private void HandleLoadGameButtonPressed()
        {
            SceneManager.LoadScene(_saveGameData.GameData.Scene);
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
