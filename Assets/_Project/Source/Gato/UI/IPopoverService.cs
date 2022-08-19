using System;

namespace Gato
{
    public interface IPopoverService //: IService
    {
        void OpenItemPopover(string itemId);

        void OpenErrorPopover(string message, Action onClosePopover);

        void OpenConfirmationPopover(string message, Action<bool> onConfirmation);
    }
}
