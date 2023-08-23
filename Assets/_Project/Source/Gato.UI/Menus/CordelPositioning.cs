using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    public class CordelPositioning : MonoBehaviour
    {
        private Vector2 _resolution;
        public int ActiveScreenIndex;
        private float _varalXPosition;

        [SerializeField]
        private float _varalSpeed;
        [SerializeField]
        private List<GameObject> _cordelOrder;

        private void Start()
        {
            _resolution = new Vector2( Screen.currentResolution.width, Screen.currentResolution.height);
            for (int i = 0; i < transform.childCount; i++)
            {
                _cordelOrder.Add(transform.GetChild(i).gameObject);
                _cordelOrder[i].transform.localPosition = new Vector3(_resolution.x * (i - ActiveScreenIndex), 0, 0);
            }

        }

        private void Update()
        {
            if (_varalXPosition == ActiveScreenIndex) return;
            _varalXPosition = Mathf.Lerp(_varalXPosition, ActiveScreenIndex, _varalSpeed);

            for (int i = 0; i < _cordelOrder.Count; i++)
            {
                _cordelOrder[i].transform.localPosition = new Vector3(_resolution.x * (i - _varalXPosition), 0, 0);
            }
        }

        public void SetIndex(int indexInput)
        {
            ActiveScreenIndex = indexInput;
        }
    }
}
