using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.UI
{
    public class PopoverSystem //: MonoSystem, IPopoverService
    {
        /*private readonly List<EventHandle> _eventHandles = new List<EventHandle>();

        [SerializeField]
        private ScreenData _homeScreenData;
        [SerializeField]
        private ScreenData _errorPopoverScreenData;
        [SerializeField]
        private ScreenData _itemPopoverScreenData;
        [SerializeField]
        private ScreenData _confirmPopoverScreenData;

        private bool _hasToOpenErrorPopover = true;
        private IEventService _eventService;
        private IUIScreen _errorPopoverScreen = null;
        private IUIScreen _confirmationPopoverScreen = null;

        public override void Setup()
        {
            DontDestroyOnLoad(gameObject);

            ServiceLocator.Shared.Set<IPopoverService>(this);

            _eventService = ServiceLocator.Shared.Get<IEventService>();
            _eventService?.AddListener<ResponseErrorEvent>(HandleResponseErrorEvent, _eventHandles);
            _eventService?.AddListener<RestartGameEvent>(HandleRestartGameEvent, _eventHandles);
            _eventService?.AddListener<ConfirmationEvent>(HandleConfirmationEvent, _eventHandles);
            _eventService?.AddListener<StopMatchmakingEvent>(HandleStopMatchmakingEvent, _eventHandles);
            _eventService?.AddListener<AfterJoinMatchEvent>(HandleAfterJoinMatchEvent, _eventHandles);
        }

        public override void Dispose()
        {
            foreach (EventHandle eventHandle in _eventHandles)
            {
                _eventService?.RemoveListener(eventHandle);
            }

            _eventHandles.Clear();

            ServiceLocator.Shared.Set<IPopoverService>(null);
        }

        public ServiceLocator OwningLocator { get; set; }

        public void OpenItemPopover(string itemId)
        {
            OpenItemPopoverAsync(itemId);
        }

        public void OpenErrorPopover(string message, Action callbackEvent = null)
        {
            OpenErrorPopoverAsync(message, callbackEvent);
        }

        public void OpenConfirmationPopover(string message, Action<bool> onConfirmation)
        {
            OpenConfirmationPopoverAsync(message, onConfirmation);
        }

        private async UniTask OpenItemPopoverAsync(string itemId)
        {
            IUIService uiService = ServiceLocator.Shared.Get<IUIService>();

            if (uiService == null)
            {
                return;
            }

            IUIScreen uIScreen = await uiService.OpenScreenAsync(new ScreenSpec
            {
                Data = _itemPopoverScreenData,
                IsAdditive = true,
                IsPersistent = false,
            });

            _eventService?.Invoke(this, new OpenItemPopoverEvent(itemId));
        }

        private async UniTask OpenErrorPopoverAsync(string errorMessage, Action callBackEvent)
        {
            IUIService uiService = ServiceLocator.Shared.Get<IUIService>();

            if (uiService == null)
            {
                return;
            }

            if (_errorPopoverScreen == null && _hasToOpenErrorPopover)
            {
                _hasToOpenErrorPopover = false;

                _errorPopoverScreen = await uiService.OpenScreenAsync(new ScreenSpec
                {
                    Data = _errorPopoverScreenData,
                    IsAdditive = true,
                    IsPersistent = false,
                });

                _errorPopoverScreen.OnCloseSelf -= HandleErrorScreenClose;
                _errorPopoverScreen.OnCloseSelf += HandleErrorScreenClose;
            }

            _eventService?.Invoke(this, new OpenErrorPopoverEvent(errorMessage, callBackEvent));
        }

        private async UniTask OpenConfirmationPopoverAsync(string message, Action<bool> onConfirmation)
        {
            IUIService uiService = ServiceLocator.Shared.Get<IUIService>();

            if (uiService == null)
            {
                return;
            }

            if (_errorPopoverScreen == null)
            {
                _confirmationPopoverScreen = await uiService.OpenScreenAsync(new ScreenSpec
                {
                    Data = _confirmPopoverScreenData,
                    IsAdditive = true,
                    IsPersistent = false,
                });

                _confirmationPopoverScreen.OnCloseSelf -= HandleConfirmationPopoverScreenClose;
                _confirmationPopoverScreen.OnCloseSelf += HandleConfirmationPopoverScreenClose;
            }

            _eventService?.Invoke(this, new OpenConfirmationPopoverEvent(message, onConfirmation));
        }

        private async UniTask OpenHomeScreenAsync()
        {
            IUIService uiService = ServiceLocator.Shared.Get<IUIService>();

            if (uiService == null)
            {
                return;
            }

            uiService.CloseAllScreens();

            await uiService.OpenScreenAsync(new ScreenSpec
            {
                Data = _homeScreenData,
                IsAdditive = false,
                IsPersistent = false,
            }, true);
        }

        private void HandleResponseErrorEvent(ref EventRef<ResponseErrorEvent> eventRef)
        {
            OpenErrorPopoverAsync(eventRef.Value.ErrorMessage, eventRef.Value.ErrorCallback);
        }

        private void HandleErrorScreenClose(IUIScreen uiScreen)
        {
            _hasToOpenErrorPopover = true;
            _errorPopoverScreen = null;
        }

        private void HandleConfirmationPopoverScreenClose(IUIScreen uiScreen)
        {
            _confirmationPopoverScreen = null;
        }

        private void HandleRestartGameEvent(ref EventRef<RestartGameEvent> eventRef)
        {
            OpenHomeScreenAsync();

            Dispose();
            Setup();
        }

        private void HandleConfirmationEvent(ref EventRef<ConfirmationEvent> eventRef)
        {
            OpenConfirmationPopover(eventRef.Value.Message, eventRef.Value.OnConfirm);
        }

        private void HandleStopMatchmakingEvent(ref EventRef<StopMatchmakingEvent> eventRef)
        {
            OpenHomeScreenAsync();
        }

        private void HandleAfterJoinMatchEvent(ref EventRef<AfterJoinMatchEvent> eventRef)
        {
            if (_confirmationPopoverScreen != null)
            {
                _confirmationPopoverScreen.CloseSelf();
            }
        }*/
    }
}
