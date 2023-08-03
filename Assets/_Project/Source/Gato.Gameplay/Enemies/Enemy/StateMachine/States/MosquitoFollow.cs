using System;

namespace Gato.Gameplay
{
    public class MosquitoFollow : StateAI
    {
        private readonly Enemy _enemy;
        private BotFSM _stateMachine;

        public MosquitoFollow(BotFSM stateMachine, Enemy enemy/*, Animator animController*/)
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
            _enemy.MosquitoFollow();
        }
    }
}
