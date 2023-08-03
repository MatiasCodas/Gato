namespace Gato.Gameplay
{
    public class StrafeFollow : StateAI
    {
        private readonly Enemy _enemy;
        private BotFSM _stateMachine;

        public StrafeFollow(BotFSM stateMachine, Enemy enemy/*, Animator animController*/)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
            // animController = _animController;
        }

        public override void EntryAction()
        {
        }

        public override void ExitAction()
        {
        }

        public override void UpdateAction()
        {
            _enemy.StrafeFollow();
        }
    }
}
