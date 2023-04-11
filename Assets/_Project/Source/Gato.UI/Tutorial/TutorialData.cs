using UnityEngine;

namespace Gato.UI
{
    [CreateAssetMenu(menuName = "Create TutorialData", fileName = "TutorialData")]
    public class TutorialData : ScriptableObject
    {
        [SerializeField]
        private string _tutorialText;

        public string TutorialText => _tutorialText;
    }
}
