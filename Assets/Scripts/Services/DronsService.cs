using System;
using System.Collections.Generic;
using Zenject;

public class DronsService : IInitializable
{
    private DataModelStorage _dataModelStorage;
    private SignalBus _signalBus;
    private DronView.Factory _dronsFactory;
    private MapView _mapView;

    private Dictionary<EFraction, List<DronView>> drons = new();

    public DronsService(
        DataModelStorage dataModelStorage,
        DronView.Factory dronsFactory,
        MapView mapView,
        SignalBus signalBus)
    {
        _dataModelStorage = dataModelStorage;
        _dronsFactory = dronsFactory;
        _mapView = mapView;
        _signalBus = signalBus;
        _signalBus.Subscribe<CountDronsSignal>(ChangeCountDrons);
    }

    public void Initialize()
    {
        var spm = _dataModelStorage.GetModel<SimulationParametrsModel>();
        ChangeCountDrons(spm.CountDronsByTeam);
    }

    private void ChangeCountDrons(CountDronsSignal countDronsSignal)
    {
        ChangeCountDrons(countDronsSignal.Count);
    }

    private void ChangeCountDrons(int count)
    {
        foreach (EFraction fraction in Enum.GetValues(typeof(EFraction)))
        {
            if (fraction == EFraction.none) continue;

            if (!drons.TryGetValue(fraction, out List<DronView> fractionDrons))
            {
                fractionDrons = new List<DronView>();
                drons[fraction] = fractionDrons;
            }

            int difference = count - fractionDrons.Count;

            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    DronView newDron = _dronsFactory.Create();
                    newDron.SetTeam(fraction);
                    newDron.transform.position = _mapView.DronSpawnPointByFraction(fraction).position;
                    fractionDrons.Add(newDron);
                }
            }
            else if (difference < 0)
            {
                int removeCount = -difference;

                for (int i = removeCount - 1; i >= 0; i--)
                {
                    DronView dronToRemove = fractionDrons[i];
                    dronToRemove.Despawn();
                    fractionDrons.RemoveAt(i);
                }
            }
        }
    }
}