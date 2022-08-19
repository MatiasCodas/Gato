using UnityEngine;

namespace Gato
{
    public sealed class DisableOnBuild : MonoBehaviour
    {
        [SerializeField] private Behaviour[] _targets;

        private void Awake()
        {
            if (!Application.isEditor)
            {
                foreach (Behaviour target in _targets)
                {
                    target.enabled = false;
                }
            }
        }
    }
}
