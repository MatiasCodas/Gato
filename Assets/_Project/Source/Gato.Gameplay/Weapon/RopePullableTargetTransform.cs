using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class RopePullableTargetTransform : MonoBehaviour
    {
        public Transform RopePullableTargetPosition;

        private void Update()
        {
            if (RopePullableTargetPosition.childCount > 1)
                for (int i = 2; i < RopePullableTargetPosition.childCount; i++)
                    RopePullableTargetPosition.GetChild(i).parent = null;
        }
    }
}
