using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Backend
{
    public class WorldInteractable : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private string _id;
        [ContextMenu("Generate guid for ID")]
        private void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
        }


        public bool Interacted;

        public void LoadData(GameData data)
        {
            data.WorldInteraction.TryGetValue(_id, out Interacted);
        }

        public void SaveData(ref GameData data)
        {
            if (data.WorldInteraction.ContainsKey(_id))
            {
                data.WorldInteraction.Remove(_id);
            }
            data.WorldInteraction.Add(_id, Interacted);
        }
    }
}
