using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private List<AudioSource> _audioClips;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            DontDestroyOnLoad(this);

            _audioClips = new List<AudioSource>();
            for (int i = 0; i < gameObject.transform.childCount; i++)
                _audioClips.Add(gameObject.transform.GetChild(i).GetComponent<AudioSource>());
        }


        // Temporary - WIP:

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
    }
}
