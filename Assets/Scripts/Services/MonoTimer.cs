using System.Collections;
using UnityEngine;
using Zenject;

public class MonoTimer : MonoBehaviour
{
    private SimulationParametrsModel _simulationParametrsModel;
    private SignalBus _signalBus;

    private Coroutine _timer;

    [Inject]
    public void Constructor(SignalBus signalBus, DataModelStorage dataModelStorage)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<SpeedSpawnResourcesSignal>(UpdateSpeed);
        _simulationParametrsModel = dataModelStorage.GetModel<SimulationParametrsModel>();

        _timer = StartCoroutine(Timer());
    }

    private void UpdateSpeed(SpeedSpawnResourcesSignal speedSignal)
    {
        _simulationParametrsModel.SpeedSourceSpawn = speedSignal.Speed;
        StopCoroutine(_timer);
        _timer = StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        var spawnInterval = _simulationParametrsModel.SpeedSourceSpawn;
        _signalBus.Fire(new TimerSpawnSignal());
        yield return new WaitForSeconds(spawnInterval);
        _timer = StartCoroutine(Timer());
    }
}