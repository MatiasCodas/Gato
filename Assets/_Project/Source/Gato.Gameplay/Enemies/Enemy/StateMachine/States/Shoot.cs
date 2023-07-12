using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class Shoot : StateAI
    {
        public readonly RangeWeapon weapon;
        public Animator animController;
        private BotFSM stateMachine;

        public Shoot(BotFSM _stateMachine, RangeWeapon _weapon, Animator _animController)
        {
            stateMachine = _stateMachine;
            weapon = _weapon;
            animController = _animController;
        }

        public override void EntryAction()
        {
            weapon.Shoot();
            animController.SetTrigger("Shoot");
        }

        public override void ExitAction() { }

        public override void UpdateAction()
        {
            stateMachine.AttackCooldown();
        }
    }
}
