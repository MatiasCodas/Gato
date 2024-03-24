using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class DoorTotem : MonoBehaviour
    {
        private void Bless()
        {
            Debug.Log("Bless");
            transform.parent.SendMessage("Bless");
            
        }
    }
}
