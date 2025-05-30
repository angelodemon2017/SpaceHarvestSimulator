using UnityEngine;
using Zenject;

public class ResourceView : MonoBehaviour, IPoolable<IMemoryPool>
{
    private IMemoryPool _pool;

    private SignalBus _signalBus;

    public bool IsAlive => _pool != null;

    [Inject]
    public void Constructor(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Despawn()
    {
        _signalBus.Fire(new ResourcePickUpSignal()
        {
            resourceView = this,
        });
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        _pool = null;
        gameObject.SetActive(false);
        _signalBus.Fire(new ResourcePickUpSignal());
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
        gameObject.SetActive(true);
    }

    public class Pool : MonoPoolableMemoryPool<IMemoryPool, ResourceView> { }

    public class Factory : PlaceholderFactory<ResourceView> { }
}