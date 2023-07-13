using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gato.Gameplay;

namespace Gato.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private Dictionary<string, AudioSource> _audioSources;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _audioSources = new Dictionary<string, AudioSource>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
                _audioSources.Add(gameObject.transform.GetChild(i).GetComponent<AudioSource>().clip.name, 
                    gameObject.transform.GetChild(i).GetComponent<AudioSource>());

            InputControlManager.OnMovingSFX += PlayMovementSFX;
        }

        private void OnDestroy()
        {
            InputControlManager.OnMovingSFX -= PlayMovementSFX;
        }

        public void PlayAudioSource(string clipName)
        {
            AudioSource audioSource = _audioSources[clipName];
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        public void StopAudioSource(string clipName)
        {
            AudioSource audioSource = _audioSources[clipName];
            if (audioSource.isPlaying)
                audioSource.Stop();
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
