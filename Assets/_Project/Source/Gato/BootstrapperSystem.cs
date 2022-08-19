using UnityEngine;

namespace Gato
{
    internal sealed class BootstrapperSystem : MonoSystem
    {
        [SerializeField]
        private ScreenData _firstScreenData;

        public override async void LateSetup()
        {
            /*IUIService uiService = ServiceLocator.Shared.Get<IUIService>();

            if (uiService != null)
            {
                await uiService.OpenScreenAsync(_firstScreenData, true, LoadingType.Splash);
            }*/
        }
    }
}
