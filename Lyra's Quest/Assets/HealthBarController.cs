using System;
using Personajes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Damageable _playerDamageable;
    
    public TMP_Text healthBarText;
    public Slider healthBarSlider;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("Player not found, make sure there is a player in the scene with the tag 'Player'.");
        }
        
        _playerDamageable = player.GetComponent<Damageable>();
    }

    private void Start()
    {
        healthBarSlider.value = CalculateSliderPercentange(_playerDamageable.Health, _playerDamageable.MaxHealth);
        healthBarText.text = "Vida " + _playerDamageable.Health + " / " + _playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        _playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    
    private void OnDisable()
    {
        _playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private static float CalculateSliderPercentange(int currentHealth, int maxHealth)
    {
        return (float) currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthBarSlider.value = CalculateSliderPercentange(newHealth, maxHealth);
        healthBarText.text = "Vida " + newHealth + " / " + maxHealth;
    }
}
