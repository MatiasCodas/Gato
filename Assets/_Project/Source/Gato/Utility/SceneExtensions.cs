using System.Collections.Generic;
using UnityEngine;

namespace Gato
{
    public static class SceneExtensions
    {
        private static readonly List<GameObject> ObjectList = new List<GameObject>(6);

        public static bool TryFindRootObjectOfType<T>(this UnityEngine.SceneManagement.Scene scene, out T obj)
        {
            ObjectList.Clear();

            scene.GetRootGameObjects(ObjectList);

            for (int i = 0; i < ObjectList.Count; i++)
            {
                GameObject gameObject = ObjectList[i];

                if (gameObject.TryGetComponent(out obj))
                {
                    return true;
                }
            }

            obj = default;

            return false;
        }
    }
}
