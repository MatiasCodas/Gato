using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "Gato/Settings/InputCodeSettings", fileName = "InputCodeSettings")]
    public class InputCodeSettings : ScriptableObject
    {
        [SerializeField]
        private KeyCode _shootWeaponKeyCode;
        [SerializeField]
        private KeyCode _dashKeyCode;

        public KeyCode ShootWeaponKeyCode => _shootWeaponKeyCode;

        public KeyCode DashKeyCode => _dashKeyCode;
    }
}
