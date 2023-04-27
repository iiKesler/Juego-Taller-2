using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Menu
{
    public class TextSlider : MonoBehaviour
    {
        public TextMeshProUGUI numberText;
        private Slider _slider;

        public void Start()
        {
            _slider = GetComponent<Slider>();
            SetNumberText(_slider.value);
        }

        public void SetNumberText(float value)
        {
            numberText.text = value.ToString();
        }
    }
}
