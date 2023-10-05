using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Zenject;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private ItemSlot _itemSlotPrefab;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private GameObject _inventorySpace;

    private ObjectPool<ItemSlot> _itemSlotPool;
    private Stack<ItemSlot> _currentSlots = new Stack<ItemSlot>();

    private InventoryController _inventoryController;
    private PlayerFacade _playerFacade;


    [Inject]
    public void Construct(InventoryController controller, PlayerFacade playerFacade)
    {
        _inventoryController = controller;
        _playerFacade = playerFacade;
    }
    private void Start()
    {
        _itemSlotPool = new ObjectPool<ItemSlot>(() =>
        {
            var slot = Instantiate(_itemSlotPrefab, _contentTransform);
            return slot;
        }, (itemSlot) =>
        {
            itemSlot.gameObject.SetActive(true);
        }, (itemSlot) =>
        {
            itemSlot.gameObject.SetActive(false);
        }, (itemSlot) =>
        {
            Destroy(itemSlot.gameObject);
        },true, 10, 100);
    }
    private void OnEnable()
    {
        _closeButton.onClick.AddListener(CloseInventory);
        _openButton.onClick.AddListener(GenerateItems);
        _inventoryController.OnInventoryChange += UpdateInvetory;
    }
    private void OnDisable()
    {
        _closeButton?.onClick.RemoveListener(CloseInventory);
        _openButton?.onClick.RemoveListener(GenerateItems);
        _inventoryController.OnInventoryChange -= UpdateInvetory;
    }
    private void GenerateItems()
    {
        if(_inventorySpace.activeSelf) return;
        _inventorySpace.SetActive(true);
        foreach(KeyValuePair<ItemData, int> data in _inventoryController.GetInventoryItems())
        {
            ItemSlot slot = _itemSlotPool.Get();
            slot.SetItemSlot(data.Key, data.Value);
            slot.OnSlotClicked += ItemAction;
            slot.OnSlotDischarge += ItemDischarge;
            _currentSlots.Push(slot);
            
        }
    }
    private void UpdateInvetory()
    {
        if(_inventorySpace.activeSelf == false) return;
        while (_currentSlots.Count > 0)
        {
            ItemSlot slot = _currentSlots.Pop();
            slot.ResetItemSlot();
            slot.OnSlotClicked -= ItemAction;
            slot.OnSlotDischarge -= ItemDischarge;
            _itemSlotPool.Release(slot);
        }
        foreach (KeyValuePair<ItemData, int> data in _inventoryController.GetInventoryItems())
        {
            ItemSlot slot = _itemSlotPool.Get();
            slot.SetItemSlot(data.Key, data.Value);
            slot.OnSlotClicked += ItemAction;
            slot.OnSlotDischarge += ItemDischarge;
            _currentSlots.Push(slot);

        }
    }
    private void CloseInventory()
    {
        _inventorySpace.SetActive(false);
        while(_currentSlots.Count > 0)
        {
            ItemSlot slot = _currentSlots.Pop();
            slot.ResetItemSlot();
            slot.OnSlotClicked -= ItemAction;
            slot.OnSlotDischarge -= ItemDischarge;
            _itemSlotPool.Release(slot);
        }
        
    }
    private void ItemAction(ItemData data)
    {
        if(data.ItemName == "Heal 1")
        {
            _playerFacade.ChangeHealth(50);
            _inventoryController.RemoveItem(data, 1);
        }
    }
    private void ItemDischarge(ItemData data)
    {
        _inventoryController.RemoveItem(data, _inventoryController.GetItemAmountByName(data.ItemName));
    }

}
