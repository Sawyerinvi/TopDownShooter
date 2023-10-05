using Zenject;


public class UIInstaller : Installer<UIInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UIFacade>().FromNew().AsSingle();
    }
}
