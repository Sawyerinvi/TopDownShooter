using System;
using UnityEngine;
using Zenject;


public class PlayerFacade 
{
    private readonly PlayerController _playerController;
    private readonly ShootingArea _shootingArea;
    private readonly PlayerStats _playerStats;
    public event Action OnHealthChange;

    public PlayerController PlayerController => _playerController;
    public ShootingArea ShootingArea => _shootingArea;
    public PlayerStats PlayerStats => _playerStats;

    public PlayerFacade(PlayerController playerController, ShootingArea shootingArea, PlayerStats playerStats)
    {
        _playerController = playerController;
        _shootingArea = shootingArea;
        _playerStats = playerStats;
    }
    public void ChangeHealth(float amount)
    {
        _playerStats.ChangeHealth(amount);
        OnHealthChange?.Invoke();
    }
    
}
