using System;
using System.Collections.Generic;
using UnityEngine;

public class DataModelStorage
{
    private Dictionary<Type, IDataModel> _models = new Dictionary<Type, IDataModel>();

    public DataModelStorage(DataModelConfig dataModelConfig)
    {
        AddModel(dataModelConfig.simulationParametrs);
        AddModel(dataModelConfig.simulation);
    }

    public T GetModel<T>() where T : class, IDataModel
    {
        if (_models.TryGetValue(typeof(T), out var model))
        {
            return model as T;
        }

        Debug.LogWarning($"Model of type {typeof(T)} not found in storage");
        return null;
    }

    public void AddModel<T>(T model) where T : class, IDataModel
    {
        if (model == null)
        {
            Debug.LogError("Cannot add null model to storage");
            return;
        }

        var type = typeof(T);
        if (_models.ContainsKey(type))
        {
            Debug.LogWarning($"Model of type {type} already exists in storage. It will be replaced.");
        }

        _models[type] = model;
        Debug.Log($"Model {nameof(model)} of type {type} added to storage");
    }
}