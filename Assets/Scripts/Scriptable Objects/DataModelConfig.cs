using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataModelConfig", menuName = "Data/Data Model Config")]
public class DataModelConfig : ScriptableObject
{
    public SimulationParametrsModel simulationParametrs;
    public SimulationModel simulation;

    public Dictionary<string, IDataModel> DefaultModels = new Dictionary<string, IDataModel>();

    public void AddDefaultModel(IDataModel model)
    {
        if (model == null) return;

        DefaultModels.TryAdd(model.ModelName, model);
    }
}