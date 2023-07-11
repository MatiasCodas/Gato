using Pathfinding;
using UnityEngine;

namespace Gato.Gameplay
{
    public class FollowPlayerMovement : NPCMovement
    {
        [SerializeField]
        private AIDestinationSetter _destinationSetter;

        private Vector2 _nextPosition;

        public override void Initialize()
        {
            SetTarget(PlayerControlSystem.Player.gameObject);
        }

        public override void SetTarget(GameObject target)
        {
            _destinationSetter.target = target.transform;
        }

        public override Vector2 Move(Transform currentPosition, float speed)
        {
            Vector3 position = currentPosition.position;

            return (Vector2)position + Vector2.ClampMagnitude(_destinationSetter.target.position - position, speed);
        }
    }
}
