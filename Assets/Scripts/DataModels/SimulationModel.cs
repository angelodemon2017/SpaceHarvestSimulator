using System;
using System.Collections.Generic;

[Serializable]
public class SimulationModel : IDataModel
{
    private Dictionary<EFraction, FractionModel> fracs = new();

    public string ModelName => nameof(SimulationModel);

    public FractionModel GetFracByEnum(EFraction fracEn)
    {
        if (fracs.TryGetValue(fracEn, out FractionModel fractionModel))
        {
            return fractionModel;
        }
        else
        {
            fracs.Add(fracEn, new FractionModel(fracEn));
        }
        return fracs[fracEn];
    }
}