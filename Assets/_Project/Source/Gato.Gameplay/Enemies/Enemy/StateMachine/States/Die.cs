using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Die : StateAI
    {

        public readonly Enemy enemy;
        public Animator animController;
        private StateMachine stateMachine;

        public Die(StateMachine _stateMachine, Enemy _enemy, Animator _animController)
        {
            stateMachine = _stateMachine;
            enemy = _enemy;
            animController = _animController;
        }

        public override void EntryAction()
        {
            animController.SetTrigger("Die");
            enemy.Die();
        }

        public override void ExitAction() { }

        public override void UpdateAction() { }
    }
}
