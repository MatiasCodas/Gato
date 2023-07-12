using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script base para os State Machines
namespace Gato.Gameplay
{
    public class StateMachine : MonoBehaviour
    {
        protected StateAI state01;
        protected StateAI state02;

        protected void Update()
        {
            if (state01 != null)
                state01.UpdateAction();

            if (state02 != null)
                state02.UpdateAction();
        }

        public void SetState01(StateAI _state)
        {
            if (state01 != null)
            {
                state01.ExitAction();
            }

            state01 = _state;
            state01.EntryAction();
        }

        public void SetState02(StateAI _state)
        {
            if (state02 != null)
            {
                state02.ExitAction();
            }

            state02 = _state;
            state02.EntryAction();
        }
    }
}
