using System;
using UnityEngine.SceneManagement;

namespace Gato
{
    public interface IUIScreen
    {
        event Action<IUIScreen> OnOpen;

        event Action<IUIScreen> OnReturn;

        event Action<IUIScreen> OnClose;

        event Action<IUIScreen> OnCloseSelf;

        Scene Scene { get; }

        bool UseBackKeyToReturn { get; set; }

        void Setup();

        void Tick(float deltaTime);

        void Dispose();

        void SetVisibility(VisibilityState value);

        void Open();

        void Return();

        void Close();

        void CloseSelf();

        void FinishLoading();
    }
}
