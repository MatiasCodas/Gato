using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Battle Stats")]
    public class BattleStats : ScriptableObject
    {
        public float BasicEnemiesDefeated;
        public float StartAtTime;
        public float FinishAtTime;
        public float BattleTime;
    }
}
