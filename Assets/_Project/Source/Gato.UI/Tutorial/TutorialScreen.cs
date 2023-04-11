using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gato.UI
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _okButton;
        [SerializeField]
        private TMP_Text _tutorialText;

        private void Awake()
        {
            _okButton.onClick.AddListener(HandleOkButtonPressed);
        }

        public void SetTutorial(TutorialData data)
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            _tutorialText.text = data.TutorialText;
        }

        private void HandleOkButtonPressed()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
