using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class DebugObjectSpawner : MonoBehaviour
    {
        public GameObject TotemCurse;
        public GameObject TotemBless;
        public GameObject EnemyBull;
        public GameObject EnemyMosquito;
        public GameObject EnemyHover;
        public GameObject Door;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Instantiate(TotemCurse, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                Instantiate(TotemBless, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
        }
    }
}
