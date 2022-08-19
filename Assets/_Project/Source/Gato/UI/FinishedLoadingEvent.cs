using Gato.Core;

namespace Gato
{
    public readonly struct FinishedLoadingEvent : IEvent
    {
        public readonly bool NeedSession;

        public FinishedLoadingEvent(bool needSession)
        {
            NeedSession = needSession;
        }
    }
}
