using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _mutantPrefab;
    [SerializeField] private GameObject _zombiePrefab;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UIFacade>().FromSubContainerResolve().
                ByNewPrefabInstaller<UIInstaller>(_playerUI).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerFacade>().FromSubContainerResolve().
                ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<InventoryController>().FromNew().AsSingle().NonLazy();

        Container.Bind<EnemyFactory>().FromNew().AsSingle();
        Container.Bind<ItemFactory>().FromNew().AsSingle();
    }
}
