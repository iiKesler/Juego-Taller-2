using UnityEngine;

namespace Personajes
{
    public class ProjectileLauncher : MonoBehaviour
    {
        public Transform launchPoint;
        public GameObject projectilePrefab;

        public void FireProjectile()
        {
            var projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
            var originalScale = projectile.transform.localScale;

            // Flip the projectile if the character is flipped
            projectile.transform.localScale = new Vector3(
                originalScale.x * transform.localScale.x > 0 ? 1 : -1,
                originalScale.y,
                originalScale.z
                );
        }
    }
}
