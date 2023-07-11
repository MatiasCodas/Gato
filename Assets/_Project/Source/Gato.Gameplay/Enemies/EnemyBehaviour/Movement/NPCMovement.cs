using UnityEngine;

namespace Gato.Gameplay
{
    public enum NPCMovementType
    {
        FollowPlayer,
    }

    public abstract class NPCMovement : NPCBehaviour
    {
        [SerializeField]
        private NPCMovementType _type;

        public abstract void SetTarget(GameObject target);

        public abstract Vector2 Move(Transform currentPosition, float speed);
    }
}
