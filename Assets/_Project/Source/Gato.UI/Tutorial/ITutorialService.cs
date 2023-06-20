using Gato.Core;

namespace Gato.UI
{
    public interface ITutorialService : IService
    {
        void SetTutorialData(TutorialData data);
    }
}
