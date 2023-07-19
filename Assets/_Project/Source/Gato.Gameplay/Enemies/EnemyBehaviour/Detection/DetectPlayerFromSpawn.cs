using System;
using UnityEngine;

namespace Gato.Gameplay
{
    public class DetectPlayerFromSpawn : NPCDetection
    {
        [SerializeField]
        private DetectionType _detectionType;

        public override void Initialize()
        {
            Detected(NPCActionTypes.Movement);
        }
    }
}
