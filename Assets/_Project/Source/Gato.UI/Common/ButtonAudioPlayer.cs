using Gato.Audio;
using Gato.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    public class ButtonAudioPlayer : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private AudioData[] _audioDatas;

        private IAudioService _audioService;

        private void Awake()
        {
            _button.onClick.AddListener(HandleButtonClick);

            _audioService = ServiceLocator.Shared.Get<IAudioService>();
        }

        private void HandleButtonClick()
        {
            for (int i = 0; i < _audioDatas.Length; i++)
            {
                _audioService?.PlayAudio(_audioDatas[i]);
            }
        }
    }
}
