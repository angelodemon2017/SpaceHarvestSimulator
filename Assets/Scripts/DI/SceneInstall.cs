using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstall : MonoInstaller
{
    [SerializeField] private List<State> _states;
    [SerializeField] private DataModelConfig _dataModelConfig;
    [SerializeField] private FractionConfigs _fractionConfigs;

    [SerializeField] private PrefabsConfig _prefabsConfig;

    [SerializeField] private MonoTimer _monoTimer;
    [SerializeField] private MapView _mapView;
    [SerializeField] private UIPanelConfigs _uIPanelConfigs;
    [SerializeField] private UIPanelGamePlay _uIPanelGamePlay;

    public override void InstallBindings()
    {
        InstallConfig();
        InstallPrefabs();
        InstallServices();
        InstallObjects();
        InstallSignals();
        InjectStates();
    }

    private void InstallConfig()
    {
        Container.BindInstance(_dataModelConfig).AsSingle();
        Container.BindInstance(_fractionConfigs).AsSingle();
    }

    private void InjectStates()
    {
        _states.ForEach(s => Container.Inject(s));
    }

    private void InstallPrefabs()
    {
        Container.Bind<BaseView>().FromComponentInNewPrefab(_prefabsConfig.baseView).AsTransient();

        BindPoolPrefab<ResourceView, ResourceView.Factory, ResourceView.Pool>(_prefabsConfig.resourceView);
        BindPoolPrefab<DronView, DronView.Factory, DronView.Pool>(_prefabsConfig.dronView);
    }

    private void BindPoolPrefab<T, T2, T3>(T prefab, int startInit = 0)
        where T : MonoBehaviour, IPoolable<IMemoryPool>
        where T2 : PlaceholderFactory<T>
        where T3 : MonoPoolableMemoryPool<IMemoryPool, T>
    {
        Container.BindFactory<T, T2>()
            .FromPoolableMemoryPool<T, T3>(poolBinder => poolBinder
                .WithInitialSize(startInit)
                .FromComponentInNewPrefab(prefab));
    }

    private void InstallServices()
    {
        Container.Bind<DataModelStorage>().AsSingle();
        Container.BindInterfacesAndSelfTo<ResourcesService>().AsSingle();
        Container.BindInterfacesAndSelfTo<DronsService>().AsSingle();
    }

    private void InstallObjects()
    {
        Container.BindInstance(_monoTimer).AsSingle();
        Container.BindInstance(_mapView).AsSingle();
        Container.BindInstance(_uIPanelGamePlay).AsSingle();
        Container.BindInstance(_uIPanelConfigs).AsSingle();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<SpeedSpawnResourcesSignal>();
        Container.DeclareSignal<SpeedDronsSignal>();
        Container.DeclareSignal<CountDronsSignal>();
        Container.DeclareSignal<TimerSpawnSignal>();
        Container.DeclareSignal<SpawnedResourceSignal>();
        Container.DeclareSignal<ResourcePickUpSignal>();
        Container.DeclareSignal<UploadingResourceSignal>();
    }
}