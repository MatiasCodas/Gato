using UnityEngine;

namespace Gato.Rendering
{
    [RequireComponent(typeof(Camera))]
    public sealed class MainCamera : MonoBehaviour
    {
        private static MainCamera _instance;

        [SerializeField]
        private Camera _camera;

        public static MainCamera Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<MainCamera>();
                }

                return _instance;
            }
        }

        public void SetupCanvas(Canvas canvas)
        {
            canvas.worldCamera = _camera;
        }

        public Vector3 WorldToScreenPoint(Vector3 position)
        {
            return _camera.WorldToScreenPoint(position);
        }
    }
}
