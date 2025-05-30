using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Fraction Config", menuName = "Fraction Configs")]
public class FractionConfigs : ScriptableObject
{
    [SerializeField] private List<FractionConfig> fractionConfigs = new();

    private Dictionary<EFraction, FractionConfig> _cacheConfigs = new();

    public FractionConfig GetConfigByEFrac(EFraction frac)
    {
        if (_cacheConfigs.TryGetValue(frac, out FractionConfig fractionConfig))
        {
            return fractionConfig;
        }
        _cacheConfigs.Add(frac, fractionConfigs.FirstOrDefault(f => f.Fraction == frac));
        return _cacheConfigs[frac];
    }
}