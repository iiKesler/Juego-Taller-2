using System;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject damageTextPrefab;
        public GameObject healthTextPrefab;
        
        public Canvas gameCanvas;
        
        private void Awake()
        {
            gameCanvas = FindObjectOfType<Canvas>();
        }

        private void OnEnable()
        {
            CharacterEvents.characterDamaged += CharacterTookDamage;
            CharacterEvents.characterHealed += CharacterHealed;
        }

        private void OnDisable()
        {
            CharacterEvents.characterDamaged -= CharacterTookDamage;
            CharacterEvents.characterHealed -= CharacterHealed;
        }

        public void CharacterTookDamage(GameObject character, int damageReceived)
        {
            // Create a new damage text
            var spawnPosition = Camera.main!.WorldToScreenPoint(character.transform.position);
            
            var tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
            
            tmpText.text = damageReceived.ToString();
        }
        
        public void CharacterHealed(GameObject character, int healthRestored)
        {
            // Character healed
            var spawnPosition = Camera.main!.WorldToScreenPoint(character.transform.position);
            
            var tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
            
            tmpText.text = healthRestored.ToString();
        }

        public void OnPauseGame(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                    Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod()!.Name);
                #endif
                
                #if (UNITY_EDITOR)
                    UnityEditor.EditorApplication.isPlaying = false;
                #elif (UNITY_STANDALONE)
                    Application.Quit();
                #elif (UNITY_WEBGL)
                    SceneManager.LoadScene("MainMenu");
                #endif
            }
        }
    }
}
