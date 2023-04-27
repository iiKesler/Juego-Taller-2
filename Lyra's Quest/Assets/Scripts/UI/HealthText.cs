using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace UI
{
    public class HealthText : MonoBehaviour
    {
        private RectTransform _textTransform;
        private TextMeshProUGUI _textMeshPro;
        
        public Vector3 moveSpeed = new(0, 75, 0);
        public float timeToFade = 1f;
        
        private float _timeElapsed;
        private Color _startColor;
        
        private void Awake()
        {
            _textTransform = GetComponent<RectTransform>();
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _startColor = _textMeshPro.color;
        }

        private void Update()
        {
            _textTransform.position += moveSpeed * Time.deltaTime;
            
            _timeElapsed += Time.deltaTime;
            
            if(_timeElapsed < timeToFade)
            {
                var fadeAlpha = _startColor.a * (1 - _timeElapsed / timeToFade);
                _textMeshPro.color = new Color(_startColor.r, _startColor.g, _startColor.b, fadeAlpha);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
