using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gato.Gameplay;

namespace Gato.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _audioClips;

        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            DontDestroyOnLoad(this);

            _audioClips = new List<AudioSource>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
                _audioClips.Add(gameObject.transform.GetChild(i).GetComponent<AudioSource>());

            InputControlManager.OnMovingSFX += PlayMovementSFX;
        }

        private void OnDestroy()
        {
            InputControlManager.OnMovingSFX -= PlayMovementSFX;
        }

        public void PlayAudioSource(string clipName)
        {
            if (_audioClips.Find(x => x.clip.name == clipName && !x.isPlaying))
                _audioClips.Find(x => x.clip.name == clipName).Play();
        }

        public void StopAudioSource(string clipName)
        {
            if (_audioClips.Find(x => x.clip.name == clipName && x.isPlaying))
                _audioClips.Find(x => x.clip.name == clipName).Stop();
        }

        public void PlayMovementSFX(Vector2 direction)
        {
            if (direction != Vector2.zero)
                PlayAudioSource("FootStep");
            else
                StopAudioSource("FootStep");
        }
    }
}
