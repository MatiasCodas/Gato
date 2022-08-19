using Cysharp.Threading.Tasks;
using Gato.Core;
using System;
using System.Collections.Generic;

namespace Gato
{
    public interface IUIService : IService
    {
        event Action<IUIScreen> OnScreenChanged;

        int HistorySize { get; }

        IUIScreen ActiveScreen { get; }

        IReadOnlyList<ScreenActivation> History { get; }

        UniTask<IUIScreen> OpenScreenAsync(ScreenSpec spec, bool showLoadingScreen = false);

        UniTask<IUIScreen> OpenScreenAsync(ScreenData screenData, bool showLoadingScreen = false);

        UniTask<IUIScreen> OpenScreenAdditiveAsync(ScreenData screenData);

        void CloseActiveScreen();

        void CloseAllScreens();

        T GetScreen<T>() where T : IUIScreen;
    }
}
