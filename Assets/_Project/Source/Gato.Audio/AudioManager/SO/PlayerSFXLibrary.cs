using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Audio
{
    [CreateAssetMenu(fileName = "PlayerSFX", menuName = "ScriptableObjects/Audio/PlayerSFX")]
    public class PlayerSFXLibrary : ScriptableObject
    {
        public AudioClip WalkSFX;
        public AudioClip ThrowRopeSFX;
        public AudioClip BoostByRopeSFX;
        public AudioClip TeleportingSFX;
    }
}
