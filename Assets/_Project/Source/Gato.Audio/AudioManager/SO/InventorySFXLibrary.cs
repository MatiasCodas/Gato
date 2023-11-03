using UnityEngine;

namespace Gato.Audio
{
    [CreateAssetMenu(fileName = "InventorySFXLibrary", menuName = "ScriptableObjects/Audio/InventorySFXLibrary")]
    public class InventorySFXLibrary : ScriptableObject
    {
        public AudioClip NewItemSFX;
    }
}
