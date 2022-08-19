using System;

namespace Gato
{
    public struct ScreenActivation : IEquatable<ScreenActivation>
    {
        public IUIScreen Screen;
        public ScreenSpec Spec;

        public bool IsValid => !Screen.IsUnityNull();

        public bool Equals(ScreenActivation other)
        {
            return Equals(Screen, other.Screen) && Spec.Equals(other.Spec);
        }
    }
}
