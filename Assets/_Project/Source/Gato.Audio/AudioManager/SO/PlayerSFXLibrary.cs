using UnityEngine;

namespace Gato.Audio
{
    [CreateAssetMenu(fileName = "PlayerSFXLibrary", menuName = "ScriptableObjects/Audio/PlayerSFXLibrary")]
    public class PlayerSFXLibrary : ScriptableObject
    {
        public AudioClip IdleSFX;
        public AudioClip WalkSFX;
        public AudioClip DamageSFX;
        public AudioClip ThrowRopeSFX;
        public AudioClip BoostByRopeSFX;
        public AudioClip TeleportingSFX;
        public AudioClip HitSFX;
        public AudioClip MissSFX;
    }
}
