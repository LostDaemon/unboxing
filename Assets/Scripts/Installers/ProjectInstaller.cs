using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TouchInputSystem>().AsSingle();
        Container.Bind<InputManager>().AsSingle();
    }
}