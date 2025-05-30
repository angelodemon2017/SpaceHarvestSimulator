using UnityEngine;

[CreateAssetMenu(menuName = "States/Uploading State", order = 1)]
public class UploadingState : DronState
{
    [SerializeField] private float _timeIdle;

    private float _timerIdle;

    protected override void Init()
    {
        base.Init();
        _timerIdle = _timeIdle;
    }

    protected override void Run()
    {
        if (_timerIdle > 0f)
        {
            _timerIdle -= Time.deltaTime;
        }

        if (_timerIdle < 0f)
        {
            _dronView.UploadedResource();
            IsFinished = true;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override bool CheckRules(IStatesCharacter character)
    {
        return false;
    }
}