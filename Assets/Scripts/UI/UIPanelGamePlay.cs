using UnityEngine;
using TMPro;
using Zenject;

public class UIPanelGamePlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RedFraction;
    [SerializeField] private TextMeshProUGUI BlueFraction;

    private DataModelStorage _dataModelStorage;
    private SimulationModel _simulationModel;

    private SignalBus _signalBus;

    [Inject]
    public void Constructor(SignalBus signalBus,
        DataModelStorage dataModelStorage)
    {
        _signalBus = signalBus;
        _dataModelStorage = dataModelStorage;
        _simulationModel = _dataModelStorage.GetModel<SimulationModel>();
        _signalBus.Subscribe<UploadingResourceSignal>(UpdateResources);

        Init();
    }

    private void UpdateResources(UploadingResourceSignal uploadingResource)
    {
        var fraction = _simulationModel.GetFracByEnum(uploadingResource.eFraction);
        fraction.Resources += uploadingResource.Amount;
        UpdateCounter(fraction);
    }

    public void UpdateCounter(FractionModel fractionModel)
    {
        switch (fractionModel.Fraction)
        {
            case EFraction.Red:
                RedFraction.text = $"{fractionModel.Resources}";
                break;
            case EFraction.Blue:
                BlueFraction.text = $"{fractionModel.Resources}";
                break;
        }
    }

    public void Init()
    {
        RedFraction.text = "0";
        BlueFraction.text = "0";
    }
}