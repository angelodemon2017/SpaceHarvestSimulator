using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ResourcesService : IInitializable
{
    private const int MaxResources = 100;

    private DiContainer _diContainer;
    private SignalBus _signalBus;
    private ResourceView.Factory _resourcesFactory;
    private MapView _mapView;

    private List<ResourceView> _resourcesView = new();

    public bool HaveResources => _resourcesView.Count() > 0;
    
    public ResourcesService(
        DiContainer diContainer,
        ResourceView.Factory factory,
        MapView mapView)
    {
        _diContainer = diContainer;
        _resourcesFactory = factory;
        _mapView = mapView;
    }

    public void Initialize()
    {
        _signalBus = _diContainer.Resolve<SignalBus>();
        _signalBus.Subscribe<TimerSpawnSignal>(OnTimer);
    }

    private void OnTimer()
    {
        if (_resourcesView.Count > MaxResources)
        {
            return;
        }

        var newSource = _resourcesFactory.Create();
        newSource.transform.position = _mapView.GetRandomPointForSource();
        _resourcesView.Add(newSource);
        _signalBus.Fire(new SpawnedResourceSignal()
        {
            Resource = newSource,
        });
    }

    public ResourceView GetNearByPosition(Vector3 center)
    {
        return _resourcesView.OrderBy(r => Vector3.Distance(r.transform.position, center)).FirstOrDefault();
    }
}