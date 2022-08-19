using UnityEngine;

namespace Gato
{
    public static class FindComponentsExtensions
    {
        public static T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null)
            {
                return null;
            }

            T component = go.GetComponent<T>();

            if (component != null)
            {
                return component;
            }

            Transform transformObject = go.transform.parent;

            while (transformObject != null && component == null)
            {
                component = transformObject.gameObject.GetComponent<T>();
                transformObject = transformObject.parent;
            }

            return component;
        }
    }
}
