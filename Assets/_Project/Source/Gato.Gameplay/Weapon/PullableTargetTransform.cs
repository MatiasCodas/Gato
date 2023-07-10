using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class PullableTargetTransform : MonoBehaviour
    {
        public Transform PullableTargetPosition;

        private void Update()
        {
            if (PullableTargetPosition.childCount > 1)
                for (int i = 2; i < PullableTargetPosition.childCount; i++)
                    PullableTargetPosition.GetChild(i).parent = null;
        }
    }
}
