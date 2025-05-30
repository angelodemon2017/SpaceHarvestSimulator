using UnityEngine;

[CreateAssetMenu(menuName = "States/Harvesting Source State", order = 1)]
public class HarvestingSourceState : DronState
{
    [SerializeField] private float _timeIdle;

    private ResourceView _resourceTarget;
    private float _timerIdle;

    protected override void Init()
    {
        base.Init();
        _timerIdle = _timeIdle;
        _resourceTarget = _dronView.GetNearSource();
    }

    public override void ResourcePickUpped(ResourceView resourceView)
    {
        if (_resourceTarget == resourceView)
        {
            IsFinished = true;
        }
    }

    protected override void Run()
    {
        if (_timerIdle > 0f)
        {
            _timerIdle -= Time.deltaTime;
        }

        if (!_resourceTarget.IsAlive)
        {
            IsFinished = true;
        }

        if (_timerIdle < 0f && _resourceTarget.IsAlive)
        {
            _resourceTarget.Despawn();
            _dronView.SourceUpDown(true);
            IsFinished = true;
        }
    }

    public override bool CheckRules(IStatesCharacter character)
    {
        return false;
    }
}