using System;

namespace Gato.UI
{
    public struct OpenErrorPopoverEvent //: IEvent
    {
        public readonly string Message;

        public readonly Action OnConfirm;

        public OpenErrorPopoverEvent(string message, Action onConfirm)
        {
            Message = message;
            OnConfirm = onConfirm;
        }
    }
}
