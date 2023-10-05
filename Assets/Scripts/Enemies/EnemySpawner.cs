using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemiesToSpawn;
    [SerializeField] private List<GameObject> _enemyPrefabs = new List<GameObject>();
    private EnemyFactory _enemyFactory;

    [Inject]
    public void Construct(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }
    private void Start()
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            var prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
            var enemy = _enemyFactory.Create(prefab);
            enemy.transform.position = transform.position;
            enemy.transform.parent = transform;
            
        }
    }

}
