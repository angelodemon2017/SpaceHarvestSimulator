using UnityEngine;
using Zenject;

public class MapView : MonoBehaviour, IInitializable
{
    [SerializeField] private Transform _redBase;
    [SerializeField] private Transform _blueBase;

    [SerializeField] private Transform _redSpawnPoint;
    [SerializeField] private Transform _blueSpawnPoint;

    [SerializeField] private Transform _cornerMin;
    [SerializeField] private Transform _cornerMax;

    private DiContainer _diContainer;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
        InitBases();
    }

    public void Initialize()
    {
        InitBases();
    }

    private void InitBases()
    {
        SpawnBaseByFraction(EFraction.Red);
        SpawnBaseByFraction(EFraction.Blue);
    }

    private void SpawnBaseByFraction(EFraction fraction)
    {
        var bv = _diContainer.Resolve<BaseView>();
        bv.SetTeam(fraction);
        bv.transform.position = BasePointByFraction(fraction).position;
    }

    public Transform BasePointByFraction(EFraction fraction) => fraction switch
    {
        EFraction.Red => _redBase,
        EFraction.Blue => _blueBase,
        _ => null
    };

    public Transform DronSpawnPointByFraction(EFraction fraction) => fraction switch
    {
        EFraction.Red => _redSpawnPoint,
        EFraction.Blue => _blueSpawnPoint,
        _ => null
    };

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 center = (_cornerMin.position + _cornerMax.position) / 2f;
        Vector3 size = _cornerMax.position - _cornerMin.position;
        Gizmos.DrawWireCube(center, size);
    }

    public Vector3 GetRandomPointForSource()
    {
        float randomX = Random.Range(_cornerMin.position.x, _cornerMax.position.x);
        float randomZ = Random.Range(_cornerMin.position.z, _cornerMax.position.z);

        return new Vector3(randomX, 0, randomZ);
    }
}