using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    public class PlayerMoveEvent : IEvent
    {
        public readonly Vector2 Direction;

        public PlayerMoveEvent(Vector2 direction)
        {
            Direction = direction;
        }
    }
}
