using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina
{
    public const int MAX_STAMINA = 100;
    private int _currentStamina;

    public int CurrentStamina { get => _currentStamina; }

    public PlayerStamina (int currentStamina)
    {
        _currentStamina = currentStamina;
    }
    public PlayerStamina ()
    {
        _currentStamina = MAX_STAMINA;
    }

    public void DecreaseStamina(int amount)
    {
        if ( _currentStamina <= 0) return;

        int value;
        value = Mathf.Max(_currentStamina - amount, 0);

        _currentStamina = value;
    }

    public void IncreaseStamina(int amount)
    {
        if (_currentStamina >= MAX_STAMINA) return;

        int value;
        value = Mathf.Min(_currentStamina + amount, MAX_STAMINA);

        _currentStamina = value;
    }

}
