namespace Gato.Gameplay
{
    public class Shoot : StateAI
    {
        private readonly RangeWeapon _weapon;
        private BotFSM _stateMachine;

        public Shoot(BotFSM stateMachine, RangeWeapon weapon)
        {
            _stateMachine = stateMachine;
            _weapon = weapon;
            // animController = _animController;
        }

        public override void EntryAction()
        {
            _weapon.Shoot();
            // animController.SetTrigger("Shoot");
        }

        public override void ExitAction() { }

        public override void UpdateAction()
        {
            _stateMachine.AttackCooldown();
        }
    }
}
