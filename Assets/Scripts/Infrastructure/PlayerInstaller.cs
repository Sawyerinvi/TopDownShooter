using UnityEngine;
using Zenject;

public class PlayerInstaller : Installer<PlayerInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerFacade>().FromNew().AsSingle();
        Container.Bind<PlayerController>().FromComponentOnRoot().AsSingle();
        Container.Bind<ShootingArea>().FromComponentOnRoot().AsSingle();
        Container.Bind<PlayerStats>().FromNew().AsSingle();
    }
}
