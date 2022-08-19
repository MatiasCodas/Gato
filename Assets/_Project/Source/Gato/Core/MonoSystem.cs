using UnityEngine;

namespace Gato
{
    [DisallowMultipleComponent]
    public abstract class MonoSystem : MonoBehaviour
    {
        public virtual void Setup()
        {
        }

        public virtual void LateSetup()
        {
        }

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void Dispose()
        {
        }

        private void Awake()
        {
            Setup();
        }

        private void Start()
        {
            LateSetup();
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
