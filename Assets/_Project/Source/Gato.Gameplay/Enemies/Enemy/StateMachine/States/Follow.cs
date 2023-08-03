using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Follow : StateAI
    {
        private readonly Enemy _enemy;
        private StateMachine _stateMachine;

        // public Animator animController;
        
        public Follow(StateMachine stateMachine, Enemy enemy/*, Animator animController*/)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
            
            // animController = _animController;
        }

        public override void EntryAction()
        {
            Debug.LogError("EntryAction");
            // animController.SetBool("Walking", true);
        }

        public override void ExitAction()
        {
            // animController.SetBool("Walking", false);
        }

        public override void UpdateAction()
        {
            Debug.LogError("UpdateAction");
            _enemy.Follow();
        }
    }
}
