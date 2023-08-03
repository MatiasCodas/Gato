using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gato.Gameplay
{
    public class Aim : StateAI
    {
        public readonly Enemy _enemy;
        private StateMachine _stateMachine;

        public Aim(StateMachine stateMachine, Enemy enemy)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
        }

        public override void EntryAction()
        {
            // animController.SetBool("Aim", true);
        }

        public override void ExitAction()
        {
            // animController.SetBool("Aim", false);
        }

        public override void UpdateAction()
        {
            _enemy.Aim();
        }
    }
}
