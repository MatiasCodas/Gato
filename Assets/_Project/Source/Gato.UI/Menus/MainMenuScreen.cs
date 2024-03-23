using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Gato.Backend;
using UnityEngine.EventSystems;

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

        [SerializeField]
        private GameObject _mainMenuFirstOption;
        [SerializeField]
        private GameObject _optionsFirstOption;
        [SerializeField]
        private GameObject _creditsFirstOption;

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
            EventSystem.current.SetSelectedGameObject(_mainMenuFirstOption);
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
            EventSystem.current.SetSelectedGameObject(_mainMenuFirstOption);
        }
        private void HandleOptionsButtonPressed()
        {
            _cordelPositioning.SetIndex(1);
            EventSystem.current.SetSelectedGameObject(_optionsFirstOption);
        }
        private void HandleCreditsButtonPressed()
        {
            _cordelPositioning.SetIndex(2);
            EventSystem.current.SetSelectedGameObject(_creditsFirstOption);
        }
    }
}
