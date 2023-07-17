using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Audio
{
    [CreateAssetMenu(fileName = "PlayerSFXLibrary", menuName = "ScriptableObjects/Audio/PlayerSFXLibrary")]
    public class PlayerSFXLibrary : ScriptableObject
    {
        public AudioClip WalkSFX;
        public AudioClip ThrowRopeSFX;
        public AudioClip BoostByRopeSFX;
        public AudioClip TeleportingSFX;
        public AudioClip HitByEnemySFX;
    }
}
