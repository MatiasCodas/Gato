using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Audio
{
    [CreateAssetMenu(fileName = "EnemySFXLibrary", menuName = "ScriptableObjects/Audio/EnemySFXLibrary")]
    public class EnemySFXLibrary : ScriptableObject
    {
        public AudioClip CurseSFX;
    }
}
