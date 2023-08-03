using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script base para os State Machines
namespace Gato.Gameplay
{
    public class StateMachine : MonoBehaviour
    {
        protected StateAI _state01;
        protected StateAI _state02;

        protected void Update()
        {
            if (_state01 != null)
                _state01.UpdateAction();

            if (_state02 != null)
                _state02.UpdateAction();
        }

        public void SetState01(StateAI state)
        {
            if (_state01 != null)
            {
                _state01.ExitAction();
            }

            _state01 = state;
            _state01.EntryAction();
        }

        public void SetState02(StateAI state)
        {
            if (_state02 != null)
            {
                _state02.ExitAction();
            }

            _state02 = state;
            _state02.EntryAction();
        }
    }
}
