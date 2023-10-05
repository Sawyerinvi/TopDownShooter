using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _healthText;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _stats.MaxHealth;
        ChangeHealth(_maxHealth);
    }
    private void OnEnable()
    {
        _stats.OnDamageTaken += ChangeHealth;
    }
    private void OnDisable()
    {
        _stats.OnDamageTaken -= ChangeHealth;
    }

    private void ChangeHealth(float health)
    {
        if (health < 0) health = 0;
        _healthText.text = $"{health}/{_maxHealth}";
        var percentage = health / _maxHealth;
        _healthBar.fillAmount = percentage;
    }
}
