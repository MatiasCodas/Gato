using UnityEngine;

namespace Gato
{
    [CreateAssetMenu(menuName = "Gato/UI/Screen Data", fileName = "ScreenData")]
    public sealed class ScreenData : ScriptableObject
    {
        [SerializeField] private string _sceneName;

        public string SceneName => _sceneName;
    }
}
