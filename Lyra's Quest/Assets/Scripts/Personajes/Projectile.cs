using UnityEngine;
using UnityEngine.Serialization;

namespace Personajes
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rb;
        
        [FormerlySerializedAs("attackDamage")] 
        public int damage = 10;
        public Vector2 moveSpeed = new(3f,0);
        public Vector2 knockback = new(0,0);

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                var deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

                var gotHit = damageable.Hit(damage, deliveredKnockback);
                
                if(gotHit)
                {
                    Debug.Log(collision.name + " hit for " + damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
