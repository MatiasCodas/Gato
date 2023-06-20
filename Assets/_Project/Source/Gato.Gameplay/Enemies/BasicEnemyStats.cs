using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemies/BasicEnemy")]
    public class BasicEnemyStats : ScriptableObject
    {
        public float Speed = 0.02f;
        public float TimeToDie = 3;
    }
}
