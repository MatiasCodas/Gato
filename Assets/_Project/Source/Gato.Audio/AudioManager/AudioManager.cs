using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gato.Gameplay;

namespace Gato.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private Dictionary<string, AudioSource> _audioSources;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _audioSources = new Dictionary<string, AudioSource>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
                _audioSources.Add(gameObject.transform.GetChild(i).GetComponent<AudioSource>().clip.name,
                    gameObject.transform.GetChild(i).GetComponent<AudioSource>());
        }

        private void Start()
        {
            InputControlManager.OnMovingSFX += MovementSFX;
        }

        private void OnDestroy()
        {
            InputControlManager.OnMovingSFX -= MovementSFX;
        }

        public string FindAudioClipName(string keyword)
        {
            foreach (string key in _audioSources.Keys)
                if (key.Contains(keyword, System.StringComparison.InvariantCultureIgnoreCase))
                    return key;
            return null;
        }

        public void ToggleSFX(string clipName, bool toggle)
        {
            AudioSource audioSource = _audioSources[clipName];

            if (toggle && !audioSource.isPlaying)
                audioSource.Play();
            else if (!toggle && audioSource.isPlaying)
                audioSource.Stop();
        }

        public void MovementSFX(bool isMoving)
        {
            // Presumably, the audio clips' names are well identified
            string audioClipName = FindAudioClipName("Foot");
            ToggleSFX(audioClipName, isMoving);
        }
    }
}
