using UnityEngine.AI;
using UnityEngine;
using Zenject;
using TMPro;

public class DronView : MonoBehaviour, IPoolable<IMemoryPool>, IStatesCharacter
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private DronState _startState;
    [SerializeField] private GameObject _source;
    [SerializeField] private TextMeshProUGUI _stateLabel;

    private Transform _myBase;
    private bool _haveResource = false;

    private MapView _mapView;
    private DronState _currentState;
    private SignalBus _signalBus;
    private DataModelStorage _dataModelStorage;
    private ResourcesService _resourcesService;
    private FractionConfigs _fractionConfigs;
    private EFraction _currentFraction;

    public bool HaveResource => _haveResource;
    public bool HaveResourcesInScene => _resourcesService.HaveResources;
    public Transform GetBaseTransform => _myBase;
    public EFraction GetFraction => _currentFraction;

    [Inject]
    private void Constructor(
        MapView mapView,
        DataModelStorage dataModelStorage,
        ResourcesService resourcesService,
        FractionConfigs fractionConfigs,
        SignalBus signalBus)
    {
        _mapView = mapView;
        _dataModelStorage = dataModelStorage;
        _resourcesService = resourcesService;
        _fractionConfigs = fractionConfigs;
        _signalBus = signalBus;
    }

    public void SourceUpDown(bool isUp)
    {
        _haveResource = isUp;
        _source.SetActive(_haveResource);
    }

    private void InitSignals()
    {
        _signalBus.Subscribe<SpawnedResourceSignal>(SpawnedResource);
        _signalBus.Subscribe<SpeedDronsSignal>(UpdateSpeed);
    }

    private void UnscribeSignals()
    {
        _signalBus.Unsubscribe<SpawnedResourceSignal>(SpawnedResource);
        _signalBus.Unsubscribe<SpeedDronsSignal>(UpdateSpeed);
    }

    public ResourceView GetNearSource()
    {
        return _resourcesService.GetNearByPosition(transform.position);
    }

    public void SetTeam(EFraction fraction)
    {
        _currentFraction = fraction;
        _myBase = _mapView.BasePointByFraction(fraction);
        var cnf = _fractionConfigs.GetConfigByEFrac(fraction);
        _meshRenderer.material = cnf.material;
    }

    public void GoTo(Vector3 newTarget)
    {
        _navMeshAgent.SetDestination(newTarget);
    }

    public void UploadedResource()
    {
        SourceUpDown(false);
        _signalBus.Fire(new UploadingResourceSignal()
        {
            eFraction = _currentFraction,
            Amount = 1,
        });
    }

    private void UpdateSpeed(SpeedDronsSignal speedDronsSignal)
    {
        UpdateSpeed(speedDronsSignal.Speed);
    }

    private void UpdateSpeed(float newSpeed)
    {
        _navMeshAgent.speed = newSpeed;
    }

    private void SpawnedResource(SpawnedResourceSignal resourceView)
    {
        _currentState.SpawnedResources(resourceView.Resource);
    }

    public MonoBehaviour GetEntityMonobeh() => this;
    public bool IsFinishedCurrentState() => _currentState.IsFinished;
    public Transform GetTransform() => transform;

    private void Update()
    {
        _currentState.RunState();
    }

    public void SetState(State state)
    {
        if (_currentState != null && _currentState.StateKey == state.StateKey)
        {
            return;
        }

        _currentState?.ExitState();
        if (_currentState != null)
        {
            Destroy(_currentState);
        }

        _currentState = (DronState)Instantiate(state);
        _stateLabel.text = _currentState.StateKey;
        _currentState.InitState(this);
    }

#region PoolAndFabric
    private IMemoryPool _pool;

    public void Despawn()
    {
        _pool?.Despawn(this);
    }

    public void OnDespawned()
    {
        _pool = null;
        gameObject.SetActive(false);
        UnscribeSignals();
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
        SourceUpDown(false);
        gameObject.SetActive(true);
        InitSignals();
        var spm = _dataModelStorage.GetModel<SimulationParametrsModel>();
        UpdateSpeed(spm.DronsSpeed);
        SetState(_startState);
    }

    public class Pool : MonoPoolableMemoryPool<IMemoryPool, DronView> { }

    public class Factory : PlaceholderFactory<DronView> { }
#endregion
}