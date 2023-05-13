using System;
using UnityEngine;

namespace Personajes
{
    public class Attack : MonoBehaviour
    {
        public int attackDamage = 10;
        public Vector2 knockback = Vector2.zero;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var damageable = collision.GetComponent<Damageable>();

            if (damageable == null) return;
            var deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            var gotHit = damageable.Hit(attackDamage, deliveredKnockback);
                
            if(gotHit)
                Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }
}
