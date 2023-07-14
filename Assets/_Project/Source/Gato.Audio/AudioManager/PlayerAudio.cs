using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudio : MonoBehaviour
    {
        public AudioSource PlayerAudioSource;
        public PlayerSFXLibrary PlayerSFX;
    }
}
