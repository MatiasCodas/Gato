using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    internal class ConfirmationPopoverScreen //: UIScreen
    {
        /*private readonly List<EventHandle> _eventHandles = new List<EventHandle>();

        [SerializeField]
        private Button _noButton;
        [SerializeField]
        private Button _yesButton;
        [SerializeField]
        private TMP_Text _messageText;

        private Action<bool> _onConfirm;
        private IEventService _eventService;

        public override void Setup()
        {
            base.Setup();

            if (!ServiceLocator.Shared.TryGet(out _eventService))
            {
                return;
            }

            _eventService.AddListener<OpenConfirmationPopoverEvent>(HandleConfirmationPopoverEvent, _eventHandles);

            _yesButton.onClick.AddListener(HandleYesButtonClick);
            _noButton.onClick.AddListener(HandleNoButtonClick);
        }

        public override void CloseSelf()
        {
            base.CloseSelf();
        }

        public override void Dispose()
        {
        }

        private void HandleConfirmationPopoverEvent(ref EventRef<OpenConfirmationPopoverEvent> eventRef)
        {
            _onConfirm = eventRef.Value.OnConfirmation;      
            _messageText.text = eventRef.Value.Message;      
        }

        private void HandleYesButtonClick()
        {
            _onConfirm?.Invoke(true);

            CloseSelf();
        }

        private void HandleNoButtonClick()
        {
            _onConfirm?.Invoke(false);

            CloseSelf();
        }*/
    }
}
