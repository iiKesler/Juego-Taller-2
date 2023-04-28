using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Menu
{
    public class TextSlider : MonoBehaviour
    {
        private Slider _slider;

        public void Start()
        {
            _slider = GetComponent<Slider>();
        }
    }
}
