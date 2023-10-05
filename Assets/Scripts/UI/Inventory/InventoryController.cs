using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;

public class InventoryController
{
    private Dictionary<ItemData, int> _items = new Dictionary<ItemData, int>();
    public event Action OnInventoryChange;
    public void AddItem(ItemData item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] += item.Amount;
        }
        else
        {
            _items.Add(item, item.Amount);
        }
        OnInventoryChange?.Invoke();
    }
    public void RemoveItem(ItemData item, int amount)
    {
        if (_items.ContainsKey(item) && _items[item] > 1)
        {
            _items[item] -= amount;
        }
        else
        {
            _items.Remove(item);
        }
        OnInventoryChange?.Invoke();
    }
    public ReadOnlyDictionary<ItemData, int> GetInventoryItems()
    {
        return new ReadOnlyDictionary<ItemData, int>(_items);
        
    }
    public int GetItemAmountByName(string name)
    {
        foreach (var item in _items)
        { 
            if(item.Key.ItemName == name) return item.Value;
        }
        return 0;
    }
    public ItemData GetItemDataByName(string name)
    {
        foreach (var item in _items)
        {
            if (item.Key.ItemName == name) return item.Key;
        }
        return null;
    }
}

