using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina
{
    public const int MAX_STAMINA = 100;
    public const int MIN_STAMINA = 0;
    private float _currentStamina;

    public float CurrentStamina { get => _currentStamina; }

    public PlayerStamina (int currentStamina)
    {
        if (currentStamina > MAX_STAMINA)
        {
            _currentStamina = MAX_STAMINA;
        }
        else if (currentStamina < 0)
        {
            _currentStamina = MIN_STAMINA;
        }
        else
        {
            _currentStamina = currentStamina;
        }
        
    }
    public PlayerStamina ()
    {
        _currentStamina = MIN_STAMINA;
    }

    public void DecreaseStamina(float amount)
    {
        if ( _currentStamina <= 0) return;

        float value;
        value = Mathf.Max(_currentStamina - amount, MIN_STAMINA);

        _currentStamina = value;
    }

    public void IncreaseStamina(float amount)
    {
        if (_currentStamina >= MAX_STAMINA) return;

        float value;
        value = Mathf.Min(_currentStamina + amount, MAX_STAMINA);

        _currentStamina = value;
    }

}
