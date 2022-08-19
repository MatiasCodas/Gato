using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    [RequireComponent(typeof(Button))]
    public class ConfirmationPopoverButton : MonoBehaviour
    {
        public enum ConfirmationType
        {
        }

        public delegate void HandleConfirmation(bool onConfirmation);

        public event HandleConfirmation OnConfirm;

        [SerializeField]
        private Button _button;
        [SerializeField]
        private ConfirmationType _type;

        private string _message;

        public void SetInteractable(bool value)
        {
            _button.interactable = value;
        }

        private void Awake()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            //IPopoverService popoverService = ServiceLocator.Shared.Get<IPopoverService>();

            //popoverService.OpenConfirmationPopover(_message, HandleConfirmationButtonClick);
        }

        private void HandleConfirmationButtonClick(bool onConfirmation)
        {
            OnConfirm?.Invoke(onConfirmation);
        }
    }
}
