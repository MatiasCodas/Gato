using Gato.Core;
using UnityEngine;

namespace Gato.Gameplay
{
    public class RangeWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform spawnPosition;
        public Animator animController;
        public ObjectPool pool;
        public string targetTag;
        [SerializeField] private bool isPlayer;

        public FloatVariable currentAmmo;
        public float initialAmmo = 10;

        private void Start()
        {
            if (isPlayer)
            {
                currentAmmo.value = initialAmmo;
            }
        }

        public void Shoot()
        {
            // if (GameManager.isPaused) return;

            if (isPlayer && currentAmmo.value == 0) return;

            if (pool.objPool.Count == 0)
            {
                CreateNewProjectile();
            }
            else
            {
                UseFromPool();
            }

            if (currentAmmo != null) currentAmmo.value--;
        }

        private void CreateNewProjectile()
        {
            GameObject instance = Instantiate(projectile, spawnPosition.position, spawnPosition.rotation, pool.transform);
            Projectile projectileScript = instance.GetComponent<Projectile>();

            projectileScript.pool = pool;
            projectileScript.targetTag = targetTag;

            if (isPlayer) projectileScript.rb.useGravity = true;
            else projectileScript.rb.useGravity = false;
        }

        private void UseFromPool()
        {
            GameObject instance = pool.objPool[0];
            pool.objPool.Remove(instance);

            instance.transform.position = spawnPosition.transform.position;
            instance.transform.rotation = spawnPosition.transform.rotation;

            Projectile projectileScript = instance.GetComponent<Projectile>();
            projectileScript.targetTag = targetTag;

            instance.SetActive(true);

            if (isPlayer) { projectileScript.rb.useGravity = true; }
            else { projectileScript.rb.useGravity = false; }

            projectileScript.ShootProjectile();
        }
    }
}
