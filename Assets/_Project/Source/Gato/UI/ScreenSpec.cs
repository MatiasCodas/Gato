using System;

namespace Gato
{
    public struct ScreenSpec : IEquatable<ScreenSpec>
    {
        public ScreenData Data;
        /// <summary>
        /// Should the screen hide the previously active screen, if any.
        /// </summary>
        public bool IsAdditive;
        /// <summary>
        /// Should this screen be closable by any screens that are opened after it. Persistent screens cannot be closed by any screens other than itself.
        /// </summary>
        public bool IsPersistent;

        public bool IsValid => Data != null;

        public bool Equals(ScreenSpec other)
        {
            return Data == other.Data && IsAdditive == other.IsAdditive && IsPersistent == other.IsPersistent;
        }
    }
}
