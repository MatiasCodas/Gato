using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gato.UI
{
    internal abstract class UIScreen : MonoBehaviour, IUIScreen
    {
        public event Action<IUIScreen> OnOpen;

        public event Action<IUIScreen> OnReturn;

        public event Action<IUIScreen> OnClose;

        public event Action<IUIScreen> OnCloseSelf;

        public event Action<IUIScreen> OnFinishLoading;

        [Header("Canvas")]
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Scene Scene => gameObject.scene;

        public bool UseBackKeyToReturn { get; set; }

        protected GraphicRaycaster GraphicRaycaster => _graphicRaycaster;

        protected CanvasGroup CanvasGroup => _canvasGroup;

        public virtual void Setup()
        {
            SetVisibility(VisibilityState.Interactable);
        }

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void SetVisibility(VisibilityState visibilityState)
        {
            switch (visibilityState)
            {
                case VisibilityState.Interactable:
                {
                    _graphicRaycaster.enabled = true;
                    _canvasGroup.alpha = 1;

                    break;
                }

                case VisibilityState.Visible:
                {
                    _canvasGroup.alpha = 1;
                    _graphicRaycaster.enabled = false;

                    break;
                }

                case VisibilityState.Hidden:
                {
                    _canvasGroup.alpha = 0;
                    _graphicRaycaster.enabled = false;

                    break;
                }
            }
        }

        public virtual void Open()
        {
            OnOpen?.Invoke(this);
        }

        public virtual void Return()
        {
            OnReturn?.Invoke(this);
        }

        public virtual void Close()
        {
            OnClose?.Invoke(this);
        }

        public virtual void CloseSelf()
        {
            OnCloseSelf?.Invoke(this);
        }

        public virtual void FinishLoading()
        {
            OnFinishLoading?.Invoke(this);
        }
    }
}
