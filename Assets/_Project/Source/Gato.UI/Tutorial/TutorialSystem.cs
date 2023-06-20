using Gato.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.UI
{
    public class TutorialSystem : MonoSystem, ITutorialService
    {
        [SerializeField]
        private TutorialData _initialData;
        [SerializeField]
        private TutorialScreen _tutorialScreen;

        public ServiceLocator OwningLocator { get; set; }

        public override void Setup()
        {
            ServiceLocator.Shared.Set<ITutorialService>(this);
            SetTutorialData(_initialData);
        }

        public void SetTutorialData(TutorialData data)
        {
            _tutorialScreen.SetTutorial(data);
        }
    }
}
