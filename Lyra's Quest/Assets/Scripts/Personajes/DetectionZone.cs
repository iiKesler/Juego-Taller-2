using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Personajes
{
    public class DetectionZone : MonoBehaviour
    {
        [FormerlySerializedAs("NoCollidersRemain")] 
        public UnityEvent noCollidersRemain;

        public List<Collider2D> dectectedColliders = new List<Collider2D>();
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            dectectedColliders.Add(collision);
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            dectectedColliders.Remove(collision);

            if (dectectedColliders.Count <= 0)
            {
                noCollidersRemain.Invoke();
            }
        }
    }
}
