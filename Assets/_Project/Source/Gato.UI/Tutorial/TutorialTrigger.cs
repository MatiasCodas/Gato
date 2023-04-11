using Gato.Core;
using UnityEngine;

namespace Gato.UI
{
    public class TutorialTrigger : MonoBehaviour
    {
        [SerializeField]
        private TutorialData _data;

        private ITutorialService _tutorialService;

        private void Start()
        {
            _tutorialService = ServiceLocator.Shared.Get<ITutorialService>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
            {
                return;
            }

            _tutorialService.SetTutorialData(_data);
        }
    }
}
