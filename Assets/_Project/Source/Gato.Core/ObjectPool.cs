using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Core
{
    public class ObjectPool : MonoBehaviour
    {
        public List<GameObject> objPool;

        public void ReturnToPool(GameObject _obj)
        {
            objPool.Add(_obj);
        }
    }
}
