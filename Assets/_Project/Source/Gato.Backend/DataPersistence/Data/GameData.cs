using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Backend
{
    [System.Serializable]
    public class GameData
    {

        public Vector3 PlayerPos;
        public SerializableDictionary<string, bool> WorldInteraction;
        public string Scene;

        public GameData ()
        {
            PlayerPos = Vector3.zero;
            WorldInteraction = new SerializableDictionary<string, bool>();
            Scene = "TestingLevel";
        }
    }
}
