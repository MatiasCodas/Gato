using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    internal class Player : MonoBehaviour, IPlayer
    {
        private const string HorizontalAnimatorParameter = "Horizontal";
        private const string VerticalAnimatorParameter = "Vertical";

        [SerializeField]
        private Animator _animator;

        public void Setup()
        {

        }
    }
}
