using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats 
{
    private float _maxPlayerHealth = 100;
    private float _currentPlayerHealth;
    private int _AKClipSize = 30;
    private int _AKClipCount;
    private int _makarovClipSize = 8;
    private int _makarovClipCount;

    public float CurrentPlayerHealth => _currentPlayerHealth;
    public float MaxPlayerHealth => _maxPlayerHealth;
    public int AKClipSize => _AKClipSize;
    public int AKClipCount => _AKClipCount;
    public int MakarovClipSize => _makarovClipSize;
    public int MakarovClipCount => _makarovClipCount;
    public PlayerStats()
    {
        _currentPlayerHealth = _maxPlayerHealth;
        _AKClipCount = _AKClipSize;
        _makarovClipCount = _makarovClipSize;
    }
    public void ChangeHealth(float amount)
    {
        _currentPlayerHealth = Mathf.Clamp(_currentPlayerHealth + amount, 0, _maxPlayerHealth);
        if (_currentPlayerHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void ChangeAKCount(int amount)
    {
        _AKClipCount = Mathf.Clamp(_AKClipCount + amount, 0, _AKClipSize);
    }
    public void ChangeMakarovCount(int amount)
    {
        _makarovClipCount = Mathf.Clamp(_makarovClipCount + amount, 0, _makarovClipSize);
    }

}
