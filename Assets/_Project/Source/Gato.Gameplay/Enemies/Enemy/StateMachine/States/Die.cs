using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Die : StateAI
    {

        private readonly Enemy _enemy;
        private StateMachine _stateMachine;

        public Die(StateMachine stateMachine, Enemy enemy)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
            // animController = _animController;
        }

        public override void EntryAction()
        {
            // animController.SetTrigger("Die");
            _enemy.Die();
        }

        public override void ExitAction() { }

        public override void UpdateAction() { }
    }
}
