using Cysharp.Threading.Tasks;
using Gato.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gato.UI
{
    internal sealed class UISystem : MonoSystem, IUIService
    {
        public event Action<IUIScreen> OnScreenChanged;

        private readonly List<ScreenActivation> _history = new List<ScreenActivation>();

        [SerializeField]
        private ScreenData _loadingScreenData;

        private ILoadingService _loadingService;
        private ScreenActivation _activeScreen;

        public int HistorySize => _history.Count;

        public IUIScreen ActiveScreen => _activeScreen.Screen;

        public IReadOnlyList<ScreenActivation> History => _history;

        public ServiceLocator OwningLocator { get; set; }

        public override void Setup()
        {
            DontDestroyOnLoad(gameObject);
            ServiceLocator.Shared.Set<IUIService>(this);

            //_loadingService = ServiceLocator.Shared.Get<ILoadingService>();
        }

        public override void Tick(float deltaTime)
        {
            if (_activeScreen.IsValid)
            {
                _activeScreen.Screen.Tick(deltaTime);
            }
        }

        public override void Dispose()
        {
        }

        public async UniTask<IUIScreen> OpenScreenAsync(ScreenSpec spec, bool showLoadingScreen = false)
        {
            ScreenActivation previousActiveScreen = _activeScreen;

            if (previousActiveScreen.IsValid)
            {
                previousActiveScreen.Screen.SetVisibility(VisibilityState.Visible);
            }

            IUIScreen screen = await OpenAsync(spec);

            if (previousActiveScreen.IsValid && !spec.IsAdditive)
            {
                previousActiveScreen.Screen.SetVisibility(VisibilityState.Hidden);
            }

            OnScreenChanged?.Invoke(screen);

            return screen;
        }

        public async UniTask<IUIScreen> OpenScreenAsync(ScreenData screenData, bool showLoadingScreen = false)
        {
            return await OpenScreenAsync(new ScreenSpec
            {
                Data = screenData,
                IsAdditive = false,
                IsPersistent = false,
            }, showLoadingScreen);
        }

        public async UniTask<IUIScreen> OpenScreenAdditiveAsync(ScreenData screenData)
        {
            return await OpenScreenAsync(new ScreenSpec
            {
                Data = screenData,
                IsAdditive = true,
                IsPersistent = false,
            }, false);
        }

        public void CloseActiveScreen()
        {
            if (_activeScreen.IsValid)
            {
                _activeScreen.Screen.Close();
            }
        }

        public void CloseAllScreens()
        {
            for (int i = _history.Count - 1; i >= 0; i--)
            {
                ScreenActivation screen = _history[i];

                RemoveFromHistory(screen.Screen);

                DisposeScreen(screen.Screen);
            }
        }

        public T GetScreen<T>() where T : IUIScreen
        {
            for (int i = 0; i < _history.Count; i++)
            {
                IUIScreen screen = _history[i].Screen;

                if (screen is T casted)
                {
                    return casted;
                }
            }

            return default;
        }

        private async UniTask<IUIScreen> OpenAsync(ScreenSpec spec)
        {
            if (!spec.IsValid)
            {
                Debug.LogError("Tried to open an invalid screen!");

                return null;
            }

            AsyncOperation operation = SceneManager.LoadSceneAsync(spec.Data.SceneName, LoadSceneMode.Additive);

            await operation;
            await UniTask.Yield();

            Scene scene = SceneManager.GetSceneByName(spec.Data.SceneName);

            if (!scene.TryFindRootObjectOfType(out IUIScreen uiScreen))
            {
                throw new ArgumentException($"No '{nameof(IUIScreen)}' components found in scene '{scene.name}'");
            }

            SetupScreen(spec, uiScreen);
            uiScreen.Open();

            return uiScreen;
        }

        private void SetupScreen(ScreenSpec spec, IUIScreen uiScreen)
        {
            ScreenActivation screenActivation = new ScreenActivation
            {
                Screen = uiScreen,
                Spec = spec,
            };

            if (!_history.Contains(screenActivation))
            {
                _history.Add(screenActivation);
            }

            uiScreen.Setup();
            uiScreen.OnClose += HandleScreenClose;
            uiScreen.OnReturn += HandleScreenReturn;
            uiScreen.OnCloseSelf += HandleScreenCloseSelf;

            SetActiveScreen(screenActivation);
        }

        private void DisposeScreen(IUIScreen screen)
        {
            if (screen.IsUnityNull())
            {
                Debug.LogError($"Tried to close null screen");

                return;
            }

            screen.OnClose -= HandleScreenClose;
            screen.OnReturn -= HandleScreenReturn;
            screen.OnCloseSelf -= HandleScreenCloseSelf;
            screen.Dispose();

            SceneManager.UnloadSceneAsync(screen.Scene);
        }

        private void SetActiveScreen(ScreenActivation uiScreen)
        {
            _activeScreen = uiScreen;
        }

        private void RefreshActiveScreen()
        {
            SetActiveScreen(_history.LastOrDefault());

            if (_activeScreen.IsValid)
            {
                _activeScreen.Screen.SetVisibility(VisibilityState.Interactable);
            }
        }

        private void RemoveFromHistory(IUIScreen screen)
        {
            for (int i = 0; i < _history.Count; i++)
            {
                ScreenActivation item = _history[i];

                if (item.Screen == screen)
                {
                    _history.RemoveAt(i);

                    break;
                }
            }
        }

        private void HandleScreenReturn(IUIScreen uiScreen)
        {
            if (uiScreen != _activeScreen.Screen)
            {
                Debug.LogError($"Screen '{uiScreen}' has called '{nameof(IUIScreen.OnReturn)}', but it's not the active screen");

                return;
            }

            RemoveFromHistory(uiScreen);
            DisposeScreen(uiScreen);
            RefreshActiveScreen();
        }

        private void HandleScreenClose(IUIScreen uiScreen)
        {
            if (uiScreen != _activeScreen.Screen)
            {
                Debug.LogError($"Screen '{uiScreen}' has called '{nameof(IUIScreen.OnClose)}', but it's not the active screen");

                return;
            }

            for (int i = _history.Count - 1; i >= 0; i--)
            {
                ScreenActivation screenActivation = _history[i];
                IUIScreen screen = screenActivation.Screen;

                if (screen != uiScreen && screenActivation.Spec.IsPersistent)
                {
                    continue;
                }

                DisposeScreen(screen);

                _history.RemoveAt(i);
            }

            RefreshActiveScreen();
        }

        private void HandleScreenCloseSelf(IUIScreen uiScreen)
        {
            bool wasActiveScreen = uiScreen == _activeScreen.Screen;

            RemoveFromHistory(uiScreen);
            DisposeScreen(uiScreen);

            if (wasActiveScreen)
            {
                RefreshActiveScreen();
            }
        }

        void IDisposable.Dispose()
        {
        }
    }
}
