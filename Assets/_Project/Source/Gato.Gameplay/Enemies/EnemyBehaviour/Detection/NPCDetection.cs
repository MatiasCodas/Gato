using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public enum DetectionType
    {
        FollowPlayer,
    }

    public class NPCDetection : NPCBehaviour
    {
        public delegate void HandleDetection(NPCActionTypes action);

        public event HandleDetection OnDetection;

        public bool HasDetected { get; set; }

        public override void Initialize()
        {
        }

        public virtual void Detected(NPCActionTypes actionAfterDetection)
        {
            OnDetection?.Invoke(actionAfterDetection);
        }
    }
}
