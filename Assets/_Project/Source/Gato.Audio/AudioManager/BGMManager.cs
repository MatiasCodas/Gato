using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gato.Audio
{
    public class BGMManager : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _audioSources;
        private void Start()
        {
            _audioSources = GetComponentsInChildren<AudioSource>();
            switch (SceneManager.GetActiveScene().name)
            {
                default:
                    _audioSources[0].loop = true;
                    _audioSources[0].Play();
                    break;
            }
        }

        

        private void Update()
        {
        
        }
    }
}
