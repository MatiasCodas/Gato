using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Door : MonoBehaviour
    {
        public Color CursedColor;
        public Color BlessedColor;
        public bool Blessed;
        public bool Open;
        public float DoorSpeedFactor;
        public Vector3 ClosedPosition;
        public Vector3 OpenPosition;
        public float ColorizingSpeedFactor;
        

        private Transform[] _doorSides;
        private SpriteRenderer [] _sprites;
        private float _timeLeftToColorize;

        private void Start()
        {
            _doorSides = GetComponentsInChildren<Transform>();
            _sprites = GetComponentsInChildren<SpriteRenderer>();
            _timeLeftToColorize = 3;

            _doorSides[1].localPosition = Open ? -OpenPosition : -ClosedPosition;
            _doorSides[2].localPosition = Open ? OpenPosition : ClosedPosition;
        }

        private void Update()
        {
            TryToColorize();
            if(Blessed)SetState();
        }

        private void TryToColorize()
        {
            if (_timeLeftToColorize <= 0) return;
            for (int i = 0; i < _sprites.Length; i++)
            {
                _sprites[i].color = Color.Lerp(_sprites[i].color, Blessed ? BlessedColor : CursedColor, ColorizingSpeedFactor);
            }
            _timeLeftToColorize -= Time.deltaTime;
        }

        private void SetState()
        {
            if (Open && _doorSides[1].position == OpenPosition) return;
            if (!Open && _doorSides[1].position == ClosedPosition) return;

            _doorSides[1].localPosition = Vector3.Lerp(_doorSides[1].localPosition, Open ? -OpenPosition : -ClosedPosition, DoorSpeedFactor);
            _doorSides[2].localPosition = Vector3.Lerp(_doorSides[2].localPosition, Open ? OpenPosition : ClosedPosition, DoorSpeedFactor);

        }

        private void Bless()
        {
            Blessed = true;
            _timeLeftToColorize = 3;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            Open = true;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!Blessed) Open = false;
        }

    }
}
