using System;

namespace Gato.UI
{
    public class OpenConfirmationPopoverEvent //: IEvent
    {
        public readonly string Message;
        public readonly Action<bool> OnConfirmation;

        public OpenConfirmationPopoverEvent(string message, Action<bool> onConfirmation)
        {
            Message = message;
            OnConfirmation = onConfirmation;
        }
    }
}
