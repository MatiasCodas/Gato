using UnityEngine;

namespace Gato.Gameplay
{
    [CreateAssetMenu(menuName = "Gato/Settings/InputCodeSettings", fileName = "InputCodeSettings")]
    public class InputCodeSettings : ScriptableObject
    {
        [SerializeField]
        private KeyCode[] _shootWeaponKeyCode;
        [SerializeField]
        private KeyCode[] _recoverWeaponKeyCode;
        [SerializeField]
        private KeyCode[] _dashKeyCode;

        
  

        public KeyCode ShootWeaponKeyCode => _shootWeaponKeyCode[0];

        public KeyCode DashKeyCode => _dashKeyCode[0];

        public KeyCode RecoverWeaponKeyCode => _recoverWeaponKeyCode[0];


        //for controllers
        public KeyCode ShootWeaponKeyCodeGamepad => _shootWeaponKeyCode[1];

        public KeyCode DashKeyCodeGamepad => _dashKeyCode[1];

        public KeyCode RecoverWeaponKeyCodeGamepad => _recoverWeaponKeyCode[1];

    }
}
