using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public float MovementSpeed;
        public float RopeTime;
        public float RopeSize;
        public float RopeCooldown;
    }
}
