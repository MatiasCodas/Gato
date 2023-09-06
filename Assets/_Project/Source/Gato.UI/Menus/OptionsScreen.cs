using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gato.UI
{
    public class OptionsScreen : MonoBehaviour
    {
        [SerializeField]
        private Slider _soundSlider, _sfxSlider, _musicSlider;

        [SerializeField]
        private Button _windowModeCycleNext, _windowModeCycleBack, _resolutionCycleNext, _resolutionCycleBack, _confirmResolutionChanges, _confirmResolutionChangesHiddenX;

        [Space(10)]
        [Header("Resolution Specifications")]
        [SerializeField]
        private string[] _windowModes;

        [HideInInspector]
        public int WindowModeIndex = 0;

        [SerializeField]
        private TMP_Text _windowModeButtonText;

        [SerializeField]
        private Vector2[] _resolutions;

        [HideInInspector]
        public int ResolutionIndex = 0;

        [SerializeField]
        private TMP_Text _resolutionButtonText;

        [SerializeField]
        private GameObject _confirmationScreen;

        [SerializeField]
        private Button _confirmationYes, _confirmationNo;

        [SerializeField]
        private TMP_Text _timeToReturnResolution;


        private void Awake()
        {
            _windowModeCycleNext.onClick.AddListener(HandleWindowCycleNext);
            _windowModeCycleBack.onClick.AddListener(HandleWindowCycleBack);
            _resolutionCycleNext.onClick.AddListener(HandleResolutionCycleNext);
            _resolutionCycleBack.onClick.AddListener(HandleResolutionCycleBack);
            _confirmResolutionChanges.onClick.AddListener(HandleConfirmResolutionChanges);
            _confirmResolutionChangesHiddenX.onClick.AddListener(HandleConfirmResolutionChanges);
            _confirmationYes.onClick.AddListener(HandleConfirmationYes);
            _confirmationNo.onClick.AddListener(HandleConfirmationNo);
            _confirmationScreen.SetActive(false);
            
            WindowModeIndex--;
            HandleWindowCycleNext();
            ResolutionIndex--;
            HandleResolutionCycleNext();
            SetConfirmationButtons(false);
        }


        private void HandleWindowCycleNext()
        {
            WindowModeIndex++;
            if (WindowModeIndex >= _windowModes.Length)
            {
                WindowModeIndex = 0;
            }
            _windowModeButtonText.text = _windowModes[WindowModeIndex];
            SetConfirmationButtons(true);
        }

        private void HandleWindowCycleBack()
        {
            WindowModeIndex--;
            if (WindowModeIndex < 0)
            {
                WindowModeIndex = _windowModes.Length-1;
            }
            _windowModeButtonText.text = _windowModes[WindowModeIndex];
            SetConfirmationButtons(true);
        }

        private FullScreenMode ScreenMode()
        {
            switch (WindowModeIndex)
            {
                default:
                case 0:
                    return FullScreenMode.ExclusiveFullScreen;
                case 1:
                    return FullScreenMode.Windowed;
                case 2:
                    return FullScreenMode.FullScreenWindow;
            }
        }

        private void HandleResolutionCycleNext()
        {
            ResolutionIndex++;
            if (ResolutionIndex >= _resolutions.Length)
            {
                ResolutionIndex = 0;
            }
            _resolutionButtonText.text = _resolutions[ResolutionIndex].x + " x " + _resolutions[ResolutionIndex].y;
            SetConfirmationButtons(true);
        }

        private void HandleResolutionCycleBack()
        {
            ResolutionIndex--;
            if (ResolutionIndex < 0)
            {
                ResolutionIndex = _resolutions.Length-1;
            }
            _resolutionButtonText.text = _resolutions[ResolutionIndex].x + " x " + _resolutions[ResolutionIndex].y;
            SetConfirmationButtons(true);
        }

        private void SetConfirmationButtons(bool isActiv)
        {

            _confirmResolutionChanges.gameObject.SetActive(isActiv);
            _confirmResolutionChangesHiddenX.gameObject.SetActive(isActiv);
        }


        private Resolution _previousResolution;
        private FullScreenMode _previousScreenMode;
        
        private void HandleConfirmResolutionChanges()
        {
            _previousResolution = Screen.currentResolution;
            _previousScreenMode = Screen.fullScreenMode;
            Screen.SetResolution((int)_resolutions[ResolutionIndex].x, (int)_resolutions[ResolutionIndex].y, ScreenMode());
            _confirmationScreen.SetActive(true);
        }

        private void HandleConfirmationYes()
        {
            _previousResolution = Screen.currentResolution;
            _previousScreenMode = Screen.fullScreenMode;
            _confirmationScreen.SetActive(false);
            SetConfirmationButtons(false);
        }

        private void HandleConfirmationNo()
        {
            Screen.SetResolution(_previousResolution.width, _previousResolution.height, _previousScreenMode);
            _confirmationScreen.SetActive(false);
            SetConfirmationButtons(false); ;
        }

        
    }
}
