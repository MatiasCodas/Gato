using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Follow : StateAI
    {
        public readonly Enemy enemy;
        public Animator animController;
        private StateMachine stateMachine;

        public Follow(StateMachine _stateMachine, Enemy _enemy, Animator _animController)
        {
            stateMachine = _stateMachine;
            enemy = _enemy;
            animController = _animController;
        }

        public override void EntryAction()
        {
            animController.SetBool("Walking", true);
        }

        public override void ExitAction()
        {
            animController.SetBool("Walking", false);
        }

        public override void UpdateAction()
        {
            enemy.Follow();
        }
    }
}
