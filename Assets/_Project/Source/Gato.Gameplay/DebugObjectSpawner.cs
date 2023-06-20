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
                Instantiate(TotemCurse, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Instantiate(TotemBless, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Instantiate(EnemyBull, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Instantiate(EnemyMosquito, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Instantiate(EnemyHover, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Instantiate(Door, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
        }
    }
}
