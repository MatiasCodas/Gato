using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gato.Backend;

namespace Gato.Gameplay
{
    public class DoorPlatform : MonoBehaviour
    {
        public bool Blessed;
        public bool Open;
        public float DoorSpeedFactor;
        private Vector3 _closedPosition;
        private Vector3 _openPosition;


        private Transform[] _doorSides;
        private WorldInteractable _saveInfo;

        private void Start()
        {
            _doorSides = GetComponentsInChildren<Transform>();

            _openPosition = _doorSides[1].localPosition;
            _closedPosition = _doorSides[2].localPosition;

            _doorSides[2].localPosition = Open ? _openPosition : _closedPosition;
            StartCoroutine(LateStart());
        }

        private IEnumerator LateStart()
        {
            yield return new WaitForEndOfFrame();
            if (_saveInfo.Interacted)
            {
                Bless();
            }
        }
        private void Update()
        {
            if (Blessed) SetState();
        }


        private void SetState()
        {
            if (Open && _doorSides[1].position == _openPosition) return;
            if (!Open && _doorSides[1].position == _closedPosition) return;

            _doorSides[2].localPosition = Vector3.Lerp(_doorSides[2].localPosition, Open ? _openPosition : _closedPosition, DoorSpeedFactor);
            _doorSides[1].GetComponent<Collider2D>().enabled = false;

        }

        private void Bless()
        {
            Blessed = true;
            Open = true;
            _saveInfo.Interacted = true;
            Debug.Log("bless");
        }
    }
}
