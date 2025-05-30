using UnityEngine;
using Zenject;

public class BaseView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private FractionConfigs _fractionConfigs;

    [Inject]
    private void Constructor(
        FractionConfigs fractionConfigs)
    {
        _fractionConfigs = fractionConfigs;
    }

    public void SetTeam(EFraction fraction)
    {
        var cnf = _fractionConfigs.GetConfigByEFrac(fraction);
        _meshRenderer.material = cnf.material;
    }
}