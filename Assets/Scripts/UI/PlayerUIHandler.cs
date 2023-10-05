using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private Button _fireButton;
    [SerializeField] private JoystickController _joystick;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _bulletText;
    private PlayerController _playerController;
    private InventoryController _inventoryController;
    private ShootingArea _shootingArea;
    private PlayerFacade _playerFacade;
    private PlayerStats _playerStats;
    //потом помен€ю
    private string _bulletName = "Bullets";

    [Inject]
    public void Construct(PlayerFacade player, InventoryController inventoryController)
    {
        _playerFacade = player;
        _inventoryController = inventoryController;
        _playerController = _playerFacade.PlayerController;
        _shootingArea = _playerFacade.ShootingArea;
        _playerStats = _playerFacade.PlayerStats;
        
    }
    private void Start()
    {
        _fireButton.onClick.AddListener(Fire);
        UpdateHealthBar();
        UpdateBulletAmount();
    }
    private void OnEnable()
    {
        _playerFacade.OnHealthChange += UpdateHealthBar;
        _inventoryController.OnInventoryChange += UpdateBulletAmount;
    }
    private void OnDisable()
    {
        _playerFacade.OnHealthChange -= UpdateHealthBar;
        _inventoryController.OnInventoryChange -= UpdateBulletAmount;
    }
    private void FixedUpdate()
    {
        var vertical = _joystick.Vertical;
        var horizontal = _joystick.Horizontal;
        if (vertical != 0 | horizontal != 0)
        {
            _playerController.Move(vertical, horizontal);
            _shootingArea.SetAimPosition(vertical, horizontal);
        }
    }
    private void Fire()
    {
        if(_playerStats.AKClipCount > 0)
        {
            _playerController.Attack();
            _playerStats.ChangeAKCount(-1);
            UpdateBulletAmount();
        }
        else
        {
            StartCoroutine(Reload());
        }
        
    }
    private IEnumerator Reload()
    {
        var bulletsInInventory = _inventoryController.GetItemAmountByName(_bulletName);
        var bulletItemData = _inventoryController.GetItemDataByName(_bulletName);
        yield return new WaitForSeconds(1);
        if(bulletsInInventory > _playerStats.AKClipSize)
        {
            _inventoryController.RemoveItem(bulletItemData, _playerStats.AKClipSize);
            _playerStats.ChangeAKCount(_playerStats.AKClipSize);
        }
        else
        {
            _inventoryController.RemoveItem(bulletItemData, bulletsInInventory);
            _playerStats.ChangeAKCount(bulletsInInventory);
        }
        UpdateBulletAmount();
    }
    private void UpdateHealthBar()
    {
        _healthText.text = $"{_playerStats.CurrentPlayerHealth} / {_playerStats.MaxPlayerHealth}";
        _healthBar.fillAmount = _playerStats.CurrentPlayerHealth / _playerStats.MaxPlayerHealth;
    }
    private void UpdateBulletAmount()
    {
        _bulletText.text = $"{_playerStats.AKClipCount} / {_inventoryController.GetItemAmountByName(_bulletName)}";
    }


}
