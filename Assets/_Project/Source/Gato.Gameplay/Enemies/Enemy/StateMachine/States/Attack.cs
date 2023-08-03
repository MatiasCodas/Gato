using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Attack : StateAI
    {
        private readonly Enemy _enemy;
        private BotFSM _stateMachine;
        private Weapon _weapon;

        public Attack(BotFSM stateMachine, Enemy enemy, Weapon weapon)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
            _weapon = weapon;
        }

        public override void EntryAction()
        {
            // animController.SetTrigger("Attack");
            _weapon.canDamage = true;
        }

        public override void ExitAction()
        {
            _weapon.canDamage = false;
        }

        public override void UpdateAction()
        {
            _stateMachine.AttackCooldown();
        }
    }
}
