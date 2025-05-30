using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
/*    [SerializeField] private DataModelConfig _dataModelConfig;
    [SerializeField] private LevelsConfig _levelsConfig;
    [SerializeField] private WindowsConfig _windowsConfig;
    [SerializeField] private TestPoolObject _testPoolObject;/**/

    public override void InstallBindings()
    {
/*        InstallConfig();
        InstallServices();
        InstallUI();
        InstallFSM();

        Container.BindFactory<TestPoolObject, TestPoolObject.Factory>()
            .FromPoolableMemoryPool<TestPoolObject, TestPoolObject.Pool>(poolBinder => poolBinder
                .WithInitialSize(2)
                .FromComponentInNewPrefab(_testPoolObject));/**/
    }

    private void InstallConfig()
    {
/*        Container.BindInstance(_dataModelConfig).AsSingle();
        Container.BindInstance(_levelsConfig).AsSingle();
        Container.BindInstance(_windowsConfig).AsSingle();/**/
    }

    private void InstallServices()
    {
/*        Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
        Container.BindInterfacesAndSelfTo<FSMService>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
        Container.BindInterfacesAndSelfTo<DataModelStorage>().AsSingle();/**/
    }

    private void InstallUI()
    {
/*        Container.BindFactory<UIWindow, UIWindow.Factory>();
        Container.Bind<LoadingWindow>().FromComponentInNewPrefab(_windowsConfig.LoadingWindow).AsSingle();
        Container.Bind<MainMenuWindow>().FromComponentInNewPrefab(_windowsConfig.MenuWindow).AsSingle();
        Container.Bind<PauseWindow>().FromComponentInNewPrefab(_windowsConfig.PauseWindow).AsSingle();
        Container.Bind<GameplayWindow>().FromComponentInNewPrefab(_windowsConfig.GameplayWindow).AsSingle();/**/
    }

    private void InstallFSM()
    {
/*        Container.Bind<MainMenuState>().AsSingle();
        Container.Bind<GameplayState>().AsSingle();
        Container.Bind<PauseMenuState>().AsSingle();/**/
    }
}