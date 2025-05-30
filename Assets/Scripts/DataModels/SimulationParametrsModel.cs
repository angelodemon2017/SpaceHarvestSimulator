using System;
using UnityEngine;

[Serializable]
public class SimulationParametrsModel : IDataModel
{
    [Range(1, Consts.MaxDrons)]
    public int CountDronsByTeam;
    [Range(1, 10)]
    public float DronsSpeed;
    public float SpeedSourceSpawn;
    public bool ShowPaths;

    public string ModelName => nameof(SimulationParametrsModel);
}