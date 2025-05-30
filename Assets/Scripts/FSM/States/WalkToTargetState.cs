using UnityEngine;

[CreateAssetMenu(menuName = "States/Walk To Target State", order = 1)]
public class WalkToTargetState : DronState
{
    [SerializeField] private float _doneDistance;
    [SerializeField] private HarvestingSourceState _stateHarvesting;
    [SerializeField] private UploadingState _uploadingState;
    [SerializeField] private IdleState _idleState;

    private Transform _target;
    private ResourceView _resourceView;

    protected override void Init()
    {
        base.Init();
        TryFindAndGoTarget();
    }

    public override void SpawnedResources(ResourceView resourceView)
    {
        if (!_dronView.HaveResource &&
            Vector3.Distance(resourceView.transform.position, _dronView.transform.position) <
            Vector3.Distance(_target.position, _dronView.transform.position))
        {
            _target = resourceView.transform;
            _dronView.GoTo(_target.position);
        }
    }

    public override void ResourcePickUpped(ResourceView resourceView)
    {
        if (_target == resourceView.transform)
        {
            TryFindAndGoTarget();
        }
    }

    private void TryFindAndGoTarget()
    {
        if (_dronView.HaveResource)
        {
            _target = _dronView.GetBaseTransform;
        }
        else
        {
            if (_dronView.HaveResourcesInScene)
            {
                _resourceView = _dronView.GetNearSource();
                _target = _resourceView.transform;
            }
            else
            {
                _dronView.SetState(_idleState);
            }
        }
        _dronView.GoTo(_target.position);
    }

    protected override void Run()
    {
        base.Run();
        if (Vector3.Distance(_target.position, _dronView.transform.position) < _doneDistance)
        {
            _dronView.SetState(_dronView.HaveResource ?
                _uploadingState : _stateHarvesting);
        }
    }

    public override void ExitState()
    {
        _dronView.GoTo(_dronView.transform.position);
    }

    public override bool CheckRules(IStatesCharacter character)
    {
        return ReadyWalkToTarget(character);
    }

    public bool ReadyWalkToTarget(IStatesCharacter character)
    {
        if (character.IsFinishedCurrentState() && character is DronView dronView)
        {
            return dronView.HaveResource ||
                !dronView.HaveResource && dronView.HaveResourcesInScene;
        }

        return false;
    }
}