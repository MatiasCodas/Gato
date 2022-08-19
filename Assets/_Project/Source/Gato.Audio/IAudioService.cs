using Gato.Core;
using System.Collections.Generic;

namespace Gato.Audio
{
    public interface IAudioService : IService
    {
        AudioHandle PlayAudio(AudioData data, List<AudioHandle> appendList = null);

        void StopAudio(in AudioHandle handle);

        void StopAllAudios();
    }
}
