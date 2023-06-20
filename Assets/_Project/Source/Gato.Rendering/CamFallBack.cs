using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Rendering
{
    public class CamFallBack : MonoBehaviour
    {
        private Vector3 _origin;
        private Vector3 _previousPosition;
        public float LerpFactor;
        
        private void Start()
        {
            _origin = transform.localPosition;
            
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(_previousPosition, _origin + transform.parent.position, LerpFactor);
            _previousPosition = transform.position;
        }
    }
}
