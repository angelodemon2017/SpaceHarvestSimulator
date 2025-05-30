using UnityEngine;
using Zenject;

public class UIPanelConfigs : MonoBehaviour
{
    [SerializeField] private UIPanelConfig _configCountDrons;
    [SerializeField] private UIPanelConfig _configSpeedDrons;
    [SerializeField] private UIPanelConfigInput _configSpeedSpawners;

    private DataModelStorage _dataModelStorage;
    private SignalBus _signalBus;

    [Inject]
    public void Constructor(SignalBus signalBus,
        DataModelStorage dataModelStorage)
    {
        _dataModelStorage = dataModelStorage;
        var spm = _dataModelStorage.GetModel<SimulationParametrsModel>();
        _configCountDrons.UpdateLabel(spm.CountDronsByTeam);
        _configSpeedDrons.UpdateLabel((int)spm.DronsSpeed);
        _configSpeedSpawners.UpdateLabel((int)spm.SpeedSourceSpawn);

        _configCountDrons.changeSlide += ChangeCountDrons;
        _configSpeedDrons.changeSlide += ChangeSpeedDrons;
        _configSpeedSpawners.changeValue += ChangeSpeedSpawn;
        _signalBus = signalBus;
    }

    private void ChangeCountDrons(int count)
    {
        _signalBus.Fire(new CountDronsSignal()
        {
            Count = count,
        });
    }

    private void ChangeSpeedDrons(int speed)
    {
        _signalBus.Fire(new SpeedDronsSignal()
        {
            Speed = speed,
        });
    }

    private void ChangeSpeedSpawn(int speed)
    {
        _signalBus.Fire(new SpeedSpawnResourcesSignal()
        {
            Speed = speed,
        });
    }

    private void OnDestroy()
    {
        _configCountDrons.changeSlide -= ChangeCountDrons;
        _configSpeedDrons.changeSlide -= ChangeSpeedDrons;
        _configSpeedSpawners.changeValue -= ChangeSpeedSpawn;
    }
}