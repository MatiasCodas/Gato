using Gato.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Audio
{
    internal sealed class AudioSystem : MonoSystem, IAudioService
    {
        private readonly Dictionary<AudioHandle, AudioSource> _instances = new Dictionary<AudioHandle, AudioSource>();

        private GameObject _gameObject;
        private ManagedPool<AudioSource> _pool;

        public ServiceLocator OwningLocator { get; set; }

        public override void Setup()
        {
            _gameObject = gameObject;
            _pool = new ManagedPool<AudioSource>(CreatePoolItem);
            _pool.OnDelete += HandlePoolDelete;
            _pool.OnPop += HandlePoolPop;
            _pool.OnPush += HandlePoolPush;

            DontDestroyOnLoad(_gameObject);
            ServiceLocator.Shared.Set<IAudioService>(this);
        }

        public override void Dispose()
        {
            foreach (KeyValuePair<AudioHandle, AudioSource> pair in _instances)
            {
                StopAudio(pair.Key);
            }

            _instances.Clear();

            ServiceLocator.Shared.Set<IAudioService>(null);
        }

        public AudioHandle PlayAudio(AudioData data, List<AudioHandle> appendList = null)
        {
            AudioHandle handle = AudioHandle.Create();
            AudioSource source = _pool.Pop();

            _instances.Add(handle, source);

            source.clip = data.AudioClip;
            source.loop = data.Loop;

            if (data.Delay > 0f)
            {
                source.PlayDelayed(data.Delay);
            }
            else
            {
                source.Play();
            }

            appendList?.Add(handle);

            return handle;
        }

        public void StopAudio(in AudioHandle handle)
        {
            if (!_instances.TryGetValue(handle, out AudioSource source))
            {
                return;
            }

            source.Stop();
            _pool.Push(source);
        }

        public void StopAllAudios()
        {
            foreach (AudioSource audioSource in _instances.Values)
            {
                audioSource.Stop();
            }
        }

        private void HandlePoolDelete(AudioSource item)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }

        private void HandlePoolPop(AudioSource item)
        {
            item.enabled = true;
        }

        private void HandlePoolPush(AudioSource item)
        {
            item.enabled = false;
        }

        private AudioSource CreatePoolItem()
        {
            AudioSource instance = _gameObject.AddComponent<AudioSource>();

            return instance;
        }
    }
}
