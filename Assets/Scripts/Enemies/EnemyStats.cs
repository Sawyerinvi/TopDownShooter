using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _baseDamage;

    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public event Action<float> OnDamageTaken;
    public event Action OnDeath;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;
        OnDamageTaken?.Invoke(_currentHealth);
        if(_currentHealth <= 0) OnDeath?.Invoke();
    }

}
