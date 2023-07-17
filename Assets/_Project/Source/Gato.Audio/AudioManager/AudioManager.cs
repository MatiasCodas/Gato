using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            DontDestroyOnLoad(this);
        }

        public void ToggleSFX(AudioSource audioSource, AudioClip audioClip, bool toggle = true)
        {
            if (toggle && !audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else if (!toggle && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
