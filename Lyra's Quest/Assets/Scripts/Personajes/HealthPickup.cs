using UnityEngine;

namespace Personajes
{
    public class HealthPickup : MonoBehaviour
    {
        public int healthRestore = 20;
        public Vector3 spinRotationSpeed = new(0, 180, 0);
        
        private void Update()
        {
            transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var damageable = collision.GetComponent<Damageable>();

            if (damageable)
            {
                var wasHealed = damageable.Heal(healthRestore);

                if (wasHealed)
                    Destroy(gameObject);
                
            }
        }
    }
}
