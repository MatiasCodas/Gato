using System;
using UnityEngine.Scripting;

namespace Gato.Audio
{
    [Preserve]
    [Serializable]
    public readonly struct AudioHandle : IEquatable<AudioHandle>
    {
        public readonly Guid Guid;

        private AudioHandle(Guid guid)
        {
            Guid = guid;
        }

        public bool IsValid => Guid != Guid.Empty;

        internal static AudioHandle Create()
        {
            return new AudioHandle(Guid.NewGuid());
        }

        public static bool operator ==(AudioHandle left, AudioHandle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AudioHandle left, AudioHandle right)
        {
            return !left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            return obj is AudioHandle other && Equals(other);
        }

        public bool Equals(AudioHandle other)
        {
            return Guid.Equals(other.Guid);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }
}
