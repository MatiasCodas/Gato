using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Backend
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);
        void SaveData(ref GameData data);
    }
}
