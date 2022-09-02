using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    internal class Player : MonoSystem, IPlayer
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        private Vector2 _direction;
        [SerializeField]
        private float _moveSpeed; //might be better to turn it into a float var scriptableObject later? Might be useful to integrate with the CharacterAnimationComponent too

        public override void Setup()
        {
        }

        public override void Tick(float deltaTime)
        {
            _direction = new Vector2(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));
            transform.Translate(_direction*_moveSpeed); //really basic movement for early prototyping and checking the feeling of it, no collision expected yet
        }
    }
}
