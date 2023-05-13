using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Personajes
{
    public class Damageable : MonoBehaviour
    {
        public UnityEvent<int, Vector2> damageableHit;
        public UnityEvent damageableDeath;
        public UnityEvent<int, int> healthChanged;

        private Animator _animator;
        
        [FormerlySerializedAs("_maxHealth")] [SerializeField]
        public int maxHealth = 100;

        public int MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        [FormerlySerializedAs("_health")] [SerializeField]
        private int health = 100;

        public int Health
        {
            get => health;
            private set
            {
                health = value;
                healthChanged?.Invoke(health, MaxHealth);

                if(health <= 0)
                {
                    IsAlive = false;
                }
            }
        }
        
        [FormerlySerializedAs("_isAlive")] [SerializeField]
        private bool isAlive = true;

        public bool IsAlive
        {
            get => isAlive;
            set
            {
                isAlive = value;
                if (_animator != null)
                {
                    _animator.SetBool(AnimationStrings.IsAlive, value);
                }
                if(value == false)
                {
                    damageableDeath?.Invoke();
                }
            }
        }

        [FormerlySerializedAs("_isInvincible")] [SerializeField]
        private bool isInvincible = false;
        private float _timeSinceHit = 0;
        public float invincibilityTime = 0.25f;
        

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (isInvincible)
            {
                if(_timeSinceHit > invincibilityTime)
                {
                    isInvincible = false;
                    _timeSinceHit = 0;
                }
                
                _timeSinceHit += Time.deltaTime;
            }
        }

        public bool Hit(int damage, Vector2 knockback)
        {
            if (!IsAlive || isInvincible) return false;
            Health -= damage;
            isInvincible = true;
            _animator.SetTrigger(AnimationStrings.HitTrigger);
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            
            return true;
        }
        
        public bool Heal(int healthRestore)
        {
            if (isAlive && Health < MaxHealth)
            {
                var maxHeal = Mathf.Max(MaxHealth - Health, 0);
                var actualHeal = Mathf.Min(maxHeal, healthRestore);
                Health += actualHeal;
                CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);

                return true;
            }

            return false;
        }
    }
}
