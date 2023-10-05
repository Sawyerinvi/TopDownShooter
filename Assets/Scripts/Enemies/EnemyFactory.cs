using UnityEngine;
using Zenject;

public class EnemyFactory
{
    private readonly DiContainer _container;
    public EnemyFactory(DiContainer container)
    {
        _container = container;
    }
    public EnemyNavigation Create(GameObject prefab)
    {
        var enemy = _container.InstantiatePrefabForComponent<EnemyNavigation>(prefab);
        return enemy;
    }
}
