using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Attack : StateAI
    {
        public readonly Enemy enemy;
        public Animator animController;
        private BotFSM stateMachine;
        private Weapon weapon;

        public Attack(BotFSM _stateMachine, Enemy _enemy, Animator _animController, Weapon _weapon)
        {
            stateMachine = _stateMachine;
            enemy = _enemy;
            animController = _animController;
            weapon = _weapon;
        }

        public override void EntryAction()
        {
            animController.SetTrigger("Attack");
            weapon.canDamage = true;
        }

        public override void ExitAction()
        {
            weapon.canDamage = false;
        }

        public override void UpdateAction()
        {
            stateMachine.AttackCooldown();
        }
    }
}
