using UnityEngine;

namespace Gato
{
    [DisallowMultipleComponent]
    public abstract class Manager : MonoBehaviour
    {
        public virtual void Setup()
        {
        }

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
