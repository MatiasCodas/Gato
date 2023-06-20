using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemies/StraferEnemy")]
    public class StraferStats : ScriptableObject
    {
        public float MovementVariation;
        public float ShakeStrength;
    }
}
