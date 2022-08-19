using UnityEngine;

namespace Gato.Audio
{
    [CreateAssetMenu(menuName = "Create AudioData", fileName = "AudioData")]
    public sealed class AudioData : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private bool _loop;
        [SerializeField] private float _delay;

        public AudioClip AudioClip => _audioClip;

        public bool Loop => _loop;

        public float Delay => _delay;
    }
}
