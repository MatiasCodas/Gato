using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemies/BullEnemy")]
    public class BullStats : ScriptableObject
    {
        public float DashSpeed;
        public float TelegraphTime;
        public float DistanceToAggro;
        public float ChargeTime;
        public float RestTime;
    }
}
