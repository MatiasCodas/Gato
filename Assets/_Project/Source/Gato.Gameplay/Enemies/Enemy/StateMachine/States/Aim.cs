using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gato.Gameplay
{
    public class Aim : StateAI
    {
        public readonly Enemy enemy;
        public Animator animController;
        private StateMachine stateMachine;

        public Aim(StateMachine _stateMachine, Enemy _enemy, Animator _animController)
        {
            stateMachine = _stateMachine;
            enemy = _enemy;
            animController = _animController;
        }

        public override void EntryAction()
        {
            animController.SetBool("Aim", true);
        }

        public override void ExitAction()
        {
            animController.SetBool("Aim", false);
        }

        public override void UpdateAction()
        {
            enemy.Aim();
        }
    }
}
