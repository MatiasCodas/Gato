using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gato.UI
{
    internal sealed class LoadingScreen : UIScreen//, ILoadingService
    {
        /*private const float FadeInvokeTime = 1f;
        private const float FadeDuration = 0.5f;
        private const int SortingOrderOverlay = 100;
        private const int SortingOrderWorldCamera = 100;

        private readonly List<EventHandle> _eventHandles = new List<EventHandle>();

        [SerializeField]
        private Canvas _rootCanvas;
        [SerializeField]
        private GameObject _splashContainer;
        [SerializeField]
        private GameObject _matchMakingContainer;
        [SerializeField]
        private GameObject _battleContainer;
        [SerializeField]
        private Slider _progressBar;

        private bool _isLoading;
        private LoadingType _currentLoadingType;
        private AsyncOperation _loadingOperation;
        private IEventService _eventService;

        public bool IsLoading => _isLoading;

        public ServiceLocator OwningLocator { get; set; }

        public override void Setup()
        {
            ServiceLocator.Shared.Set<ILoadingService>(this);

            _eventService = ServiceLocator.Shared.Get<IEventService>();
            _eventService?.AddListener<FinishedLoadingEvent>(HandleFinishLoadingEvent, _eventHandles);
        }

        public void SetLoadingOperation(AsyncOperation loadingOperation)
        {
            _loadingOperation = loadingOperation;
            UpdateProgress();
        }

        public override void Close()
        {
            CanvasGroup.DOFade(0f, FadeDuration).OnComplete(()=>
            {
                CloseSelf();
                _isLoading = false;
                Dispose();
            });
        }

        public override void Dispose()
        {
            ServiceLocator.Shared.Set<ILoadingService>(null);
            _loadingOperation = null;

            foreach (EventHandle eventHandle in _eventHandles)
            {
                _eventService.RemoveListener(eventHandle);
            }

            _eventHandles.Clear();
        }

        public void SetLoading(LoadingType loadingType)
        {
            _currentLoadingType = loadingType;

            switch (loadingType)
            {
                case LoadingType.Splash:
                {
                    _battleContainer.SetActive(false);
                    _matchMakingContainer.SetActive(false);
                    _splashContainer.SetActive(true);

                    _rootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

                    break;
                }

                case LoadingType.MatchMaking:
                {
                    _battleContainer.SetActive(false);
                    _matchMakingContainer.SetActive(true);
                    _splashContainer.SetActive(false);

                    MainCamera.Instance.SetupCanvas(_rootCanvas);
                    _rootCanvas.sortingOrder = SortingOrderOverlay;

                    break;
                }

                case LoadingType.Battle:
                {
                    _battleContainer.SetActive(true);
                    _matchMakingContainer.SetActive(false);
                    _splashContainer.SetActive(false);

                    MainCamera.Instance.SetupCanvas(_rootCanvas);
                    _rootCanvas.sortingOrder = SortingOrderOverlay;

                    break;
                }
            }

            SetVisibility(VisibilityState.Interactable);
            _isLoading = true;
        }

        private async UniTask FinishLoadingEvent(bool needSession)
        {
            ISession session = ServiceLocator.Shared.Get<IAuthenticationService>().FetchUserSession();
            
            if (needSession && session != null)
            {
                await UniTask.WaitUntil(() => session.IsRefreshExpired == false);
            }

            Invoke(nameof(Close), FadeInvokeTime);
        }

        private async void UpdateProgress()
        {
            if (_currentLoadingType != LoadingType.Splash)
            {
                return;
            }

            while (!_loadingOperation.isDone)
            {
                _progressBar.value = _loadingOperation.progress;
                await UniTask.Yield();
            }
        }

        private void HandleFinishLoadingEvent(ref EventRef<FinishedLoadingEvent> eventRef)
        {
            FinishLoadingEvent(eventRef.Value.NeedSession).Forget();
        }*/
    }
}
