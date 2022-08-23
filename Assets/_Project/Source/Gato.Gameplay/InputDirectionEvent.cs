using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    public struct InputDirectionEvent : IEvent
    {
        public readonly Vector2 Direction;

        public InputDirectionEvent(Vector2 direction)
        {
            Direction = direction;
        }
    }
}
