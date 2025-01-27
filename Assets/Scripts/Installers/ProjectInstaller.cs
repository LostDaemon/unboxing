using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public GridSettingsScriptableObject GridSettings;
    public CameraSettingsScriptableObject CameraSettings;

    public override void InstallBindings()
    {
        Container.Bind<TouchInputSystem>().AsSingle();
        Container.Bind<InputManager>().AsSingle();
        Container.Bind<GridSettingsScriptableObject>().FromInstance(GridSettings).AsSingle();
        Container.Bind<CameraSettingsScriptableObject>().FromInstance(CameraSettings).AsSingle();
        Container.Bind<GameSceneManager>().AsSingle();
        Container.Bind<GameManager>().AsSingle();
        Container.Bind<LootRepository>().AsSingle();
        Container.Bind<RewardService>().AsSingle();
    }
}