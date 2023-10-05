using UnityEngine;
using Zenject;

public class ItemFactory
{
    private readonly DiContainer _container;
    public ItemFactory(DiContainer container)
    {
        _container = container; 
    }
    public Item Create(GameObject prefab)
    {
        var item = _container.InstantiatePrefabForComponent<Item>(prefab);
        return item;
    }
}
