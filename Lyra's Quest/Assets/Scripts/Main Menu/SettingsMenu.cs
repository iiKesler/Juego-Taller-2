using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace Main_Menu
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
        private Resolution[] _resolutions;

        public TMP_Dropdown resolutionDropdowns;

        private void Start()
        {
            _resolutions = Screen.resolutions;
            resolutionDropdowns.ClearOptions();

            var options = new List<string>();
            
            var currentResolutionIndex = 0;
            for (var i = 0; i < _resolutions.Length; i++)
            {
                var option = _resolutions[i].width + " x " + _resolutions[i].height + " @ " + _resolutions[i].refreshRate + "hz";
                options.Add(option);

                if (_resolutions[i].Equals(Screen.currentResolution))
                {
                    currentResolutionIndex = i;
                }
            }
            
            resolutionDropdowns.AddOptions(options);
            resolutionDropdowns.value = currentResolutionIndex;
            resolutionDropdowns.RefreshShownValue();
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
    }
}
