using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _disposeButton;
    [SerializeField] private Button _useButton;

    public event Action<ItemData> OnSlotClicked;
    public event Action<ItemData> OnSlotDischarge;

    private ItemData _data;
    private void Start()
    {
        _useButton.onClick.AddListener(ItemAction);
        _disposeButton.onClick.AddListener(DischargeItem);
    }

    public void SetItemSlot(ItemData data, int amount)
    {
        _data = data;
        _itemName.text = $"{_data.ItemName}\n {amount}";
        _itemIcon.sprite = _data.IconSprite;
    }
    public void ResetItemSlot()
    {
        _data = null;
        _itemName.text = null;
        _itemIcon.sprite = null ;
    }
    private void ItemAction()
    {
        OnSlotClicked?.Invoke(_data);
    }
    private void DischargeItem()
    {
        OnSlotDischarge?.Invoke(_data);
    }
    
}
