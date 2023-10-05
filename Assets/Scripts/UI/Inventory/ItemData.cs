using System;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "SO/Data/Base Item", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private int _amount;

    public string ItemName => _itemName;
    public string Description => _description;
    public Sprite IconSprite => _iconSprite;
    public int Amount => _amount;
    public override bool Equals(object obj)
    {
        if (obj is ItemData otherItem)
        {
            return ItemName.Equals(otherItem.ItemName);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ItemName.GetHashCode();
    }
}

