using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "Gato/Settings/InputCodeSettings", fileName = "InputCodeSettings")]
    public class InputCodeSettings : ScriptableObject
    {
        [SerializeField]
        private KeyCode _shootWeaponKeyCode;

        public KeyCode ShootWeaponKeyCode => _shootWeaponKeyCode;
    }
}
