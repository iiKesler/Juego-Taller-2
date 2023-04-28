using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Personajes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JefeFinalController : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rb;
        private TouchingDirection _touchingDirection;
        private Damageable _damageable;
        public Collider2D deathCollider;
        
        public DetectionZone detectionZone;
        
        public float flightSpeed = 2f;
        public float waypointReachedDistance = 0.1f;

        public List<Transform> waypoints;
        
        [FormerlySerializedAs("_waypointNumber")] [SerializeField] 
        private int waypointNumber = 0;
        private Transform _nextWayponit;

        private bool CanMove => _animator.GetBool(AnimationStrings.CanMove);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _damageable = GetComponent<Damageable>();
        }
        
        private void Start()
        {
            _nextWayponit = waypoints[waypointNumber];
        }

        private void OnEnable()
        {
            _damageable.damageableDeath.AddListener(OnDeath);
        }

        private void FixedUpdate()
        {
            if (_damageable.IsAlive)
            {
                if (CanMove)
                {
                    Flight();
                }
                else
                {
                    _rb.velocity = Vector3.zero;
                }
            }
        }

        private void Flight()
        {
            // Move towards next waypoint
            var position = transform.position;
            var position1 = _nextWayponit.position;
            Vector2 directionToWaypoint = (position1 - position).normalized;

            // Check if we reached the waypoint
            var distance = Vector2.Distance(position1, position);
            
            _rb.velocity = directionToWaypoint * flightSpeed;
            UpdateDirection();

            // Check if we need to change waypoints
            if (distance <= waypointReachedDistance)
            {
                // Swticth to next waypoint
                waypointNumber++;
                
                if (waypointNumber >= waypoints.Count)
                {
                    // Loop back to original waypoint
                    waypointNumber = 0;
                }
                
                _nextWayponit = waypoints[waypointNumber];
            }
        }

        private void UpdateDirection()
        {
            var localScale = transform.localScale;
            
            if (transform.localScale.x > 0)
            {
                // Facing right
                if (_rb.velocity.x < 0)
                {
                    // Flip
                    transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
                }
            }
            else
            {
                // Facing left
                if (_rb.velocity.x > 0)
                {
                    // Flip
                    transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
                }
            }
        }

        public void OnDeath()
        {
            // Falls to ground when dead
            _rb.gravityScale = 2f;
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            deathCollider.enabled = true;
        }
    }
}
