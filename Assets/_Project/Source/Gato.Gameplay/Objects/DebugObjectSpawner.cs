using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gato.Gameplay
{
    public class DebugObjectSpawner : MonoBehaviour
    {

        [SerializeField]
        private InputActionReference _1, _2, _3, _4, _5, _6, _7;

        public GameObject TotemCurse;
        public GameObject TotemBless;
        public GameObject EnemyBull;
        public GameObject EnemyMosquito;
        public GameObject EnemyHover;
        public GameObject Door;

        private void Update()
        {
            if(_1.action.IsPressed())
            {
                Instantiate(TotemCurse, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (_2.action.IsPressed())
            {
                Instantiate(TotemBless, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (_3.action.IsPressed())
            {
                Instantiate(EnemyBull, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (_4.action.IsPressed())
            {
                Instantiate(EnemyMosquito, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (_5.action.IsPressed())
            {
                Instantiate(EnemyHover, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
            if (_6.action.IsPressed())
            {
                Instantiate(Door, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, null);
            }
        }
    }
}
