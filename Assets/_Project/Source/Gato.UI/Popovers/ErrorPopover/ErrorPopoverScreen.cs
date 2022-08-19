using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    internal class ErrorPopoverScreen : UIScreen
    {
        /*private const string ConnectionErrorMessage = "Check internet connection!";

        private readonly List<EventHandle> _eventHandles = new List<EventHandle>();

        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private Button _tryAgainButton;
        [SerializeField]
        private TMP_Text _messageText;

        private Action _onClose;
        private IEventService _eventService;

        public override void Setup()
        {
            base.Setup();

            if (!ServiceLocator.Shared.TryGet(out _eventService))
            {
                return;
            }

            _eventService.AddListener<OpenErrorPopoverEvent>(HandleOpenPopoverEvent, _eventHandles);
            _closeButton.onClick.AddListener(HandleCloseButtonClick);
            _tryAgainButton.onClick.AddListener(HandleTryAgainButtonClick);
        }

        public override void CloseSelf()
        {
            _onClose?.Invoke();

            Dispose();

            base.CloseSelf();
        }

        public override void Dispose()
        {
            foreach (EventHandle eventHandle in _eventHandles)
            {
                _eventService?.RemoveListener(eventHandle);
            }

            _eventHandles.Clear();
        }

        private void HandleOpenPopoverEvent(ref EventRef<OpenErrorPopoverEvent> eventRef)
        {
            _messageText.text = eventRef.Value.Message;
            _onClose = eventRef.Value.OnConfirm;

            _tryAgainButton.gameObject.SetActive(_onClose != null);
        }

        private void HandleTryAgainButtonClick()
        {
            CloseSelf();
        }

        private void HandleCloseButtonClick()
        {
            CloseSelf();
        }*/
    }
}
